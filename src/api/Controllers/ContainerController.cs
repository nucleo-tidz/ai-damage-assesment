namespace api.Controllers
{
    using api.Model;
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

            var fileName = $"{Guid.NewGuid()}.jpg";
            var filePath = Path.Combine("C:\\Apps\\Misc\\", fileName);
             System.IO.File.WriteAllBytes(filePath, detail.DamageImage);
            var imageId = Guid.NewGuid().ToString();
            var response = new ContainerResponseModel
            {
                Analysis = detail.Analysis,
                ImageId = imageId 
            };

            return Ok(response);
        }

        [HttpGet("download/{imageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DownloadImage(string imageId)
        {
            var filePath = Path.Combine("C:\\Apps\\Misc\\", imageId);
            byte[] imageBytes = System.IO.File.ReadAllBytes($"{filePath}.jpg");
            return File(imageBytes, "image/jpg", $"{imageId}.jpg");
        }
    }
}
