using System;
using System.Linq;
using System.Collections.Generic;
using RedPanda.Utils;

namespace RedPanda.Dialogue
{
    public class ChatIterator
    {
        private const string EndConversationAction = "endConversation";
        private const string SaveConversationAction = "save";
        private const string CancelConversationAction = "cancel";

        public string ChainPosition { get; private set; }
        private List<ChatNode> Collection { get; set; }
        private ChatNode CurrentNode { get; set; }
        private Action<string> OnChatComplete { get; set; }
        private Queue<ChatNode> ChatQueue { get; set; }

        public ChatIterator(List<ChatNode> _collection)
        {
            Collection = new List<ChatNode>(_collection);
            ChatQueue = new Queue<ChatNode>();
        }

        private bool NodeDataNotValid(ChatNode node) => node.To == null && !node.HasChoices && !node.Actions.Any(action => action == EndConversationAction);
        private bool NodeDataConflict(ChatNode node) => node.To != null && node.HasChoices;

        private bool ValidateNode(ChatNode node)
        {
            // TODO: Consts please for these errors.
            if (node == null)
            {
                Log.Out("There was a problem finding a node. Try running 'start' first.");
                return false;
            }

            if (NodeDataNotValid(node))
            {
                Log.Out("The current node is invalid. It must have a 'to' OR 'choices', or, an 'endConversation' action if this was intended.");
                return false;
            }

            if (NodeDataConflict(node))
            {
                Log.Out("The current node is invalid. It must have either 'to' OR 'choices', and not both.");
                return false;
            }

            return true;
        }

        private ChatNode QueryNode(string query)
        {
            // This will never (at present) scan for choice nodes (may not ever need to).
            ChatNode nextNode = Collection.FirstOrDefault(node => node.Id == query);
            return ValidateNode(nextNode) ? nextNode : null;
        }

        private ChatNode PreviousNode;
        public ChatNode GoToNext(string query = null)
        {
            if (ChatQueue.Count == 0 && query == null)
            {
                Log.Out("This seems to be the entry call for the conversation. You must pass a query to it.");
                return null;
            }

            ChatNode CurrentNode = query != null ? QueryNode(query) : ChatQueue.Dequeue();

            if (CurrentNode == null) return null;

            if (CurrentNode.HasRoute)
            {
                ChatNode NextNode = QueryNode(CurrentNode.To);
                ChatQueue.Enqueue(NextNode);
            }

            if (CurrentNode.HasActions && CurrentNode.Actions.Any(action => action == EndConversationAction))
            {
                if (CurrentNode.Actions.Any(action => action == SaveConversationAction))
                {
                    // ... onSave, etc
                    ChainPosition = CurrentNode.Id;
                    Log.Out("Saved chain up to ID.");
                }

                if (CurrentNode.Actions.Any(action => action == CancelConversationAction))
                {
                    // ... onCancel, etc
                    Log.Out("Cancelled chain, nothing saved.");
                }

                CurrentNode.IsLast = true;
            }

            return CurrentNode;
        }
    }
}