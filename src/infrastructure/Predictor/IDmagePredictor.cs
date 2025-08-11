namespace infrastructure.Predictor
{
    public interface IDamagePredictor
    {
        Task<byte[]> GetDamage(byte[] containerImage);
    }
}
