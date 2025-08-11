using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Predictor
{
    public class CustomVisionSettings
    {
        public string PredictionKey { get; set; }
        public string Endpoint { get; set; }
        public string ProjectId { get; set; }
        public string ModelName { get; set; }
    }
}
