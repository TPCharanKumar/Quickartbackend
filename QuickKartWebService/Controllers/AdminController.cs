using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace QuickKartWebService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : Controller
    {

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var fileName = file.FileName;
                string Bloburl = "";
                string ConnectString = "DefaultEndpointsProtocol=https;AccountName=quickkartstorageaccg4;AccountKey=qHJCYaSsh5iMoi1R9OqeCWPTNzIQu8wiTrdQWgYi4W14iPj/zSg9yATffJx0NZI7deo7fAVPY6HF+ASt5itm4g==;EndpointSuffix=core.windows.net";
                string ContainerName = "containergrp4";
                if (file.Length > 0)
                {
                    //Your code starts

                    try
                    {
                        BlobContainerClient containerClient = new BlobContainerClient(ConnectString, ContainerName);
                        BlobClient blobClient = containerClient.GetBlobClient(fileName);

                        using Stream uploadFileStream = file.OpenReadStream();

                        blobClient.Upload(uploadFileStream);

                        uploadFileStream.Close();

                        Bloburl = blobClient.Uri.AbsoluteUri;

                    }
                    catch
                    {
                        return BadRequest();
                    }

                    return Ok(Bloburl);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }





    }
}
