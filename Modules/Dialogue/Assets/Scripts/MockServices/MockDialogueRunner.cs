using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using RedPanda.Dialogue;

namespace RedPanda.MockServices
{
    public class MockDialogueRunner : MonoBehaviour
    {
        private string FirstNodeId = "n1";
        private ChatManager Chat;
        private List<ChatNode> LoadedChatNodes;
        private MockEntity[] MockEntities;
        private List<ChatNode> ChatNodeData;

        private void OnEnable() => ChatManager.OnNext += OnNextChatNode;
        private void OnDisable() => ChatManager.OnNext -= OnNextChatNode;

        private string ParseName(string actorId)
        {
            MockEntity currentActor = MockEntities.FirstOrDefault(y => y.Id == actorId);
            return currentActor ? currentActor.Name : "{ Undefined }";
        }

        private string ParseText(string original, string[] textParams)
        {
            string[] namesFromParams = textParams != null ? textParams
                .Select(id =>
                {
                    MockEntity current = MockEntities.FirstOrDefault(y => y.Id == id);
                    return current != null ? current.Name : "{ Undefined }";
                })
                .ToArray() : new string[] { "{ Undefined }" };

            return string.Format(original, namesFromParams);
        }

        private void Start()
        {
            // Not the most performent thing, but it works (it won't work efficiently
            // in a game with lots of npc's though, you need to get what you need,
            // maybe put the id's in the chat data at the top?)
            MockEntities = FindObjectsOfType<MockEntity>();
            Chat = GetComponent<ChatManager>();

            // Bootstrap the new conversation (again implementation may vary)
            ChatNodeData = new List<ChatNode>(MockDialogueService.ChatNodes).Select(node =>
            {
                node.ActorName = ParseName(node.ActorId);
                node.Text = ParseText(node.Text, node.TextParams);

                if (node.Choices != null) {
                    node.Choices
                        .ForEach(subNode =>
                        {
                            subNode.ActorName = ParseName(node.ActorId);
                            subNode.Text = ParseText(subNode.Text, subNode.TextParams);
                        });
                }

                return node;
            }).ToList();
        }

        private void Update()
        {
            if (Input.GetKeyDown("space") && !Chat.IsActive)
            {
                Chat.StartDialogue(FirstNodeId, ChatNodeData);
            }
        }

        private void OnNextChatNode(ChatNode nodeData)
        { }
    }
}