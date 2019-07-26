using System.Collections.Generic;
using RedPanda.Dialogue;

namespace RedPanda.MockServices
{
    public static class MockDialogueService
    {
        public static string[] ChatActors = new string[] { "player", "dave" };

        public static List<ChatNode> ChatNodes = new List<ChatNode>()
        {
            new ChatNode()
            {
                Id = "n1",
                To = "n2",
                Text = "Hello {targetActorId}!",
                ActorId = "player"
            },
            new ChatNode()
            {
                Id = "n2",
                To = "n3",
                Text = "Hello back {targetActorId}!",
                ActorId = "dave"
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
                        Text = "I think I've had enough."
                    },
                    new ChatNode()
                    {
                        Id = "choice2",
                        To = "n1",
                        Text = "Wait, let's start over!"
                    }
                },
                Text = "I have some choices for you:",
                ActorId = "dave"
            },
            new ChatNode()
            {
                Id = "n4",
                Text = "Goodbye from choice 1!",
                ActorId = "player",
                IsLast = true
            }
        };
    }
}