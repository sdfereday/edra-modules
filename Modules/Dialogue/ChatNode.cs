using System.Collections.Generic;

namespace RedPanda.Dialogue
{
    [System.Serializable]
    public class ChatNode
    {
        public string Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }

        public List<ChatNode> Choices { get; set; }
        public List<string> Actions { get; set; }

        public bool HasOrigin => From != null;
        public bool HasRoute => To != null;
        public bool HasChoices => Choices.Count > 0;
        public bool HasActions => Actions.Count > 0;
        public bool IsLast { get; set; }
    }
}