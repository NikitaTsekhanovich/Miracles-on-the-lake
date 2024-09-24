using System;
using System.Collections.Generic;
using PlayerHandlers;
using TMPro;
using UnityEngine;

namespace StoreHandlers
{
    public class StoreController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private List<StoreItem> _storeItemsData = new();
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private AudioSource _buyItemSound;

        private int _currentCoins;
        private StoreItem _currentItem;

        public static Action OnBuyGoldHeart;
        public static Action OnUpdateCoins;

        private void OnEnable()
        {
            _currentCoins = PlayerPrefs.GetInt(PlayerDataKeys.CoinsDataKey);
            UpdateChooseStateItems();
            UpdateCoinsText();
            UpdateCurrentItem(0);
            CheckBoughtItems();
        }

        private void UpdateChooseStateItems()
        {
            foreach (var item in _storeItemsData)
                item.FrameChoose.enabled = false;
        }

        private void UpdateCoinsText()
        {
            _coinsText.text = $"{_currentCoins}";
        }

        private void UpdateCurrentItem(int index)
        {
            _currentItem = _storeItemsData[index];
            _currentItem.FrameChoose.enabled = true;
            _descriptionText.text = _currentItem.StoreItemData.Description;
            _priceText.text = $"{_currentItem.StoreItemData.Price}";
        }

        private void CheckBoughtItems()
        {
            foreach (var item in _storeItemsData)
            {
                if (item.StoreItemData.IsBought == (int)TypeItemPurchased.Bought)
                    item.IsBoughtImage.SetActive(true);
                else
                    item.IsBoughtImage.SetActive(false);
            }
        }

        public void ChooseItem(int index)
        {
            _currentItem.FrameChoose.enabled = false;
            UpdateCurrentItem(index);
        }

        public void BuyItem()
        {
            if (_currentItem.StoreItemData.IsBought == (int)TypeItemPurchased.NotBought && _currentCoins - _currentItem.StoreItemData.Price >= 0)
            {
                _buyItemSound.Play();
                PlayerPrefs.SetInt(_currentItem.StoreItemData.IsBoughtKey, 1);
                _currentCoins -= _currentItem.StoreItemData.Price;
                PlayerPrefs.SetInt(PlayerDataKeys.CoinsDataKey, _currentCoins);
                OnUpdateCoins?.Invoke();
                UpdateCoinsText();
                _currentItem.IsBoughtImage.SetActive(true);

                if (_currentItem.StoreItemData.Name == "Health3")
                {
                    PlayerPrefs.SetInt(PlayerDataKeys.AmountGoldHeartKey, 3);
                    OnBuyGoldHeart?.Invoke();
                }

                // switch (_currentItem.StoreItemData.Name)
                // {
                //     case "Health1":
                //         PlayerPrefs.SetInt(PlayerDataKeys.AmountGoldHeartKey, 1);
                //         break;
                //     case "Health2":
                //         PlayerPrefs.SetInt(PlayerDataKeys.AmountGoldHeartKey, 2);
                //         break;
                //     case "Health3":
                //         PlayerPrefs.SetInt(PlayerDataKeys.AmountGoldHeartKey, 3);
                //         break;
                // }
            }
        }
    }
}

