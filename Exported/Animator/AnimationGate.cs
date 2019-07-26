using System;
using System.Collections.Generic;
using System.Linq;

namespace RedPanda.Animator
{
    [Serializable]
    public class AnimationGate
    {
        public string playAnimation;
        public bool interrupts = false;

        public List<ConditionObject<float>> floatConditions;
        public List<ConditionObject<bool>> boolConditions;

        public void SetFloat(string query, float value)
        {
            if (floatConditions.Count == 0) return;

            floatConditions.FirstOrDefault(condition => condition.Id == query)
                .Value = value;
        }

        public void SetBool(string query, bool value)
        {
            if (boolConditions.Count == 0) return;

            boolConditions.FirstOrDefault(condition => condition.Id == query)
                .Value = value;
        }

        public bool IsTruthy()
        {
            return floatConditions.All(x => x.Assert()) &&
                boolConditions.All(x => x.Assert());
        }
    }
}