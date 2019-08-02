using UnityEngine;
using RedPanda.Effects;
using RedPanda.Utils;
using RedPanda.Entities;

namespace RedPanda.Interaction
{
    public class Carry : MonoBehaviour, IResponseTask
    {
        private ICarryable carryable;
        private Transform objectToCarry;
        private SortZ sortZ;
        private SpriteRenderer spr;
        private Throw throwAction;

        public Vector2 offset = Vector2.zero;
        public bool IsActive { get; private set; }
        public RESPONSE_TYPE ResponseType
        {
            get
            {
                return RESPONSE_TYPE.TOGGLE;
            }
        }

        private void Awake()
        {
            sortZ = GetComponent<SortZ>();
            spr = GetComponent<SpriteRenderer>();
            throwAction = GetComponent<Throw>();
        }

        private void Update()
        {
            if (IsActive)
            {
                objectToCarry.position = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);
                // gameObject.transform.Translate (newpos.x, newpos.y, Time.deltaTime);
                // ^- Use if you want some sort of actual transition (perhaps pushing or pulling).
            }
        }

        public void Run(INTERACTIBLE_TYPE originType, Transform _objectToCarry)
        {
            objectToCarry = _objectToCarry;
            carryable = objectToCarry.GetComponent<ICarryable>();

            IsActive = true;
        }

        public void Complete()
        {
            objectToCarry.position = new Vector2(transform.position.x + offset.x, transform.position.y);
            IsActive = false;

            if (throwAction != null)
            {
                throwAction.StartThrow(objectToCarry);
            }
        }

        public void Next()
        {
            // ...
        }
    }
}