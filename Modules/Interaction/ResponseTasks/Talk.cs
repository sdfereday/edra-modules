using UnityEngine;
using RedPanda.Dialogue;
using RedPanda.UserInput;
using RedPanda.Entities;

namespace RedPanda.Interaction
{
    public class Talk : MonoBehaviour, IResponseTask
    {
        public ChatManager chatManager;
        public PlayerInput playerInput;

        public bool IsActive { get; private set; }
        public RESPONSE_TYPE ResponseType => RESPONSE_TYPE.CONTINUOUS;

        private void OnEnable() => ChatManager.OnConversationComplete += Complete;
        private void OnDisable() => ChatManager.OnConversationComplete -= Complete;

        public void Next() => chatManager.NextSentence();
        public void Complete()
        {
            playerInput.ToggleMovement(true);
            IsActive = false;
        }

        public void Run(INTERACTIBLE_TYPE originType, Transform originTransform)
        {
            // TODO: Maybe make the ID related to conversation, not just generic.
            string conversationStartPoint = originTransform.GetComponent<IIdentifier>().Identifier;

            // Disable movement on player whilst talking (or give auto control if cutscene)
            playerInput.ToggleMovement(false);
            IsActive = true;

            // Start the thing up (buttons will load after initial)
            chatManager.StartDialogue(conversationStartPoint);
        }
    }
}