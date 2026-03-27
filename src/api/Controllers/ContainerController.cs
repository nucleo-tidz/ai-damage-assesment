namespace api.Controllers
{
    using api.Model;

    using infrastructure.Storage;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using service;

    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController(IContainerService containerService, IBlobStorageService blobStorageService) : ControllerBase
    {
        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }


            var detail = await containerService.GetContainerDamage(fileBytes);
          


            var response = new ContainerResponseModel
            {
                Damages = detail.Item1.Damage,
                ImageId = detail.Item2
            };

            return Ok(response);
        }

        [HttpGet("download/{imageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DownloadImage(string imageId)
        {
            byte[] imageBytes = await blobStorageService.DownloadImageAsync(imageId);
            return File(imageBytes, "image/jpg", $"{imageId}.jpg");
        }
    }
}
