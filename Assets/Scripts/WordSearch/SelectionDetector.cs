using System.Collections.Generic;
using Core.Events;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace WordSearch
{
    public class SelectionDetector : MonoBehaviour
    {
        [SerializeField] private GridGenerator _gridGenerator;
        [SerializeField] private WordValidator _wordValidator;
        [SerializeField] private Camera _camera;
        
        private List<GridCell> _selectedCells = new List<GridCell>();
        private bool _isSelecting;

        private void OnEnable()
        {
            EnhancedTouchSupport.Enable();
            Touch.onFingerDown += OnFingerDown;
            Touch.onFingerMove += OnFingerMove;
            Touch.onFingerUp += OnFingerUp;
        }
        
        private void OnDisable()
        {
            Touch.onFingerDown -= OnFingerDown;
            Touch.onFingerMove -= OnFingerMove;
            Touch.onFingerUp -= OnFingerUp;
            EnhancedTouchSupport.Disable();
        }

        private void OnFingerDown(Finger finger)
        {
            GridCell cell = GetCellAtPosition(finger.screenPosition);
            if (cell == null) return;
            
            _isSelecting = true;
            _selectedCells.Clear();
            AddCell(cell);
            
            EventBus.Publish(new SelectionStartedEvent(new Vector2Int(cell.Row, cell.Col)));
        }

        private void OnFingerMove(Finger finger)
        {
            if (!_isSelecting) return;
            
            GridCell cell = GetCellAtPosition(finger.screenPosition);
            if (cell == null || _selectedCells.Contains(cell)) return;

            AddCell(cell);
        }

        private void OnFingerUp(Finger finger)
        {
            if (!_isSelecting) return;
            
            _isSelecting = false;
            EventBus.Publish(new SelectionEndedEvent());
            _wordValidator.Validate(_selectedCells);

            ClearSelection();
        }

        private void AddCell(GridCell cell)
        {
            _selectedCells.Add(cell);
            cell.SetHighlight(true);
        }

        private void ClearSelection()
        {
            foreach (var cell in _selectedCells)
                cell.SetHighlight(false);
            _selectedCells.Clear();
        }

        private GridCell GetCellAtPosition(Vector2 screenPosition)
        {
            Vector3 worldPos = _camera.ScreenToWorldPoint(new Vector3(screenPosition.x, 
                                screenPosition.y, _camera.nearClipPlane));
            
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider == null) return null;
            
            return hit.collider.GetComponent<GridCell>();
        }
        
#if UNITY_EDITOR
        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                GridCell cell = GetCellAtPosition(UnityEngine.Input.mousePosition);
                if (cell == null) return;
                _isSelecting = true;
                _selectedCells.Clear();
                AddCell(cell);
                EventBus.Publish(new SelectionStartedEvent(new Vector2Int(cell.Row, cell.Col)));
            }

            if (UnityEngine.Input.GetMouseButton(0) && _isSelecting)
            {
                GridCell cell = GetCellAtPosition(UnityEngine.Input.mousePosition);
                if (cell != null && !_selectedCells.Contains(cell))
                    AddCell(cell);
            }

            if (UnityEngine.Input.GetMouseButtonUp(0) && _isSelecting)
            {
                _isSelecting = false;
                EventBus.Publish(new SelectionEndedEvent());
                _wordValidator.Validate(_selectedCells);
                ClearSelection();
            }
        }
#endif
    }
}