using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace _Main._Core.Scripts.Tools
{
    [Serializable]
    public class PhysicsTarget
    {
        public enum PhysicsMethod
        {
            None,
            OnTriggerEnter,
            OnTriggerStay,
            OnTriggerExit,
            OnCollisionEnter,
            OnCollisionStay,
            OnCollisionExit,
            OnTriggerEnter2D,
            OnTriggerStay2D,
            OnTriggerExit2D,
            OnCollisionEnter2D,
            OnCollisionStay2D,
            OnCollisionExit2D,
        }
        public GameObject target;
        public UnityEvent unityEvent;
        public List<PhysicsMethod> physicsMethods;
    }

    public class PhysicsActions : MonoBehaviour
    {
        [SerializeField] private List<PhysicsTarget> m_physicsTargets;

        #region 3D
        private void OnTriggerEvent(Collider other, PhysicsTarget.PhysicsMethod physicsMethod)
        {
            var list = m_physicsTargets.Where(physicsTarget => physicsTarget.physicsMethods.Contains(physicsMethod));

            foreach (var physicsTarget in list)
            {
                physicsTarget.unityEvent?.Invoke();
                physicsTarget.target.SendMessage(physicsMethod.ToString(), other, SendMessageOptions.DontRequireReceiver);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEvent(other, PhysicsTarget.PhysicsMethod.OnTriggerEnter);
        }

        private void OnTriggerStay(Collider other)
        {
            OnTriggerEvent(other, PhysicsTarget.PhysicsMethod.OnTriggerStay);
        }

        private void OnTriggerExit(Collider other)
        {
            OnTriggerEvent(other, PhysicsTarget.PhysicsMethod.OnTriggerExit);
        }

        private void OnCollisionEvent(Collision other, PhysicsTarget.PhysicsMethod physicsMethod)
        {
            var list = m_physicsTargets.Where(physicsTarget => physicsTarget.physicsMethods.Contains(physicsMethod));

            foreach (var physicsTarget in list)
            {
                physicsTarget.unityEvent?.Invoke();
                physicsTarget.target.SendMessage(physicsMethod.ToString(), other);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            OnCollisionEvent(other, PhysicsTarget.PhysicsMethod.OnCollisionEnter);
        }

        private void OnCollisionStay(Collision other)
        {
            OnCollisionEvent(other, PhysicsTarget.PhysicsMethod.OnCollisionStay);
        }

        private void OnCollisionExit(Collision other)
        {
            OnCollisionEvent(other, PhysicsTarget.PhysicsMethod.OnCollisionExit);
        }
        #endregion

        #region 2D
        private void OnTriggerEvent2D(Collider2D other, PhysicsTarget.PhysicsMethod physicsMethod)
        {
            var list = m_physicsTargets.Where(physicsTarget => physicsTarget.physicsMethods.Contains(physicsMethod));

            foreach (var physicsTarget in list)
            {
                physicsTarget.unityEvent?.Invoke();
                physicsTarget.target?.SendMessage(physicsMethod.ToString(), other);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEvent2D(other, PhysicsTarget.PhysicsMethod.OnTriggerEnter2D);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerEvent2D(other, PhysicsTarget.PhysicsMethod.OnTriggerStay2D);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnTriggerEvent2D(other, PhysicsTarget.PhysicsMethod.OnTriggerExit2D);
        }

        private void OnCollisionEvent2D(Collision2D other, PhysicsTarget.PhysicsMethod physicsMethod)
        {
            var list = m_physicsTargets.Where(physicsTarget => physicsTarget.physicsMethods.Contains(physicsMethod));

            foreach (var physicsTarget in list)
            {
                physicsTarget.unityEvent?.Invoke();
                physicsTarget.target?.SendMessage(physicsMethod.ToString(), other);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnCollisionEvent2D(other, PhysicsTarget.PhysicsMethod.OnCollisionEnter2D);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            OnCollisionEvent2D(other, PhysicsTarget.PhysicsMethod.OnCollisionStay2D);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            OnCollisionEvent2D(other, PhysicsTarget.PhysicsMethod.OnCollisionExit2D);
        }
        #endregion
    }
}