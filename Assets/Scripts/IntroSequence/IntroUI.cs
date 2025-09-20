using System;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace IntroSequence
{
    public class IntroUI : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private GameObject choiceButtonPrefab;
        [SerializeField] private GameObject buttonPanel;
        private VerticalLayoutGroup layoutGroup;
        
        [Header("Setup")]
        [SerializeField] private TextAsset inkAsset;

        private TextRenderer textRenderer;
        
        Story story;
        private bool storyReady = false;
        private bool isInChoices = false;
        private string lastStoryLine;
        private readonly int elementPadding = 1;
    
        void Awake()
        {
            story = new Story(inkAsset.text);
            textRenderer = GetComponent<TextRenderer>();
            layoutGroup = buttonPanel.GetComponent<VerticalLayoutGroup>();
            GameManager.onAdvanceDialogue += RequestContinueStory;
        }

        private void OnDestroy()
        {
            GameManager.onAdvanceDialogue -= RequestContinueStory;
        }

        private void Start()
        {
            text.text = "";
            ContinueStory();
        }

        private void Update()
        {
            if (!storyReady) return;
            
            storyReady = false;
            float offset = 0;
            
            // If currently rendering text
            if (textRenderer.IsRendering)
            {
                text.text = lastStoryLine;
                textRenderer.isEnabled = false;
            }

            else
            {
                // If there's more story, show the next line
                if (story.canContinue)
                {
                    text.gameObject.SetActive(true);
                    //outline.SetActive(false);
                    
                    textRenderer.isEnabled = true;
                    lastStoryLine = story.Continue();
                    StartCoroutine(textRenderer.TypewriterEffect(lastStoryLine, text));
                }

                // No more lines, but there's now choices to be made
                else if (story.currentChoices.Count > 0)
                {
                    isInChoices = true;
                    text.gameObject.SetActive(false);
                    //outline.SetActive(true);
                    
                    for (int i = 0; i < story.currentChoices.Count; ++i)
                    {
                        Choice choice = story.currentChoices[i];

                        GameObject choiceButtonObject = Instantiate(choiceButtonPrefab, buttonPanel.transform, false);
                        Button choiceButton = choiceButtonObject.GetComponent<Button>();

                        choiceButtonObject.transform.Translate(new Vector2(0, offset));

                        TextMeshProUGUI choiceText = choiceButtonObject.GetComponentInChildren<TextMeshProUGUI>();
                        choiceText.text = choice.text;

                        int choiceId = i;
                        choiceButton.onClick.AddListener(delegate { ChoiceSelected(choiceId); });

                        offset -= (choiceText.fontSize + layoutGroup.padding.top + layoutGroup.padding.bottom +
                                   elementPadding);
                    }
                }

                // The end of the story
                else
                {
                    EndDialogue();
                }
            }
        }
        
        private void ChoiceSelected(int id)
        {
            isInChoices = false;
            story.ChooseChoiceIndex(id);
            ContinueStory();
            
            // Queue all buttons for deletion
            RemoveChoices();
        }
        
        public void ContinueStory()
        {
            storyReady = true;
        }

        private void RequestContinueStory()
        {
            if (isInChoices) return;
            ContinueStory();
        }

        private void RemoveChoices()
        {
            int childCount = buttonPanel.transform.childCount;
            for (int i = childCount - 1; i >= 0; --i)
            {
                Destroy(buttonPanel.transform.GetChild(i).gameObject);
            }
        }

        private void EndDialogue()
        {
            
        }
        
    }
}
