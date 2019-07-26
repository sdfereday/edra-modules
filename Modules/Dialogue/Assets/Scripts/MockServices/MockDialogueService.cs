using System.Collections.Generic;
using RedPanda.Dialogue;

namespace RedPanda.MockServices
{
    public static class MockDialogueService
    {
        public static List<ChatNode> ChatNodes = new List<ChatNode>()
        {
            new ChatNode()
            {
                Id = "n1",
                To = "n2",
                Text = "Hello World!"
            },
            new ChatNode()
            {
                Id = "n2",
                To = "n3",
                Text = "Hello Back!"
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
                        Text = "I am choice 1"
                    },
                    new ChatNode()
                    {
                        Id = "choice2",
                        To = "n5",
                        Text = "I am choice 2"
                    }
                },
                Text = "I have some choices for you:"
            },
            new ChatNode()
            {
                Id = "n4",
                Text = "Goodbye from choice 1!",
                IsLast = true
            },
            new ChatNode()
            {
                Id = "n5",
                Text = "Goodbye from choice 2!",
                IsLast = true
            }
        };
    }
}