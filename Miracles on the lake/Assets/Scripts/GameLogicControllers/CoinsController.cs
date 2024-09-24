using DG.Tweening;
using PlayerHandlers;
using PlayerInterface;
using StoreHandlers;
using TMPro;
using UnityEngine;

namespace GameLogicControllers
{
    public class CoinsController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private TMP_Text _coefText;
        [SerializeField] private AudioSource _increaseCoinsSound;
        [SerializeField] private AudioSource _increaseCoefSound;
        [SerializeField] private AudioSource _decreaseCoefSound;

        private int _currentCoins;
        private int _inSuccession;
        private int _coefficient = 1;
        private Sequence _spawnCoefTextAnim;
        private Sequence _coefAnim;

        private void OnEnable()
        {
            WormScoreController.OnIncreaseCoins += IncreaseCoins;
            PlayerHandlers.CollisionHandler.OnDealDamage += DecreaseCoefficient;
            StoreController.OnUpdateCoins += UpdateCoins;
            PlayerInterfaceController.OnRestartCoinCoef += DecreaseCoefficient;
        }

        private void OnDisable()
        {   
            WormScoreController.OnIncreaseCoins -= IncreaseCoins;
            PlayerHandlers.CollisionHandler.OnDealDamage -= DecreaseCoefficient;
            StoreController.OnUpdateCoins -= UpdateCoins;
            PlayerInterfaceController.OnRestartCoinCoef -= DecreaseCoefficient;
        }

        private void Start()
        {
            UpdateCoins();
            UpdateCoinsText();
        }

        private void UpdateCoins()
        {
            _currentCoins = PlayerPrefs.GetInt(PlayerDataKeys.CoinsDataKey);
            UpdateCoinsText();
        }

        private void IncreaseCoins()
        {
            _increaseCoinsSound.Play();

            DOTween.Sequence()
                .Append(_coinsText.DOColor(Color.green, 0.5f))
                .Append(_coinsText.DOColor(new Color(1f, 0.38f, 0.53f), 0.5f));

            CheckInSuccession();
            _currentCoins += _coefficient;
            PlayerPrefs.SetInt(PlayerDataKeys.CoinsDataKey, _currentCoins);
            UpdateCoinsText();
        }

        private void CheckInSuccession()
        {
            _inSuccession++;

            if (_inSuccession % 3 == 0 && _coefficient < 10)
            {
                if (_coefficient == 1)
                {
                    _spawnCoefTextAnim = DOTween.Sequence()
                        .Append(_coefText.transform.DOScale(Vector3.one, 1f));

                    _coefAnim = DOTween.Sequence()
                        .Append(_coefText.transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 0.4f))
                        .Append(_coefText.DOColor(Color.red, 0.5f))
                        .Append(_coefText.transform.DOScale(Vector3.one, 0.4f))
                        .Append(_coinsText.DOColor(new Color(1f, 0.38f, 0.53f), 0.5f))
                        .SetLoops(-1, LoopType.Yoyo);

                }
                _increaseCoefSound.Play();
                _coefficient++;
                _coefText.text = $"x{_coefficient}";
            }
        }

        private void DecreaseCoefficient()
        {
            if (_coefficient > 1) 
            {        
                _spawnCoefTextAnim.Kill();
                _coefAnim.Kill();
                _coefText.transform.DOScale(Vector3.zero, 1f);
                _decreaseCoefSound.Play();
            }

            _coefficient = 1;
            _inSuccession = 0;
        }

        private void UpdateCoinsText()
        {
            _coinsText.text = $"{_currentCoins}";
        }
    }
}

