using Core.Events;
using UnityEngine;

namespace PetSystem
{
    public class EmotionStateMachine : MonoBehaviour
    {
        [SerializeField] private float _thinkingTimeout = 10f;
        
        private PetEmotion _currentEmotion = PetEmotion.Idle;
        private float _lastActivityTime;
        
        public PetEmotion CurrentEmotion => _currentEmotion;
        
        public System.Action<PetEmotion> OnEmotionChanged;

        private void OnEnable()
        {
            EventBus.Subscribe<WordFoundEvent>(OnWordFound);
            EventBus.Subscribe<LevelCompleteEvent>(OnLevelComplete);
            EventBus.Subscribe<PetTappedEvent>(OnPetTapped);
            _lastActivityTime = Time.time;
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<WordFoundEvent>(OnWordFound);
            EventBus.Unsubscribe<LevelCompleteEvent>(OnLevelComplete);
            EventBus.Unsubscribe<PetTappedEvent>(OnPetTapped);
        }

        private void Update()
        {
            if (_currentEmotion != PetEmotion.Idle) return;
            if (Time.time - _lastActivityTime > _thinkingTimeout)
                SetEmotion(PetEmotion.Thinking);
        }

        private void OnWordFound(WordFoundEvent e)
        {
            _lastActivityTime = Time.time;
            SetEmotion(PetEmotion.Happy);
            Invoke(nameof(ReturnToIdle), 2f);
        }

        private void OnLevelComplete(LevelCompleteEvent e)
        {
            SetEmotion(PetEmotion.Excited);
        }

        private void OnPetTapped(PetTappedEvent e)
        {
            _lastActivityTime = Time.time;
            SetEmotion(PetEmotion.Happy);
            Invoke(nameof(ReturnToIdle), 2f);
        }

        private void SetEmotion(PetEmotion emotion)
        {
            if (_currentEmotion == emotion) return;
            _currentEmotion = emotion;
            OnEmotionChanged?.Invoke(_currentEmotion);
        }

        private void ReturnToIdle()
        {
            SetEmotion(PetEmotion.Idle);
            _lastActivityTime = Time.time;
        }
    }
}