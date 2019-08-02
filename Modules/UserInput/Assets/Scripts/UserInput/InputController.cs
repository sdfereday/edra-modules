using UnityEngine;

namespace RedPanda.UserInput
{
    // See: https://bitbucket.org/drunkenoodle/rr-clone/src for original (and battle ideas).
    [RequireComponent(typeof(Rigidbody2D))]
    public class InputController : MonoBehaviour, IDirectionInfo
    {
        public delegate void InputAction(INPUT_TYPE inputType);
        public static event InputAction OnConfirm;
        public static event InputAction OnCancel;

        public float maxSpeed = 5f;
        public bool InteractionsEnabled { get; private set; }
        public bool MovementEnabled { get; private set; }
        public Vector2 Facing { get; set; }
        public Vector2 CurrentVelocity => rbody.velocity;

        private Rigidbody2D rbody;

        private void OnSceneLoadComplete() => ToggleMovement(true);
        private void OnSceneLoadStarted() => ToggleMovement(false);

        private void Start()
        {
            rbody = GetComponent<Rigidbody2D>();
            rbody.gravityScale = 0;
            rbody.freezeRotation = true;

            Facing = Vector2.right;

            InteractionsEnabled = OnConfirm != null || OnCancel != null;
            MovementEnabled = true;
        }

        private void Update()
        {
            rbody.velocity = Vector2.zero;

            if (InteractionsEnabled)
            {
                if (Input.GetKeyDown(KeyCodeConsts.CONFIRM))
                {
                    OnConfirm(INPUT_TYPE.USE);
                }

                if (Input.GetKeyDown(KeyCodeConsts.CANCEL))
                {
                    OnCancel(INPUT_TYPE.CANCEL);
                }
            }

            if (MovementEnabled)
            {
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

        public void ToggleInteractions(bool state) => InteractionsEnabled = state;

        public void ToggleMovement(bool state) => MovementEnabled = state;

        public Vector2 GetFirectionVector2D() => Facing;

        public float GetCurrentMagnitude() => CurrentVelocity.magnitude;
    }
}