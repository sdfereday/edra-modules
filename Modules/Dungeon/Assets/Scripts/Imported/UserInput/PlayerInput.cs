using UnityEngine; // I think you can put this within the namespace?

namespace RedPanda.UserInput
{
    // A stripped down version of playerInput.
    public class PlayerInput : MonoBehaviour
    {
        public float maxSpeed = 3f;
        public bool InteractionsEnabled { get; private set; }
        public bool MovementEnabled { get; private set; }
        public Vector2 Facing { get; set; }
        public Vector2 CurrentVelocity => rbody.velocity;

        private Rigidbody2D rbody;

        private void Start()
        {
            rbody = GetComponent<Rigidbody2D>();
            Facing = Vector2.right;

            InteractionsEnabled = true;
            MovementEnabled = true;
        }

        private void Update()
        {
            rbody.velocity = Vector2.zero;
            float xAxis = Input.GetAxisRaw(KeyCodeConsts.Horizontal);
            float yAxis = Input.GetAxisRaw(KeyCodeConsts.Vertical);

            if (xAxis != 0 || yAxis != 0)
            {
                Vector2 nm = new Vector2(xAxis * maxSpeed, yAxis * maxSpeed).normalized;
                rbody.velocity = nm * maxSpeed;
                Facing = CurrentVelocity.normalized;
            }
        }
    }
}