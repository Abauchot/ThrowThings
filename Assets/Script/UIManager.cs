using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class UIManager: MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        private GameManager _gameManager;


        private void Start()
        {
            if (restartButton)
            {
                restartButton.gameObject.SetActive(false);
            }
            _gameManager = FindFirstObjectByType<GameManager>();
        }

        private void Update()
        {
            if (_gameManager && _gameManager.IsGameOver() && restartButton && !restartButton.gameObject.activeSelf)
            {
                restartButton.gameObject.SetActive(true);
            }
        }

        public void OnRestartButton()
        {
            restartButton.gameObject.SetActive(false);
            _gameManager.RestartGame();
            
        }
        
        
    }
}