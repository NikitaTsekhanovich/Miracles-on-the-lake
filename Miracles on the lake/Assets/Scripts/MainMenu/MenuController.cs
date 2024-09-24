using PlayerHandlers;
using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _bestScoreText;

        private void Start()
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            UpdateBestScore();
        }

        private void UpdateBestScore()
        {
            _bestScoreText.text = $"{PlayerPrefs.GetInt(PlayerDataKeys.WormScoreDataKey)}";
        }

        public void StartGame()
        {
            LoadingScreenController.Instance.ChangeScene("Game");
        }
    }
}

