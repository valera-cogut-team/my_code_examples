using _Main._Core.Scripts.Tools;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Main._Core.Scripts.Popups
{
    public abstract class ABasePopup : MonoBehaviour
    {
        [SerializeField] private Button m_exitButton;
        [SerializeField] private bool m_shouldRestartOnExit;

        protected SceneLoader m_sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            m_sceneLoader = sceneLoader;
        }

        protected virtual void OnEnable()
        {
            m_exitButton.onClick.AddListener(OnExit);
        }

        protected virtual void OnDisable()
        {
            m_exitButton.onClick.RemoveListener(OnExit);
        }

        private void OnExit()
        {
            if (m_shouldRestartOnExit)
                Restart();
            else
                m_sceneLoader.LoadScene();
        }

        protected void Restart()
        {
            m_sceneLoader.LoadScene();
        }
    }
}