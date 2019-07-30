using System.Collections.Generic;
using RedPanda.Dialogue;

namespace RedPanda.MockServices
{
    public static class MockDialogueService
    {
        /* Nodes could easily have a dialogue editor built for them. */
        public static List<ChatNode> ChatNodes = new List<ChatNode>()
        {
            new ChatNode()
            {
                Id = "n1",
                To = "n2",
                Text = "Hello {0}!",
                ActorId = "playerId",
                TextParams = new string[] {
                    "npcId"
                }
            },
            new ChatNode()
            {
                Id = "n2",
                To = "n3",
                Text = "Hello back {0}!",
                ActorId = "npcId",
                TextParams = new string[] {
                    "playerId"
                }
            },
            new ChatNode()
            {
                Id = "n3",
                Choices = new List<ChatNode>()
                {
                    new ChatNode()
                    {
                        Id = "choice1",
                        To = "n4",
                        Text = "I think I've had enough.",
                        TextParams = new string[] { }
                    },
                    new ChatNode()
                    {
                        Id = "choice2",
                        To = "n1",
                        Text = "Wait, let's start over {0}!",
                        TextParams = new string[] {
                            "npcId"
                        }
                    }
                },
                Text = "I have some choices for you {0}. Would you consider visiting {1}?",
                ActorId = "npcId",
                TextParams = new string[] {
                    "playerId",
                    "locationId"
                }
            },
            new ChatNode()
            {
                Id = "n4",
                Text = "Goodbye from choice 1, {0}!",
                ActorId = "playerId",
                IsLast = true,
                TextParams = new string[] {
                    "npcId"
                }
            }
        };
    }
}