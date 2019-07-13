using System.Collections.Generic;
using RedPanda.Dialogue;

namespace RedPanda.Storage
{
    public static class ConversationStub
    {
        // The actions here MUST match the internals set on chat iterator.
        public static List<ChatNode> Collection = new List<ChatNode>() {
        new ChatNode() {
            Id = "m1",
            From = null,
            To = null,
            Text = "This is the first message.",
            Choices = new List<ChatNode>() {
                new ChatNode() {
                    Id = "m1a",
                    From = "m1",
                    To = "m2",
                    Text = "I will select A.",
                    Choices = new List<ChatNode>(),
                    Actions = new List<string>()
                },
                new ChatNode() {
                    Id = "m1b",
                    From = "m1",
                    To = "m3",
                    Text = "I will select B.",
                    Choices = new List<ChatNode>(),
                    Actions = new List<string>()
                }
            },
            Actions = new List<string>()
        },
        new ChatNode() {
            Id = "m2",
            From = "m1a",
            To = "m4",
            Text = "This is if you select A.",
            Choices = new List<ChatNode>(),
            Actions = new List<string>()
        },
        new ChatNode() {
            Id = "m3",
            From = "m1b",
            To = null,
            Text = "This is if you select B.",
            Choices = new List<ChatNode>(),
            Actions = new List<string>() {
                "cancel"
            }
        },
        new ChatNode() {
            Id = "m4",
            From = "m2",
            To = null,
            Text = "This should be the last in the chain for A.",
            Choices = new List<ChatNode>(),
            Actions = new List<string>() {
                "endConversation",
                "save"
            }
        }
    };
    }
}