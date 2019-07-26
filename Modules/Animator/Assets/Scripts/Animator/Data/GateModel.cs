using System.Collections.Generic;

namespace RedPanda.Animator
{
    public class GateModel
    {
        public string animationName;
        public bool interrupts;
        public List<GateFloat> floatConditions;
        public List<GateBool> boolConditions;
    }
}