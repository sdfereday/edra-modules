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

        public void Init(List<GateModel> gates)
        {
            AnimationGateData = new List<AnimationGate>();

            // Attempt to reconstruct gate data using this new instanced data
            AnimationGateData = gates.Select(gate =>
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
                    playAnimation = gate.animationName,
                    interrupts = gate.interrupts,
                    floatConditions = floatConditions == null ? new List<ConditionObject<float>>() : floatConditions,
                    boolConditions = boolConditions == null ? new List<ConditionObject<bool>>() : boolConditions
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

        // TODO: add to a class
        private string dissallow = string.Empty;
        private bool truthValue;

        private void Update()
        {
            if (AnimationGateData == null)
                return;

            /*
             We need to check:
             - if anim was interupter, if the state has changed
             - keep a record of that state 'if' the animator just exited
             - if it did just exit, but the state has not changed, and we only
             wish to play it once, then we know that we cannot push that animation
             until its state has reset again.
             */
             // This ONLY applies to animations that have an exit routine.
             // TOOD: Make a list out of this, more robust.
             if (SpriteAnimator.JustExited)
            {
                dissallow = SpriteAnimator.CurrentAnimName;                
            }

             // Check if anything's being dissallowed
            if (dissallow.Length > 0)
            {
                // Check if the state's returned to what's acceptable
                AnimationGate dissallowedGate = AnimationGateData.FirstOrDefault(x => x.playAnimation == dissallow);
                dissallow = dissallowedGate.IsTruthy() != truthValue ? string.Empty : dissallow;
            }

            AnimationGate firstTruthyGate = AnimationGateData
                .Where(x => x.playAnimation != dissallow && x.IsTruthy())
                .OrderByDescending(x => x.interrupts)
                .FirstOrDefault();

            if (firstTruthyGate != null && !SpriteAnimator.IsCurrent(firstTruthyGate.playAnimation))
            {
                SpriteAnimator.PlayAnimation(firstTruthyGate.playAnimation);

                // Expected value needed to reset (could be no longer true, or false)
                if (firstTruthyGate.interrupts)
                    truthValue = firstTruthyGate.IsTruthy();
            }
        }
    }
}