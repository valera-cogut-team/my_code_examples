using System;
using _Main._Core.Scripts.Tools;
using UnityEngine;

namespace _Main._Core.Scripts.Features.Abilities
{
    [Serializable]
    public abstract class ABaseAbility : ScriptableObject, IAbility
    {
        public virtual void ApplyAbilityOnTriggerEnter(GameTagReference target, GameTagReference other)
        {
            
        }

        public virtual void ApplyAbilityOnTriggerStay(GameTagReference target, GameTagReference other)
        {
            
        }

        public virtual void ApplyAbilityOnTriggerExit(GameTagReference target, GameTagReference other)
        {
            
        }

        public virtual void ApplyAbilityOnCollisionEnter(GameTagReference target, GameTagReference other)
        {
            
        }

        public virtual void ApplyAbilityOnCollisionStay(GameTagReference target, GameTagReference other)
        {
            
        }

        public virtual void ApplyAbilityOnCollisionExit(GameTagReference target, GameTagReference other)
        {
            
        }
    }
}