using UnityEngine;
using RedPanda.Dialogue;

namespace RedPanda.MockServices
{
    public class MockDialogueRunner : MonoBehaviour
    {
        private ChatManager Chat;

        private void Start()
        {
            Chat = GetComponent<ChatManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown("space") && !Chat.IsActive)
            {
                Chat.StartDialogue("n1", MockDialogueService.ChatNodes);
            }
        }
    }
}