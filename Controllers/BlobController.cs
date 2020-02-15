using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.AspNetCore.Cors;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace RiseRestApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly IConfiguration _config;

        public BlobController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("[Action]")]
        async public Task<IActionResult> SaveFile(IFormFile files)
        {
            var name = _config.GetValue(typeof(string), "BlobStorage:Name") as string;
            var key = _config.GetValue(typeof(string), "BlobStorage:ServiceApiKey") as string;

            var storageCredentials = new StorageCredentials("riseblob", "tjYEgCxKNtLqs49TDPDfHPeh4HG/0htvXtwAnvpPE1lPXNXOB+XF++GXc9Hbcz77O0GJ68KFa8AwXLg811FTog==");
            //var storageCredentials = new StorageCredentials(name, key);
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
        async public Task<IActionResult> RemoveFile(string fileName)
        {
            var storageCredentials = new StorageCredentials("riseblob", "tjYEgCxKNtLqs49TDPDfHPeh4HG/0htvXtwAnvpPE1lPXNXOB+XF++GXc9Hbcz77O0GJ68KFa8AwXLg811FTog==");
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            var container = cloudBlobClient.GetContainerReference("images");
            await container.CreateIfNotExistsAsync();
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            await blockBlob.DeleteAsync();
            return new JsonResult(new { success = true });
        }
    }
}