using System.Collections.Generic;

namespace RedPanda.Dialogue
{
    [System.Serializable]
    public class ChatNode
    {
        /// <summary>
        /// Set in data structures
        /// </summary>
        public string Id { get; set; }
        public string ActorId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
        public bool IsLast { get; set; }
        public List<ChatNode> Choices { get; set; }
        public List<string> Actions { get; set; }

        /// <summary>
        /// Can be populated externally or set in data, up to you
        /// </summary>
        public string ActorName { get; set; }

        /// <summary>
        /// Simple statuses for this node
        /// </summary>
        public bool HasOrigin => From != null;
        public bool HasRoute => To != null;
        public bool HasChoices => Choices != null ? Choices.Count > 0 : false;
        public bool HasActions => Actions != null ? Actions.Count > 0 : false;
    }
}