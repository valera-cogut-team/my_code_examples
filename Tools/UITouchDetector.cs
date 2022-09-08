using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Main._Core.Scripts.Tools
{
    public class UITouchDetector
    {
        public bool UITouchDetected()
        {
            return Input.touchCount > 0 ? 
                Input.touches.Select(touch => touch.fingerId).Any(id => EventSystem.current.IsPointerOverGameObject(id)) : 
                EventSystem.current.IsPointerOverGameObject();
        }
    }
}