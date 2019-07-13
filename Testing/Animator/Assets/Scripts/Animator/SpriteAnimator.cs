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

        public bool JustExited { get; private set; }
        public string CurrentAnimName => CurrentAnim != null ? CurrentAnim.name : string.Empty;

        private void Update()
        {
            if (Time.time < NextFrameTime || CurrentAnim == null || JustExited) return;

            CurrentFrame += 1;

            if (CurrentFrame >= CurrentAnim.FrameCount)
            {
                CurrentFrame = 0;

                // If the animation has no loop, we only want to play it once.
                if (!CurrentAnim.Loops)
                {
                    JustExited = true;
                    return;
                }
            }

            SpriteRenderer.sprite = CurrentAnim.GetFrame(CurrentFrame);
            NextFrameTime += CurrentAnim.SecsPerFrame;
        }

        // Has to match the actual name of the object (not fully tested)
        public void PlayAnimation(string query) {
            if (!IsCurrent(query))
                CurrentFrame = 0;

            if (JustExited && !IsCurrent(query))
                JustExited = false;

            CurrentAnim = AnimationsAvailable.FirstOrDefault(x => x.name == query);
        }

        public bool IsCurrent(string query) => query == CurrentAnimName;
    }
}