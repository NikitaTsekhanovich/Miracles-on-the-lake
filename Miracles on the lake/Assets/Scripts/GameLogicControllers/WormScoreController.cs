using System;
using PlayerHandlers;
using PlayerInterface;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace GameLogicControllers
{
    public class WormScoreController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _wormScoreText;

        private int _currentScore;
        private int _bestScore;

        public static Action OnIncreaseCoins;

        private void OnEnable()
        {
            CollisionHandler.OnIncreaseScore += IncreaseScore;
            PlayerInterfaceController.OnRestartScore += RestartScore;
        }

        private void OnDisable()
        {  
            CollisionHandler.OnIncreaseScore -= IncreaseScore;
            PlayerInterfaceController.OnRestartScore -= RestartScore;
        }

        private void Start()
        {
            _bestScore = PlayerPrefs.GetInt(PlayerDataKeys.WormScoreDataKey);
            UpdateScoreText();
        }

        private void RestartScore()
        {
            _currentScore = 0;
            UpdateScoreText();
        }

        private void IncreaseScore()
        {
            DOTween.Sequence()
                .Append(_wormScoreText.DOColor(Color.green, 0.5f))
                .Append(_wormScoreText.DOColor(new Color(1f, 0.38f, 0.53f), 0.5f));
            _currentScore++;
            UpdateScoreText();
            CheckBestScore();
            OnIncreaseCoins?.Invoke();
        }

        private void UpdateScoreText()
        {
            _wormScoreText.text = $"{_currentScore}";
        }

        private void CheckBestScore()
        {
            if (_currentScore > _bestScore)
            {
                PlayerPrefs.SetInt(PlayerDataKeys.WormScoreDataKey, _currentScore);
                _bestScore = _currentScore;
            }
        }
    }
}

