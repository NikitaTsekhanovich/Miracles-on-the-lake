using System;
using MainMenu;
using UnityEngine;

namespace PlayerInterface
{
    public class PlayerInterfaceController : MonoBehaviour
    {
        public static Action OffPlayerCollision;
        public static Action OnUpdateAbilitiesState;
        public static Action OnRestartHealth;
        public static Action OnRestartSpawner;
        public static Action OnRestartScore;
        public static Action OnRestartPlayerCollision;
        public static Action OnRestartCoinCoef;
        public static Action OnClick;
        public static Action OffClick;

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            OnUpdateAbilitiesState?.Invoke();
            OnClick?.Invoke();
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
            OffClick?.Invoke();
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;
            OnUpdateAbilitiesState?.Invoke();
            OnRestartHealth?.Invoke();
            OnRestartScore?.Invoke();
            OnRestartSpawner?.Invoke();
            OnRestartPlayerCollision?.Invoke();
            OnClick?.Invoke();
        }

        public void BackToMenu()
        {
            OffPlayerCollision?.Invoke();
            Time.timeScale = 1f;
            OnClick?.Invoke();
            LoadingScreenController.Instance.ChangeScene("Menu");
        }
    }
}
