using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using RedPanda.Storage;

namespace RedPanda.Dialogue
{
    public class ChatManager : MonoBehaviour
    {
        public delegate void CompleteAction();
        public static event CompleteAction OnConversationComplete;

        public GameObject DialogueBox;
        public Text NameField;
        public Text DialogueField;
        public GameObject NextButton;
        public GameObject ButtonContainer;
        public GameObject ChoiceButtonPrefab;

        private ChatIterator chatIterator;
        private bool WaitingForChoices { get; set; }
        public bool IsActive { get; private set; }
        public bool ExitScheduled { get; private set; }

        // TODO: Set up a simple animation slide in, as this is a bit crap.
        private void Awake()
        {
            DialogueBox.SetActive(false);
            NextButton.GetComponent<Button>()
                .onClick.AddListener(() => NextSentence());
        }

        private IEnumerator TypeSentence(ChatNode node)
        {
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
                Debug.Log(node.Choices.Count);

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
                            NextSentence(choice.To);
                        });
                });
            }
            else
            {
                NextButton.SetActive(true);
            }
        }

        public void StartDialogue(string startPoint)
        {
            chatIterator = new ChatIterator(ConversationStub.Collection);

            IsActive = true;
            DialogueBox.SetActive(IsActive);
            NextSentence(startPoint);
        }

        public void NextSentence(string specificPoint = null)
        {
            if (WaitingForChoices || !IsActive) return;

            if (ExitScheduled)
            {
                OnChatComplete();
                return;
            }

            NextButton.SetActive(false);

            if (ButtonContainer.transform.childCount > 0)
            {
                foreach (Transform child in ButtonContainer.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            ChatNode node = chatIterator.GoToNext(specificPoint);

            if (node == null)
            {
                Debug.LogError("Chat quit unexpectedly.");
                OnChatComplete();
                return;
            }

            Debug.Log(node.Text);
            DialogueField.text = node.Text;

            ExitScheduled = node.IsLast;

            StopAllCoroutines();
            StartCoroutine(TypeSentence(node));
        }

        public void OnChatComplete()
        {
            IsActive = false;
            ExitScheduled = false;
            DialogueBox.SetActive(IsActive);
            OnConversationComplete?.Invoke();
        }
    }
}