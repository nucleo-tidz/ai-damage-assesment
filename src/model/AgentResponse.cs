using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
