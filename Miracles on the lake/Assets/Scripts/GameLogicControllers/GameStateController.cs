using System;
using PlayerHandlers;
using TMPro;
using UnityEngine;

namespace GameLogicControllers
{
    public class GameStateController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentWormsText;
        [SerializeField] private TMP_Text _currentCoinsText;
        [SerializeField] private TMP_Text _wormsText;
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private AudioSource _loseSound;

        public static Action OffClick;

        private void OnEnable()
        {
            HealthHandler.OnLose += DoLose;
        }

        private void OnDisable()
        {
            HealthHandler.OnLose -= DoLose;
        }

        private void DoLose()
        {
            OffClick?.Invoke();
            _loseSound.Play();
            _loseScreen.SetActive(true);
            _wormsText.text = $"{_currentWormsText.text}";
            _coinsText.text = $"{_currentCoinsText.text}";
        }
    }
}

