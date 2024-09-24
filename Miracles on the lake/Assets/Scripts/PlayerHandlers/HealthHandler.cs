using System;
using System.Collections;
using PlayerInterface;
using StoreHandlers;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerHandlers
{
    public class HealthHandler : MonoBehaviour
    {
        [SerializeField] private Image _heart1;
        [SerializeField] private Image _heart2;
        [SerializeField] private Image _heart3;
        [SerializeField] private Image _goldHeart1;
        [SerializeField] private Image _goldHeart2;
        [SerializeField] private Image _goldHeart3;
        [SerializeField] AnimationsController _animationsController;
        [SerializeField] private AudioSource _dealDamageAudio;

        private int _currentHeart = 3;
        private int _currentGoldHeart;

        public static Action OnLose;
        public static Action OffPlayerCollision;
        public static Action OnUseGoldenHeart;

        private void Start()
        {
            CheckGoldHearts();
        }

        private void OnEnable()
        {
            CollisionHandler.OnDealDamage += DealDamage;
            StoreController.OnBuyGoldHeart += CheckGoldHearts;
            PlayerInterfaceController.OnRestartHealth += RestartHealth;
        }

        private void OnDisable()
        {
            CollisionHandler.OnDealDamage -= DealDamage;
            StoreController.OnBuyGoldHeart -= CheckGoldHearts;
            PlayerInterfaceController.OnRestartHealth -= RestartHealth;
        }

        private void RestartHealth()
        {
            CheckGoldHearts();

            _currentHeart = 3;
            _heart1.fillAmount = 1;
            _heart2.fillAmount = 1;
            _heart3.fillAmount = 1;
        }

        private void CheckGoldHearts()
        {
            if (PlayerPrefs.GetInt(PlayerDataKeys.AmountGoldHeartKey) == 3)
            {
                _goldHeart1.fillAmount = 1;
                _goldHeart2.fillAmount = 1;
                _goldHeart3.fillAmount = 1;
                _heart1.fillAmount = 1;
                _heart2.fillAmount = 1;
                _heart3.fillAmount = 1;
                _currentGoldHeart = 3;
                _currentHeart = 3;
            }
            // else if (PlayerPrefs.GetInt(PlayerDataKeys.AmountGoldHeartKey) == 2)
            // {
            //     if (_currentGoldHeart == 0)
            //     {
            //         _goldHeart2.fillAmount = 1;
            //         _goldHeart3.fillAmount = 1;
            //         _heart2.fillAmount = 1;
            //         _heart3.fillAmount = 1;
            //         _currentGoldHeart = 2;

            //         if (_currentHeart == 1)
            //         {
            //             _currentHeart = 2;
            //             _heart2.fillAmount = 1;
            //             _heart3.fillAmount = 1;
            //         }
            //         else if (_currentHeart == 2)
            //         {
            //             _currentHeart = 3;
            //             _heart1.fillAmount = 1;
            //             _heart2.fillAmount = 1;
            //         }
            //     }
            //     else if (_currentGoldHeart == 1)
            //     {
            //         _goldHeart1.fillAmount = 1;
            //         _goldHeart2.fillAmount = 1;
            //         _heart1.fillAmount = 1;
            //         _heart2.fillAmount = 1;
            //         _currentGoldHeart = 2;
            //         _currentHeart = 3;
            //     }
                
            // }
            // else if (PlayerPrefs.GetInt(PlayerDataKeys.AmountGoldHeartKey) == 1)
            // {
            //     switch (_currentGoldHeart)
            //     {
            //         case 2:
            //             _heart3.fillAmount = 1;
            //             _currentGoldHeart = 3;

            //             if (_currentHeart == 2)
            //             {
            //                 _currentHeart = 3;
            //                 _heart1.fillAmount = 1;
            //             }

            //             break;
            //         case 1:
            //             _goldHeart2.fillAmount = 1;
            //             _currentGoldHeart = 2;

            //             if (_currentHeart == 1)
            //             {
            //                 _currentHeart = 2;
            //                 _heart2.fillAmount = 1;
            //             }

            //             break;
            //         case 0:
            //             _goldHeart3.fillAmount = 1;
            //             _currentGoldHeart = 1;
            //             break;
            //     }
            // }
        }

        private void DealDamage()
        {
            _dealDamageAudio.Play();
            
            if (_currentGoldHeart <= 0)
            {
                _currentHeart = GetCurrentHeart(_currentHeart, _heart1, _heart2, _heart3);
                if (_currentHeart <= 0)
                {
                    OnLose?.Invoke();
                    OffPlayerCollision?.Invoke();
                }
            }
            else 
            {
                _currentGoldHeart = GetCurrentHeart(_currentGoldHeart, _goldHeart1, _goldHeart2, _goldHeart3);
                if (_currentGoldHeart < 3)
                {
                    PlayerPrefs.SetInt(PlayerDataKeys.AmountGoldHeartKey, 0);
                    OnUseGoldenHeart?.Invoke();
                }
            }
        }

        private int GetCurrentHeart(int currentHeart, Image heart1, Image heart2, Image heart3)
        {
            switch (currentHeart)
            {
                case 3:
                    StartCoroutine(IncreaseHeartAnimation(heart1));
                    _animationsController.DealDamageAnimation();
                    break;
                case 2:
                    StartCoroutine(IncreaseHeartAnimation(heart2));
                    _animationsController.DealDamageAnimation();
                    break;
                case 1:
                    StartCoroutine(IncreaseHeartAnimation(heart3));
                    _animationsController.DealDamageAnimation();
                    break;
            }

            currentHeart--;
            return currentHeart;
        }

        private IEnumerator IncreaseHeartAnimation(Image heart)
        {
            var timeAnimation = 1f;

            while (timeAnimation > 0)
            {
                heart.fillAmount -= 0.1f;
                timeAnimation -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}

