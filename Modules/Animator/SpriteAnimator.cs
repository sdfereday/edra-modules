using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace RedPanda.Animator
{
    public class SpriteAnimator : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer;
        public List<AnimationObject> AnimationsAvailable;

        private int CurrentFrame;
        private AnimationObject CurrentAnim;
        private float NextFrameTime;
    
        private void Update()
        {
            if (Time.time < NextFrameTime || CurrentAnim == null) return;

            CurrentFrame += 1;

            if (CurrentFrame >= CurrentAnim.FrameCount)
            {
                if (!CurrentAnim.Loops)
                {
                    // ... Pop back to the central decision state / exit out, as this was likely a trigger
                    return;
                }

                CurrentFrame = 0;
            }

            SpriteRenderer.sprite = CurrentAnim.GetFrame(CurrentFrame);
            NextFrameTime += CurrentAnim.SecsPerFrame;
        }

        // Has to match the actual name of the object (haven't tested it yet)
        public void PlayAnimation(string query) => CurrentAnim = AnimationsAvailable.FirstOrDefault(x => x.name == query);
    }
}