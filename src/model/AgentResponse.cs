namespace model
{
    public class AgentResponse
    {
        public Damage[] Damages { get; set; }
    }
    public class Damage
    {
        public string DamageType { get; set; }
        public string DamageDescription { get; set; }
        public string[] PotentialImplications { get; set; }
        public string[] RecommendedActions { get; set; }

    }

}
