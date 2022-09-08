using UnityEngine;
using UnityEngine.EventSystems;

namespace _Main._Core.Scripts.Tools
{
    public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private float m_dampingSpeed = 0.05f;
        private RectTransform m_draggingObjectRectTransform;
        private Vector3 m_velocity = Vector3.zero;

        private void Awake()
        {
            m_draggingObjectRectTransform = transform as RectTransform;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (
                RectTransformUtility.ScreenPointToWorldPointInRectangle
                (
                    m_draggingObjectRectTransform, 
                    eventData.position, 
                    eventData.pressEventCamera, 
                    out var globalMousePosition
                )
            )
            {
                m_draggingObjectRectTransform.position = Vector3.SmoothDamp
                (
                    m_draggingObjectRectTransform.position,
                    globalMousePosition, 
                    ref m_velocity, 
                    m_dampingSpeed
                );
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
        }
    }
}