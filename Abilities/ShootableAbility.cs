using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Main._Core.Scripts.Configs;
using _Main._Core.Scripts.Features.Weapon;
using _Main._Core.Scripts.Installers;
using _Main._Core.Scripts.Tools;
using _Main._Core.Scripts.World;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

namespace _Main._Core.Scripts.Features.Abilities
{
    [CreateAssetMenu(fileName = "Ability Name", menuName = "Abilities/ShootableAbility")]
    public class ShootableAbility : ABaseAbility
    {
        [Serializable]
        public class AbilityPair
        {
            public GameTagReference Target;
            public GameTagReference Other;
        }
        [SerializeField] private List<AbilityPair> m_abilityPairs;
        public List<AbilityPair> AbilityPairs => m_abilityPairs;

        private WeaponComponent m_weaponComponent;
        private SignalBus m_signalBus;
        private GameWorld m_gameWorld;
        private GameTag m_colorableGameTag;
        private GameTag m_teamOneGameTag;
        private GameTag m_teamTwoGameTag;
        private List<AbilityPair> m_closestAbilityPairs;

        [Inject]
        private void Construct
        (
            SignalBus signalBus, 
            Config config, 
            GameWorld gameWorld, 
            [Inject(Id = GameInstaller.COLORABLE)]GameTag colorableGameTag,
            [Inject(Id = GameInstaller.TEAM_ONE)]GameTag teamOneGameTag,
            [Inject(Id = GameInstaller.TEAM_TWO)]GameTag teamTwoGameTag
        )
        {
            m_signalBus = signalBus;
            m_gameWorld = gameWorld;
            m_colorableGameTag = colorableGameTag;
            m_teamOneGameTag = teamOneGameTag;
            m_teamTwoGameTag = teamTwoGameTag;

            m_signalBus.Subscribe<LevelStartedSignal>(OnLevelStartedSignal);
            m_abilityPairs = new List<AbilityPair>();
            m_closestAbilityPairs = new List<AbilityPair>();
        }

        private void OnDestroy()
        {
            m_signalBus.Unsubscribe<LevelStartedSignal>(OnLevelStartedSignal);
        }

        private void OnLevelStartedSignal(LevelStartedSignal signal)
        {
            m_gameWorld.StartCoroutine(UpdateAbility());
        }

        private IEnumerator UpdateAbility()
        {
            while (true)
            {
                if (m_gameWorld.LevelStatus == GameWorld.ELevelStatus.LevelFinished) yield break;

                UpdateShooting();

                yield return new WaitForSeconds(0.01f);
            }
        }

        private void UpdateShooting()
        {
            if (0 == m_abilityPairs.Count) return;

            m_closestAbilityPairs.Clear();
            m_abilityPairs.ForEach(pair =>
            {
                if (null != m_closestAbilityPairs.FirstOrDefault(x => x.Target == pair.Target)) return;

                var closestPair = m_abilityPairs.Where(x => x.Target == pair.Target)
                    .OrderBy(x => Vector3.Distance(x.Target.transform.position, x.Other.transform.position))
                    .First();

                m_closestAbilityPairs.Add(closestPair);
            });

            m_closestAbilityPairs.ForEach(pair =>
            {
                m_weaponComponent = pair.Target.GetComponent<WeaponComponent>();
                if (null == m_weaponComponent || !m_weaponComponent.gameObject.activeInHierarchy) return;

                m_weaponComponent.shootDirection = pair.Other.transform.position - pair.Target.transform.position;
                m_weaponComponent.shootDirection.y = 0;
                if (m_weaponComponent.shouldLookAtTarget) pair.Target.transform.GetChild(0).LookAt(pair.Other.transform);
                m_weaponComponent.CallShootCoroutine(m_weaponComponent.shootDirection);
            });
        }

        public override void ApplyAbilityOnTriggerEnter(GameTagReference target, GameTagReference other)
        {
            base.ApplyAbilityOnTriggerEnter(target, other);
            var collider = other.GetComponentInChildren<Collider>();
            if (null != collider && collider.isTrigger) return;

            if (!other.HasTag(m_colorableGameTag)) return;

            if (target.HasTag(m_teamOneGameTag) &&
                other.HasTag(m_teamOneGameTag)) return;

            if (target.HasTag(m_teamTwoGameTag) &&
                other.HasTag(m_teamTwoGameTag)) return;

            m_abilityPairs.Add(new AbilityPair{Target = target, Other = other});

            ClearNullableAbilityPairs();
        }

        /*public override void ApplyAbilityOnTriggerStay(GameTagReference target, GameTagReference other)
        {
            base.ApplyAbilityOnTriggerStay(target, other);
            var collider = other.GetComponentInChildren<Collider>();
            if (null != collider && collider.isTrigger) return;

            if (!other.HasTag(m_colorableGameTag)) return;

            var flag = target.HasTag(m_teamOneGameTag) &&
                       other.HasTag(m_teamOneGameTag);
            flag = flag || target.HasTag(m_teamTwoGameTag) &&
                other.HasTag(m_teamTwoGameTag);

            var searchedPair = m_abilityPairs.FirstOrDefault(x => x.Target == target && x.Other == other);
            if (!flag)
            {
                if (null == searchedPair)
                    m_abilityPairs.Add(new AbilityPair{Target = target, Other = other});
                return;
            }

            if (null == searchedPair) return;

            m_abilityPairs.Remove(searchedPair);

            ClearNullableAbilityPairs();
        }*/

        public override void ApplyAbilityOnTriggerExit(GameTagReference target, GameTagReference other)
        {
            base.ApplyAbilityOnTriggerExit(target, other);

            m_abilityPairs.Remove(m_abilityPairs.FirstOrDefault(abilityPair => abilityPair.Target == target && abilityPair.Other == other));

            ClearNullableAbilityPairs();
        }

        private void ClearNullableAbilityPairs()
        {
            m_abilityPairs.Remove(m_abilityPairs.FirstOrDefault(abilityPair => abilityPair.Target == null || abilityPair.Other == null));
        }
    }
}