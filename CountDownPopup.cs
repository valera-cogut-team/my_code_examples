using System.Collections;
using _Main._Core.Scripts.UI;
using TMPro;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;

namespace _Main._Core.Scripts.Popups
{
    public class CountDownPopup : ABasePopup
    {
        [SerializeField] private TMP_Text m_timerLabel;
        private UIViewsController m_uiViewsController;
        private SignalBus m_signalBus;

        [Inject]
        private void Construct(SignalBus signalBus, UIViewsController uiViewsController)
        {
            m_signalBus = signalBus;
            m_uiViewsController = uiViewsController;
        }

        private async UniTaskVoid Start()
        {
            m_signalBus.Fire(new CountDownPopupShowSignal());

            for (var i = 3; i >= 1; i--)
            {
                m_timerLabel.text = i.ToString();
                await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);
            }

            m_timerLabel.text = "Go!";
            await UniTask.Delay(TimeSpan.FromSeconds(1), ignoreTimeScale: false);

            m_uiViewsController.HideAllPopups();
        }
    }
}