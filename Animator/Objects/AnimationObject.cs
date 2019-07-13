using UnityEngine;

namespace RedPanda.Animator
{
    [CreateAssetMenu(fileName = "New 2D Animation Object", menuName = "2D Animation Object", order = 51)]
    public class AnimationObject : ScriptableObject
    {
        public Sprite[] Frames;
        public int FPS = 32;
        public bool Loops = false;
        public int FrameCount => Frames.Length;
        public float Duration => FrameCount * FPS;
        public float SecsPerFrame => 1f / FPS;

        public Sprite GetFrame(int FrameNumber = 0)
        {
            return Frames[FrameNumber];
        }
    }
}