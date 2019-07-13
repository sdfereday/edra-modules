using System.Collections.Generic;

namespace RedPanda.Animator
{
    public class GateModel
    {
        public string playAnimation;
        public List<GateFloat> floatConditions;
        public List<GateBool> boolConditions;
    }
}