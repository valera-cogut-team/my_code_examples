using _Main._Core.Scripts.Features.Projectile;
using _Main._Core.Scripts.Features.Unit;
using _Main._Core.Scripts.Installers;
using _Main._Core.Scripts.Tools;
using UnityEngine;
using Zenject;

namespace _Main._Core.Scripts.Features.Abilities
{
    [CreateAssetMenu(fileName = "Ability Name", menuName = "Abilities/ColorableAbility")]
    public class ColorableAbility : ABaseAbility
    {
        private UnitComponent m_unitComponent;
        private ProjectileComponent m_projectileComponent;
        private GameTag m_colorableGameTag;
        private GameTag m_projectileGameTag;
        private GameTag m_teamOneGameTag;
        private GameTag m_teamTwoGameTag;

        [Inject]
        private void Construct
        (
            [Inject(Id = GameInstaller.COLORABLE)]GameTag colorableGameTag,
            [Inject(Id = GameInstaller.PROJECTILE)]GameTag projectileGameTag,
            [Inject(Id = GameInstaller.TEAM_ONE)]GameTag teamOneGameTag,
            [Inject(Id = GameInstaller.TEAM_TWO)]GameTag teamTwoGameTag
        )
        {
            m_colorableGameTag = colorableGameTag;
            m_projectileGameTag = projectileGameTag;
            m_teamOneGameTag = teamOneGameTag;
            m_teamTwoGameTag = teamTwoGameTag;
        }

        public override void ApplyAbilityOnCollisionEnter(GameTagReference target, GameTagReference other)
        {
            base.ApplyAbilityOnCollisionEnter(target, other);

            if (!target.HasTag(m_colorableGameTag)) return;
            if (!other.HasTag(m_projectileGameTag)) return;

            if (target.HasTag(m_teamOneGameTag) &&
                other.HasTag(m_teamOneGameTag)) return;

            if (target.HasTag(m_teamTwoGameTag) &&
                other.HasTag(m_teamTwoGameTag)) return;

            m_unitComponent = target.GetComponent<UnitComponent>();
            if (null == m_unitComponent) return;

            m_projectileComponent = other.GetComponent<ProjectileComponent>();
            if (null == m_projectileComponent) return;

            if (other.HasTag(m_teamOneGameTag))
                m_unitComponent.ShootsCounterTeamOne += m_projectileComponent.WeaponConfig.damage;
            else if (other.HasTag(m_teamTwoGameTag))
                m_unitComponent.ShootsCounterTeamTwo += m_projectileComponent.WeaponConfig.damage;
        }
    }
}