namespace api.Controllers
{
    using api.Model;

    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using service;

    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController(IContainerService containerService) : ControllerBase
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
            var imageId = Guid.NewGuid().ToString();
            var tempDir = Path.Combine(AppContext.BaseDirectory, "temp");
            Directory.CreateDirectory(tempDir);
            var filePath = Path.Combine(tempDir, $"{imageId}.jpg");
            System.IO.File.WriteAllBytes(filePath, detail.DamageImage);
            var response = new ContainerResponseModel
            {
                Damages = detail.Damage,
                ImageId = imageId
            };

            return Ok(response);
        }

        [HttpGet("download/{imageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DownloadImage(string imageId)
        {
            var tempDir = Path.Combine(AppContext.BaseDirectory, "temp");
            var filePath = Path.Combine(tempDir, $"{imageId}.jpg");
            byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
            return File(imageBytes, "image/jpg", $"{imageId}.jpg");
        }
    }
}
