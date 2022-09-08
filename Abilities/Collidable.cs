using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace _Main._Core.Scripts.Features.Abilities
{
    [Serializable]
    public class LayerCollisionEvent
    {
        public string layerName;
        public UnityEvent unityEvent;
    }

    public class Collidable : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_unityEventOnDestroy;
        [SerializeField] private List<LayerCollisionEvent> m_layerCollisionEvents;
        [SerializeField] private bool m_destroyOnCollision;
        [SerializeField] private float m_destroyAllowedAfterTime;
        private float m_startTime;

        private void Start()
        {
            m_startTime = Time.timeSinceLevelLoad;
        }

        private void OnCollisionEnter(Collision col)
        {
            var layerCollisionEvent = m_layerCollisionEvents.FirstOrDefault
            (
                lce => col.gameObject.layer == LayerMask.NameToLayer(lce.layerName)
            );

            if (null != layerCollisionEvent)
                layerCollisionEvent.unityEvent?.Invoke();

            else if (m_destroyOnCollision)
            {
                if
                (
                    m_destroyAllowedAfterTime <= 0 || 
                    m_destroyAllowedAfterTime > 0 && Time.timeSinceLevelLoad - m_startTime >= m_destroyAllowedAfterTime
                )
                    OnDestroyEvent();
            }
        }

        private void OnDestroyEvent()
        {
            m_unityEventOnDestroy?.Invoke();
            Destroy(gameObject);
        }
    }
}