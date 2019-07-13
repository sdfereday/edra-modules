using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace RedPanda.Animator
{
    [RequireComponent(typeof(SpriteAnimator))]
    public class AnimatorLogicManager : MonoBehaviour
    {
        public SpriteAnimator SpriteAnimator;
        private List<AnimationGate> AnimationGateData;

        public void Init(List<GateModelCollection> inboundGateData, string targetId)
        {
            AnimationGateData = new List<AnimationGate>();

            List<GateModel> loadedGates = inboundGateData.Where(x => x.targetEntity == targetId)
                .FirstOrDefault().gates;

            // Attempt to reconstruct gate data using this new instanced data
            AnimationGateData = loadedGates.Select(gate =>
            {
            // This is where it gets rather nasty. Compare via switch to see what we need to bind. It's also nasty
            // IMO because I have to cast it as a condition object, then convert it all to a list.
            List<ConditionObject<float>> floatConditions = gate.floatConditions != null ? gate.floatConditions.Select(f =>
                {
                    return new FloatCondition(f.Id, f.Expected, f.Value, f.LogicMethod) as ConditionObject<float>;
                }).ToList() : new List<ConditionObject<float>>();

                List<ConditionObject<bool>> boolConditions = gate.boolConditions != null ? gate.boolConditions.Select(b =>
                {
                    return new BoolCondition(b.Id, b.Expected, b.Value, b.LogicMethod) as ConditionObject<bool>;
                }).ToList() : new List<ConditionObject<bool>>();

            // Nasty stuff ended, we just return a new animation gate.
            return new AnimationGate()
                {
                    playAnimation = gate.playAnimation,
                    floatConditions = floatConditions,
                    boolConditions = boolConditions
                };
            }).ToList();
        }

        public void SetFloat(string id, float value)
        {
            if (AnimationGateData != null)
                AnimationGateData.ForEach(x => x.SetFloat(id, value));
        }

        public void SetBool(string id, bool value)
        {
            if (AnimationGateData != null)
                AnimationGateData.ForEach(x => x.SetBool(id, value));
        }

        private void Update()
        {
            if (AnimationGateData == null)
                return;

            AnimationGate firstTruthyGate = AnimationGateData
                .Where(x => x.IsTruthy())
                .FirstOrDefault();

            if (firstTruthyGate != null)
                SpriteAnimator.PlayAnimation(firstTruthyGate.playAnimation);
        }
    }
}