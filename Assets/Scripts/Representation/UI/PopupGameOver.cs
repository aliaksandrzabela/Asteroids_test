using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.View
{
    public class PopupGameOver : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;
        [SerializeField] Button restartButton;

        public void Show(int score, Action OnReload)
        {
            gameObject.SetActive(true);
            scoreText.text = $"Score: {score}";

            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(() => OnReload());
        }
    }
}

