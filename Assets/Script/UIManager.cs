using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class UIManager: MonoBehaviour
    {
        private Button _restartButton;
        private GameManager _gameManager;


        private void Start()
        {
            if (_restartButton)
            {
                _restartButton.gameObject.SetActive(false);
            }
            _gameManager = FindFirstObjectByType<GameManager>();
        }

        private void Update()
        {
            if(_gameManager && _gameManager.IsGameOver() && !_restartButton.gameObject.activeSelf)
            {
              _restartButton.gameObject.SetActive(true);
            }
        }

        public void OnRestartButton()
        {
            FindFirstObjectByType <GameManager>().RestartGame();
        }
    }
}