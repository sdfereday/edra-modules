using UnityEngine;

// Take note that we could actually convert the player to not use a rigidBody at all, we'd need a custom collision callback for this however.
// See here for details: http://docs.unity3d.com/Manual/CollidersOverview.html
// To summarize, return to this at a later date for optimization tactics.
// Possible solution for 'shaky tile' problem when camera moves:
// http://forum.unity3d.com/threads/solved-2d-sprites-flicker-shake-on-camera-movement.270741/ (adapted here)
namespace RedPanda.Toolbox.Camera
{
    public class SmoothFollowTarget : MonoBehaviour
    {

        public float dampTime = 30f;
        public bool JustPosition = false;
        public Transform target;
        public string SearchForTag = "Player";

        // Ensure pixel-precision tracking
        public float pixelToUnits = 40f;

        void Start()
        {
            if (target == null)
            {
                target = GameObject.FindGameObjectWithTag(SearchForTag).transform;
            }
        }

        void Update()
        {
            if (JustPosition)
            {
                transform.position = new Vector3(target.position.x + .5f, target.position.y, -10f);
            }
        }

        void FixedUpdate()
        {
            if (target && !JustPosition)
            {
                Vector3 from = transform.position;
                Vector3 to = target.position;
                to.z = transform.position.z;

                from.x = RoundToNearestPixel(transform.position.x);
                from.y = RoundToNearestPixel(transform.position.y);

                to.x = RoundToNearestPixel(target.transform.position.x);
                to.y = RoundToNearestPixel(target.transform.position.y);

                transform.position -= (from - to) * dampTime * Time.fixedDeltaTime;
            }
        }

        public float RoundToNearestPixel(float unityUnits)
        {
            float valueInPixels = unityUnits * pixelToUnits;
            valueInPixels = Mathf.Round(valueInPixels);
            float roundedUnityUnits = valueInPixels * (1 / pixelToUnits);
            return roundedUnityUnits;
        }

    }
}