using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.Storage;
//using Microsoft.Azure.Storage.Auth;
//using Microsoft.Azure.Storage.Blob;
using Microsoft.AspNetCore.Cors;

namespace RiseRestApi.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        public BlobController()
        {
            //var storageCredentials = new StorageCredentials("riseblob", "tjYEgCxKNtLqs49TDPDfHPeh4HG/0htvXtwAnvpPE1lPXNXOB+XF++GXc9Hbcz77O0GJ68KFa8AwXLg811FTog==");
            //var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            //var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            //var container = cloudBlobClient.GetContainerReference("images");
            //await container.CreateIfNotExistsAsync();
            //var newBlob = container.GetBlockBlobReference("myfile");
            //await newBlob.UploadFromFileAsync(@"path\myfile.png");
            //var newBlob = container.GetBlockBlobReference("myfile");
            //await newBlob.DownloadToFileAsync("path/myfile.png", FileMode.Create);

        }
    }
}