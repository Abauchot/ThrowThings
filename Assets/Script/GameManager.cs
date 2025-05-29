using UnityEngine;
using TMPro;

namespace Script
{


    public class GameManager : MonoBehaviour
    {
        public int score = 0;
        public TextMeshProUGUI scoreText;
        public GameObject projectilePrefab;
        public Transform projectileSpawn;

        private void OnEnable()
        {
            GameEvents.OnProjectileHitTarget += AddScore;
        }

        private void OnDisable()
        {
            GameEvents.OnProjectileHitTarget -= AddScore;
        }

        void Start()
        {
            SpawnProjectile();
            UpdateScoreUI();
        }

        public void AddScore()
        {
            score++;
            UpdateScoreUI();
            SpawnProjectile();
        }

        public void RestartGame()
        {
            score = 0;
            UpdateScoreUI();
            SpawnProjectile();
        }

        void SpawnProjectile()
        {
            Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);
        }

        void UpdateScoreUI()
        {
            if (scoreText != null)
                scoreText.text = "Score: " + score;
        }
    }
}
