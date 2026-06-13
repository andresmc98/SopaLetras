using TMPro;
using UnityEngine;

namespace PetSystem
{
    public class DialogueBubble : MonoBehaviour
    {
        [SerializeField] private GameObject _bubbleRoot;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private float _displayDuration = 3f;
        
        private readonly string[] _happyPhrases =
        {
            "¡Excelente!",
            "¡Muy bien!",
            "¡Encontraste una!",
            "¡Genial!"
        };

        private readonly string[] _thinkingPhrases =
        {
            "Hmm... ¿necesitas ayuda?",
            "Busca palabras horizontales",
            "Intenta buscar verticalmente"
        };

        private readonly string[] _excitedPhrases =
        {
            "¡¡INCREÍBLE!!",
            "¡Lo lograste!",
            "¡Completaste el nivel!"
        };

        private readonly string[] _idlePhrases =
        {
            "¡Hola!",
            "¿Listo para jugar?",
            "¡Encuentra todas las palabras!"
        };

        private void OnEnable()
        {
            _bubbleRoot.SetActive(false);
        }

        public void ShowForEmotion(PetEmotion emotion)
        {
            string phrase = emotion switch
            {
                PetEmotion.Happy => GetRandom(_happyPhrases),
                PetEmotion.Thinking => GetRandom(_thinkingPhrases),
                PetEmotion.Excited => GetRandom(_excitedPhrases),
                PetEmotion.Idle => GetRandom(_idlePhrases),
                _ => ""
            };
            Show(phrase);
        }

        private void Show(string text)
        {
            _dialogueText.text = text;
            _bubbleRoot.SetActive(true);
            CancelInvoke(nameof(Hide));
            Invoke(nameof(Hide), _displayDuration);
        }

        private void Hide()
        {
            _bubbleRoot.SetActive(false);
        }

        private string GetRandom(string[] phrases)
        {
            return phrases[Random.Range(0, phrases.Length)];
        }
    }
}