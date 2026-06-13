using UnityEngine;

namespace PetSystem
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private EmotionStateMachine _emotionStateMachine;

        private static readonly int IdleHash = Animator.StringToHash("Idle");
        private static readonly int HappyHash = Animator.StringToHash("Happy");
        private static readonly int ExcitedHash = Animator.StringToHash("Excited");
        private static readonly int ThinkingHash = Animator.StringToHash("Thinking");

        private void OnEnable()
        {
            _emotionStateMachine.OnEmotionChanged += OnEmotionChanged;
        }

        private void OnDisable()
        {
            _emotionStateMachine.OnEmotionChanged -= OnEmotionChanged;
        }

        private void OnEmotionChanged(PetEmotion emotion)
        {
            if (_animator == null) return;

            _animator.ResetTrigger(IdleHash);
            _animator.ResetTrigger(HappyHash);
            _animator.ResetTrigger(ExcitedHash);
            _animator.ResetTrigger(ThinkingHash);

            switch (emotion)
            {
                case PetEmotion.Idle:
                    _animator.SetTrigger(IdleHash);
                    break;
                case PetEmotion.Happy:
                    _animator.SetTrigger(HappyHash);
                    break;
                case PetEmotion.Excited:
                    _animator.SetTrigger(ExcitedHash);
                    break;
                case PetEmotion.Thinking:
                    _animator.SetTrigger(ThinkingHash);
                    break;
            }
        }
    }
}