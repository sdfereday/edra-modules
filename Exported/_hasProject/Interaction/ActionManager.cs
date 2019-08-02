using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// TODO: Consider making other entities work in the same way to this so it all matches, rather than just having
// different scripts for each entity type, they could just have their own set of responses.
namespace RedPanda.Interaction
{
    public class ActionManager : MonoBehaviour
    {
        [System.Serializable]
        public class Response : PropertyAttribute
        {
            public string Name;
            public INTERACTIBLE_TYPE InteractibleType;
            public MonoBehaviour ResponseTrigger;
            private IResponseTask Task { get; set; }

            public void Init() => Task = ResponseTrigger.GetComponent<IResponseTask>();
            public void Activate(INTERACTIBLE_TYPE originType, Transform originTransform) => Task.Run(originType, originTransform);

            public void Update(INTERACTIBLE_TYPE originType, Transform originTransform)
            {
                if (Task.ResponseType == RESPONSE_TYPE.CONTINUOUS)
                    Task.Next();

                if (Task.ResponseType == RESPONSE_TYPE.TOGGLE)
                    Task.Complete();
            }

            public bool ContainsUnfinished() => Task.IsActive;
        }

        public List<Response> ResponseList;
        public bool ResponseMustFinish { get; private set; }

        private void Start() => ResponseList.ForEach(r => r.Init());

        public void Act(INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            var matchedResponse = ResponseList.Find(r => r.InteractibleType == originType);
            ResponseMustFinish = ResponseList.Any(r => r.ContainsUnfinished());

            if (matchedResponse == null)
                return;

            if (ResponseMustFinish)
            {
                matchedResponse.Update(originType, originTransform);
            }
            else
            {
                matchedResponse.Activate(originType, originTransform);
            }
        }
    }
}