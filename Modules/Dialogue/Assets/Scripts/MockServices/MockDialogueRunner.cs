using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using RedPanda.Dialogue;

namespace RedPanda.MockServices
{
    public class MockDialogueRunner : MonoBehaviour
    {
        private ChatManager Chat;
        private string LoadedChatStart = "n1";
        private List<ChatNode> LoadedChatNodes;
        private MockEntity[] MockEntities;

        private void OnEnable() => ChatManager.OnNext += OnNextChatNode;
        private void OnDisable() => ChatManager.OnNext -= OnNextChatNode;

        private void Start()
        {
            // Not the most performent thing, but it works (it won't work efficiently
            // in a game with lots of npc's though, you need to get what you need,
            // maybe put the id's in the chat data at the top?)
            MockEntities = FindObjectsOfType<MockEntity>();
            Chat = GetComponent<ChatManager>();
        }

        private void Update()
        {
            if (Input.GetKeyDown("space") && !Chat.IsActive)
            {
                Chat.StartDialogue(LoadedChatStart, MockDialogueService.ChatNodes, MockDialogueService
                    .ChatActors
                    .Select(ActorId => MockEntities.FirstOrDefault(y => y.Id == ActorId).Name)
                    .ToArray());
            }
        }

        private void OnNextChatNode(ChatNode nodeData)
        {
            Chat.SetNameField(MockEntities.FirstOrDefault(y => y.Id == nodeData.ActorId).Name);
        }
    }
}