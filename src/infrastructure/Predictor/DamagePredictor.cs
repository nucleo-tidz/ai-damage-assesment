using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using Microsoft.Extensions.Options;
using System.Drawing;
using System.Drawing.Imaging;

namespace infrastructure.Predictor
{
    public class DamagePredictor(IOptions<CustomVisionSettings> options) : IDamagePredictor
    {
        public async Task<byte[]> GetDamage(byte[] containerImage)
        {
          
            var visionClient = new CustomVisionPredictionClient(new ApiKeyServiceClientCredentials(options.Value.PredictionKey))
            {
                Endpoint = options.Value.Endpoint
            };

            using (var imageStream = new MemoryStream(containerImage))
            {
                var detectionResult = await visionClient.DetectImageAsync(Guid.Parse(options.Value.ProjectId), options.Value.ModelName, imageStream);

                var boundingBox = detectionResult.Predictions
                        .OrderByDescending(p => p.Probability)
                        .Where(p => p.Probability >= 0.6).Select(x => x.BoundingBox);

                if (boundingBox is not null && boundingBox.Any())
                {
                    return DrawBoundingBox(containerImage, boundingBox);
                }
                else
                    return default;
            }
        }
        private byte[] DrawBoundingBox(byte[] containerImage, IEnumerable<BoundingBox> boundingBox)
        {

            using var image = (Bitmap)System.Drawing.Image.FromStream(new MemoryStream(containerImage));
          
            using var graphics = Graphics.FromImage(image);
            foreach (var box in boundingBox)
            {
                int x = (int)(box.Left * image.Width);
                int y = (int)(box.Top * image.Height);
                int width = (int)(box.Width * image.Width);
                int height = (int)(box.Height * image.Height);
                using var pen = new Pen(Color.Red, 6);
                graphics.DrawRectangle(pen, x, y, width, height);
            }
            using var ms = new MemoryStream();
            using var bmp = new Bitmap(image);
            bmp.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }

}
