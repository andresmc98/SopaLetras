using Core.Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PetSystem
{
    public class PetInteraction : MonoBehaviour
    {
        [SerializeField] private EmotionStateMachine _emotionStateMachine;
        [SerializeField] private DialogueBubble _dialogueBubble;
        [SerializeField] private Camera _camera;

        private void OnEnable()
        {
            _emotionStateMachine.OnEmotionChanged += OnEmotionChanged;
        }

        private void OnDisable()
        {
            _emotionStateMachine.OnEmotionChanged -= OnEmotionChanged;
        }

        private void Update()
        {
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
            {
                Vector2 touchPos = Touchscreen.current.primaryTouch.position.ReadValue();
                CheckTap(touchPos);
            }

#if UNITY_EDITOR
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
                CheckTap(Mouse.current.position.ReadValue());
#endif
        }

        private void CheckTap(Vector2 screenPosition)
        {
            Vector3 worldPos = _camera.ScreenToWorldPoint(
                new Vector3(screenPosition.x, screenPosition.y, _camera.nearClipPlane));

            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                EventBus.Publish(new PetTappedEvent());
                _dialogueBubble.ShowForEmotion(PetEmotion.Happy);
            }
        }

        private void OnEmotionChanged(PetEmotion emotion)
        {
            if (emotion == PetEmotion.Idle) return;
            _dialogueBubble.ShowForEmotion(emotion);
        }
    }
}