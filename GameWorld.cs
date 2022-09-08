using System.Collections.Generic;
using _Main._Core.Scripts.Configs;
using _Main._Core.Scripts.Tools.Types;
using _Main._Core.Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Main._Core.Scripts.World
{
    public class GameWorld : MonoBehaviour
    {
        public enum ELevelStatus
        {
            Initial, LevelStarted, LevelFinished
        }
        public ELevelStatus LevelStatus { get; set; }
        [SerializeField] private IntVariable m_levelVariable;
        [SerializeField] private List<GameObject> m_gangstersTeamOne;
        [SerializeField] private List<GameObject> m_bazookaMansTeamOne;
        [SerializeField] private List<GameObject> m_carsTeamOne;
        [SerializeField] private List<GameObject> m_gangstersTeamTwo;
        [SerializeField] private List<GameObject> m_bazookaMansTeamTwo;
        [SerializeField] private List<GameObject> m_carsTeamTwo;
        [SerializeField] private List<GameObject> m_gangstersNeutral;
        [SerializeField] private List<GameObject> m_carsNeutral;

        private SignalBus m_signalBus;
        private UIViewsController m_uiViewsController;
        private Config m_config;
        private bool m_canShootTeamOne;
        public bool CanShootTeamOne => m_canShootTeamOne;
        private bool m_canShootTeamTwo;
        public bool CanShootTeamTwo => m_canShootTeamTwo;

        [Inject]
        private void Construct
        (
            SignalBus signalBus,
            UIViewsController uiViewsController,
            Config config
        )
        {
            m_signalBus = signalBus;
            m_uiViewsController = uiViewsController;
            m_config = config;

            LevelStatus = ELevelStatus.Initial;
            m_levelVariable.Value = Mathf.Max(1, m_levelVariable.Value);
            m_canShootTeamOne = m_canShootTeamTwo = true;
        }

        private void GenerateLevel()
        {
            SetActiveByLevelNumber(m_gangstersTeamOne, 3);
            SetActiveByLevelNumber(m_gangstersTeamTwo, 2);

            SetActiveByLevelNumber(m_bazookaMansTeamOne, 3);
            SetActiveByLevelNumber(m_bazookaMansTeamTwo, 2);

            SetActiveByLevelNumber(m_gangstersNeutral, 2);
            SetActiveByLevelNumber(m_carsNeutral, 1, 3);
            SetActiveByLevelNumber(m_carsTeamOne, 1, 5);
            SetActiveByLevelNumber(m_carsTeamTwo, 1, 4);
        }

        private void SetActiveByLevelNumber(List<GameObject> list, int size, int fromLevel = 0)
        {
            var flag = m_levelVariable.Value >= fromLevel;
            for (var i = 0; i < list.Count; i++)
            {
                flag = flag && i < (m_levelVariable.Value / size) + 1;
                list[i].SetActive(flag);
            }
        }

        public void StartLevel()
        {
            LevelStatus = ELevelStatus.LevelStarted;
            m_signalBus.Fire(new LevelStartedSignal());

            GenerateLevel();
        }

        private void FinishLevel()
        {
            LevelStatus = ELevelStatus.LevelFinished;
            m_signalBus.Fire(new LevelFinishedSignal());
        }

        public void OnVictory()
        {
            if (LevelStatus != ELevelStatus.LevelStarted) return;

            m_levelVariable.Value++;
            m_uiViewsController.Invoke(nameof(m_uiViewsController.ShowVictoryPopup), m_config.Settings.waitFinishedLevelPopup);
            FinishLevel();
        }

        public void OnDefeat()
        {
            if (LevelStatus != ELevelStatus.LevelStarted) return;

            m_uiViewsController.Invoke(nameof(m_uiViewsController.ShowDefeatPopup), m_config.Settings.waitFinishedLevelPopup);
            FinishLevel();
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Q)) m_canShootTeamOne = !m_canShootTeamOne;
            if (Input.GetKeyUp(KeyCode.E)) m_canShootTeamTwo = !m_canShootTeamTwo;
        }
#endif
    }
}