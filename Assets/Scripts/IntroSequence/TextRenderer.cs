using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

namespace IntroSequence
{
    [RequireComponent(typeof(AudioSource))]
    public class TextRenderer : MonoBehaviour
    {
        public bool IsRendering { get; private set; } = false;
        public bool isEnabled { get; set; } = false;
        
        [SerializeField] private float timeBetweenLetters = 0.05f;
        
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public IEnumerator TypewriterEffect(string textToDisplay, TextMeshProUGUI textObject)
        {
            IsRendering = true;

            textObject.text = "";

            foreach (char character in textToDisplay.TakeWhile(character => isEnabled))
            {
                float time = character == ' ' ? timeBetweenLetters * 1.5f : timeBetweenLetters; 
                textObject.text += character;
                audioSource.Play();
                yield return new WaitForSecondsRealtime(time);
            }

            IsRendering = false;
        }
    }
}