using Infrastructure.Services.Level;
using Infrastructure.Services.Score;
using UnityEngine;
using Zenject;

namespace UI.Level
{
    public class LevelView : MonoBehaviour
    {
        [Header("Popup")]
        [SerializeField] private GameObject _congratulationPopup;
        [SerializeField] private GameObject _losingPopup;

        [Inject] private IGameResetService _gameResetService;
        [Inject] private IGameResultService _gameResultService;

        private void Start()
        {
            _gameResultService.OnWin += ShowCongratulationPopup;
            _gameResultService.OnLose += ShowLosingPopup;
        }

        private void OnDestroy()
        {
            _gameResultService.OnWin -= ShowCongratulationPopup;
            _gameResultService.OnLose -= ShowLosingPopup;
        }

        public void Restart() =>
            _gameResetService.RestartLevel();

        private void ShowCongratulationPopup() =>
            _congratulationPopup.SetActive(true);

        private void ShowLosingPopup() =>
            _losingPopup.SetActive(true);
    }
}
