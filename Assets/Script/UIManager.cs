using UnityEngine;

namespace Script
{
    public class UIManager: MonoBehaviour
    {
        public void OnRestartButton()
        {
            FindFirstObjectByType <GameManager>().RestartGame();
        }
    }
}