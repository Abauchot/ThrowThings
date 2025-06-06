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
        public GameObject targetPrefab;
       
        [Header("Game Area Bounds")]
        public Rect gameArea = new Rect(-2.5f, -5f, 5f, 10f);


        private void OnEnable()
        {
            GameEvents.onProjectileHitTarget += AddScore;
        }

        private void OnDisable()
        {
            GameEvents.onProjectileHitTarget -= AddScore;
        }

        private void Start()
        {
            UpdateScoreUI();
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

        public void RestartGame()
        {
            score = 0;
            UpdateScoreUI();
            RespawnProjectile();
            RespawnTarget();
        }
        
        private void UpdateScoreUI()
        {
            if (scoreText)
                scoreText.text = "Score: " + score;
        }
        
        /*
         *  This method is responsible for respawning the projectile.
         *  It first destroys any existing projectile objects in the scene,
         *  then instantiates a new projectile at the specified spawn position.
         *  It checks if the projectilePrefab and projectileSpawn are set before instantiating.
         */
        
        public void RespawnProjectile()
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
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(gameArea.center, gameArea.size);
        }

        
        
    }
}
