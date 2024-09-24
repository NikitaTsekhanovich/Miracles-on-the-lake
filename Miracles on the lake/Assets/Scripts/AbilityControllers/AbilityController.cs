using System;
using System.Collections.Generic;
using PlayerHandlers;
using PlayerInterface;
using StoreHandlers;
using UnityEngine;
using UnityEngine.UI;

namespace AbilityControllers
{
    public class AbilityController : MonoBehaviour
    {
        [SerializeField] private List<StoreItemData> _storeItemsData = new();
        [SerializeField] private Button _shieldButton;
        [SerializeField] private Button _offEaglesButton;
        [SerializeField] private Button _smallPlayerButton;
        [SerializeField] private AudioSource _shieldSound;
        [SerializeField] private AudioSource _offEaglesSound;
        [SerializeField] private AudioSource _smallPlayerSound;

        public static Action OnShield;
        public static Action OnOffEagles;
        public static Action OnSmallPlayer;

        public static Action OnClick;
        public static Action OffClick;

        private void OnEnable()
        {
            PlayerInterfaceController.OnUpdateAbilitiesState += UpdateAbilitiesState;
            HealthHandler.OnUseGoldenHeart += UseGoldenHeart;
        }

        private void OnDisable()
        {
            PlayerInterfaceController.OnUpdateAbilitiesState -= UpdateAbilitiesState;
            HealthHandler.OnUseGoldenHeart -= UseGoldenHeart;
        }

        private void Start()
        {
            UpdateAbilitiesState();
        }

        private void UpdateAbilitiesState()
        {
            if (_storeItemsData[0].IsBought == (int)TypeItemPurchased.Bought)
                _shieldButton.interactable = true;
            if (_storeItemsData[1].IsBought == (int)TypeItemPurchased.Bought)
                _offEaglesButton.interactable = true;
            if (_storeItemsData[2].IsBought == (int)TypeItemPurchased.Bought)
                _smallPlayerButton.interactable = true;
        }

        public void UseShield()
        {
            UseAbility(_shieldButton, 0, _shieldSound, OnShield);
        }

        public void UseOffEagles()
        {
            UseAbility(_offEaglesButton, 1, _offEaglesSound, OnOffEagles);
        }

        public void UseSmallPlayer()
        {
            UseAbility(_smallPlayerButton, 2, _smallPlayerSound, OnSmallPlayer);
        }

        private void UseAbility(Button button, int index, AudioSource soundAbility, Action action)
        {
            OffClick?.Invoke();

            soundAbility.Play();
            button.interactable = false;
            PlayerPrefs.SetInt(_storeItemsData[index].IsBoughtKey, 0);
            action?.Invoke();

            OnClick?.Invoke();
        }

        private void UseGoldenHeart()
        {
            PlayerPrefs.SetInt(_storeItemsData[3].IsBoughtKey, 0);
        }
    }
}

