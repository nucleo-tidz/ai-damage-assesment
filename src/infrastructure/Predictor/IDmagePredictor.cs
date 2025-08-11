using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Predictor
{
    public interface IDamagePredictor
    {
        Task<byte[]> GetDamage(byte[] containerImage);
    }
}
