using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace RedPanda.Dialogue
{
    // How do we know who is speaking?
    public class ChatManager : MonoBehaviour
    {
        public delegate void CompleteAction();
        public static event CompleteAction OnConversationComplete;

        public delegate void NextAction(ChatNode nodeData);
        public static event NextAction OnNext;

        public GameObject DialogueBox;
        public Text NameField;
        public Text DialogueField;
        public GameObject ButtonContainer;
        public GameObject NextButtonPrefab;
        public GameObject ChoiceButtonPrefab;

        private ChatIterator chatIterator;
        private bool WaitingForChoices { get; set; }
        public bool IsActive { get; private set; }
        public bool ExitScheduled { get; private set; }

        // TODO: Set up a simple animation slide in, as this is a bit crap.
        private void Awake()
        {
            DialogueBox.SetActive(false);
            ClearButtons();
        }

        private void ClearButtons()
        {
            if (ButtonContainer.transform.childCount > 0)
            {
                foreach (Transform child in ButtonContainer.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        private IEnumerator TypeSentence(ChatNode node)
        {
            OnNext?.Invoke(node);

            NameField.text = node.ActorName;
            DialogueField.text = "";

            foreach (char letter in node.Text.ToCharArray())
            {
                DialogueField.text += letter;
                yield return null;
            }

            if (node.HasChoices)
            {
                WaitingForChoices = true;
                // Load choices available
                Debug.Log("Choices available: " + node.Choices.Count);

                // TODO: You'll have to somehow pass things with the nodes here. Perhaps make
                // a small class to pass, or, some sort of event listener?
                // Or however many you need...
                node.Choices.ForEach(choice =>
                {
                    GameObject ButtonObj = Instantiate(ChoiceButtonPrefab, ButtonContainer.transform.position, Quaternion.identity, ButtonContainer.transform);

                    ButtonObj.transform.Find("Text").GetComponent<Text>()
                        .text = choice.Text;

                    ButtonObj.GetComponent<Button>()
                        .onClick.AddListener(() =>
                        {
                            WaitingForChoices = false;
                            Next(choice.To);
                        });
                });
            }
            else if(NextButtonPrefab != null)
            {
                GameObject ButtonObj = Instantiate(NextButtonPrefab, ButtonContainer.transform.position, Quaternion.identity, ButtonContainer.transform);

                ButtonObj.transform.Find("Text").GetComponent<Text>()
                        .text = "Next";

                ButtonObj.GetComponent<Button>()
                    .onClick.AddListener(() => Next());
            }
        }

        private void OnChatComplete()
        {
            IsActive = false;
            ExitScheduled = false;
            DialogueBox.SetActive(IsActive);
            OnConversationComplete?.Invoke();
        }

        public void StartDialogue(string startChatId, List<ChatNode> chatData)
        {
            List<ChatNode> parsedChat = new List<ChatNode>(chatData);          
            chatIterator = new ChatIterator(parsedChat);

            IsActive = true;
            DialogueBox.SetActive(IsActive);
            Next(startChatId);
        }

        public void Next(string nodeId = null)
        {
            if (WaitingForChoices || !IsActive) return;

            if (ExitScheduled)
            {
                OnChatComplete();
                return;
            }

            ClearButtons();

            ChatNode node = chatIterator.GoToNext(nodeId);

            if (node == null)
            {
                Debug.LogError("Chat quit unexpectedly. Couldn't find a node to display.");
                OnChatComplete();
                return;
            }

            Debug.Log(node.Text);
            DialogueField.text = node.Text;

            ExitScheduled = node.IsLast;

            StopAllCoroutines();
            StartCoroutine(TypeSentence(node));
        }
        
        public void SetNameField(string name) => NameField.text = name;
    }
}