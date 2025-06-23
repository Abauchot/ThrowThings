using UnityEngine;
using TMPro;

namespace Script
{


    public class GameManager : MonoBehaviour
    {
        public int score = 0;
        public int lives = 3;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI livesText;
        public GameObject projectilePrefab;
        public Transform projectileSpawn;
        public GameObject targetPrefab;
       
        [Header("Game Area Bounds")]
        public Rect gameArea = new Rect(-2.5f, -5f, 5f, 10f);


        private void OnEnable()
        {
            GameEvents.onProjectileHitTarget += AddScore;
            GameEvents.onProjectileMissedTarget += LoseLife;
            GameEvents.onProjectileOutOfBounds += LoseLife;
        }

        private void OnDisable()
        {
            GameEvents.onProjectileHitTarget -= AddScore;
            GameEvents.onProjectileMissedTarget -= LoseLife;
            GameEvents.onProjectileOutOfBounds -= LoseLife;
        }

        private void Start()
        {
            UpdateScoreUI();
            UpdateLivesUI();
            RespawnProjectile();
            RespawnTarget();
        }

        private  void AddScore()
        {
            score++;
            UpdateScoreUI();
            RespawnProjectile();
            RespawnTarget();
        }
        
        private void LoseLife()
        {
            lives--;
            UpdateLivesUI();
            if (lives <= 0)
            {
                IsGameOver();
                Debug.Log("Game Over!");
            }
            else
            {
                RespawnProjectile();
                RespawnTarget();
            }
        }

        public void RestartGame()
        {
            score = 0;
            lives = 3;
            UpdateScoreUI();
            UpdateLivesUI();
            RespawnProjectile();
            RespawnTarget();
            Time.timeScale = 1f;
        }
        
        private void UpdateScoreUI()
        {
            if (scoreText)
                scoreText.text = "Score: " + score;
        }

        private void UpdateLivesUI()
        {
            if (livesText)
            {
                livesText.text = "Lives: " + new string('♥', lives);
            }
        }
        
        /*
         *  This method is responsible for respawning the projectile.
         *  It first destroys any existing projectile objects in the scene,
         *  then instantiates a new projectile at the specified spawn position.
         *  It checks if the projectilePrefab and projectileSpawn are set before instantiating.
         */
        
        private void RespawnProjectile()
        {
            foreach (var oldProjectile in FindObjectsByType<Projectile>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)) 
            {
                Destroy(oldProjectile.gameObject);
            }
            if(projectilePrefab && projectileSpawn)
            {
                Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity);
            }
        }
        
        /*
         *  This method is responsible for respawning the target.
         *  It first destroys any existing target objects in the scene,
         *  then instantiates a new target at a random position within the defined game area.
         *  It checks if the targetPrefab is set before instantiating.
         *  The random position is generated using the Rect's xMin, xMax, yMin, and yMax properties.
         */

        private void RespawnTarget()
        {
            foreach (var oldTarget in FindObjectsByType<Target>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)) 
            {
                Destroy(oldTarget.gameObject);
            }
            if (!targetPrefab) return;
            var x = Random.Range(gameArea.xMin, gameArea.xMax);
            var y = Random.Range(gameArea.yMin, gameArea.yMax);
            var spawnPos = new Vector3(x, y, 0f);
            Instantiate(targetPrefab, spawnPos, Quaternion.identity);
        }
        
        public bool IsGameOver()
        {
            if (lives > 0) return false;
            Time.timeScale = 0f;
            return true;

        }

        
        
    }
}
