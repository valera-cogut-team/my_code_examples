using _Main._Core.Scripts.Tools;

namespace _Main._Core.Scripts.Features.Abilities
{
    public interface IAbility
    {
        void ApplyAbilityOnTriggerEnter(GameTagReference target, GameTagReference other);
        void ApplyAbilityOnTriggerStay(GameTagReference target, GameTagReference other);
        void ApplyAbilityOnTriggerExit(GameTagReference target, GameTagReference other);
        void ApplyAbilityOnCollisionEnter(GameTagReference target, GameTagReference other);
        void ApplyAbilityOnCollisionStay(GameTagReference target, GameTagReference other);
        void ApplyAbilityOnCollisionExit(GameTagReference target, GameTagReference other);
    }
}