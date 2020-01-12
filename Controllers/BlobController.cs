using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RiseRestApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        [HttpPost("[Action]")]
        async public Task<IActionResult> SaveFile(IFormFile files)
        {
            var storageCredentials = new StorageCredentials("riseblob", "tjYEgCxKNtLqs49TDPDfHPeh4HG/0htvXtwAnvpPE1lPXNXOB+XF++GXc9Hbcz77O0GJ68KFa8AwXLg811FTog==");
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference("images");
            await container.CreateIfNotExistsAsync();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(files.FileName);

            using (var fileStream = files.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            return new JsonResult(new
            {
                name = blockBlob.Name,
                uri = blockBlob.Uri,
                size = blockBlob.Properties.Length
            });
        }

        [HttpPost("[Action]")]
        async public Task<IActionResult> RemoveFile()
        {
            return new JsonResult(true);
        }
    }
}