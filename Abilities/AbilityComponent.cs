using System.Collections.Generic;
using _Main._Core.Scripts.Tools;
using UnityEngine;

namespace _Main._Core.Scripts.Features.Abilities
{
    public class AbilityComponent : MonoBehaviour
    {
        [SerializeField] private List<ABaseAbility> m_abilities;
        private GameTagReference m_gameTagReference;

        private void Start()
        {
            m_gameTagReference = GameTagReference.GetGameTagReference(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherGameTagRef = GameTagReference.GetGameTagReference(other.gameObject);
            if (null == otherGameTagRef) return;

            m_abilities.ForEach(ability => ability.ApplyAbilityOnTriggerEnter(
                m_gameTagReference, otherGameTagRef)
            );
        }

        /*private void OnTriggerStay(Collider other)
        {
            var otherGameTagRef = GameTagReference.GetGameTagReference(other.gameObject);
            if (null == otherGameTagRef) return;

            m_abilities.ForEach(ability => ability.ApplyAbilityOnTriggerStay(
                m_gameTagReference, otherGameTagRef)
            );
        }*/

        private void OnTriggerExit(Collider other)
        {
            var otherGameTagRef = GameTagReference.GetGameTagReference(other.gameObject);
            if (null == otherGameTagRef) return;

            m_abilities.ForEach(ability => ability.ApplyAbilityOnTriggerExit(
                m_gameTagReference, otherGameTagRef)
            );
        }

        private void OnCollisionEnter(Collision other)
        {
            var otherGameTagRef = GameTagReference.GetGameTagReference(other.gameObject);
            if (null == otherGameTagRef) return;

            m_abilities.ForEach(ability => ability.ApplyAbilityOnCollisionEnter(
                m_gameTagReference, otherGameTagRef)
            );
        }

        public void OnCollisionEnter(GameObject other)
        {
            var otherGameTagRef = GameTagReference.GetGameTagReference(other.gameObject);
            if (null == otherGameTagRef) return;

            m_abilities.ForEach(ability => ability.ApplyAbilityOnCollisionEnter(
                m_gameTagReference, otherGameTagRef)
            );
        }

        /*private void OnCollisionStay(Collision other)
        {
            var otherGameTagRef = GameTagReference.GetGameTagReference(other.gameObject);
            if (null == otherGameTagRef) return;

            m_abilities.ForEach(ability => ability.ApplyAbilityOnCollisionStay(
                m_gameTagReference, otherGameTagRef)
            );
        }*/

        private void OnCollisionExit(Collision other)
        {
            var otherGameTagRef = GameTagReference.GetGameTagReference(other.gameObject);
            if (null == otherGameTagRef) return;

            m_abilities.ForEach(ability => ability.ApplyAbilityOnCollisionExit(
                m_gameTagReference, otherGameTagRef)
            );
        }
    }
}