using System;
using UnityEngine;

namespace Script
{
    public class Target : MonoBehaviour
    {
        public float speed = 2f;
        public float amplitude = 2f;
        private Vector3 _startPos;
        private Vector2 _randomDirection;
        private const float ChangeDirectionTime = 2f;
        private float _timer; 
       
        
        private void Start()
        {
            _startPos = transform.position;
            SetRandomDirection();
        }

       
        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > ChangeDirectionTime)
            {
                SetRandomDirection();
                _timer = 0f;
            }
            transform.position += (Vector3)_randomDirection * (speed * Time.deltaTime);
            
            var gamerManager = FindFirstObjectByType<GameManager>();
            if (!gamerManager) return;
            var gameArea = gamerManager.gameArea;
            var pos = transform.position;
            
            if(pos.x < gameArea.xMin || pos.x > gameArea.xMax || pos.y < gameArea.yMin || pos.y > gameArea.yMax)
            {
                _randomDirection.x *= -1;
                pos.x = Mathf.Clamp(pos.x, gameArea.xMin, gameArea.xMax);
                transform.position = pos;
            }

            if (!(pos.y < gameArea.yMin) && !(pos.y > gameArea.yMax)) return;
            _randomDirection.y *= -1;
            pos.y = Mathf.Clamp(pos.y, gameArea.yMin, gameArea.yMax);
            transform.position = pos;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            /*
             * This method is called when a collider enters the trigger collider attached to the target.
             * It checks if the other collider is a projectile, and if so, it invokes the onProjectileHitTarget event,
             * destroys the projectile, and destroys the target itself.
             * If the other collider is not a projectile, it does nothing.
            */
            if (other && other.gameObject && other && other.CompareTag("Projectile")) return;
            GameEvents.onProjectileHitTarget?.Invoke();
            Destroy(other.gameObject);
            Destroy(gameObject);
            Console.WriteLine("Projectile hit the target!");

        }

        private void SetRandomDirection()
        {
            var angle = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
            _randomDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        }
    }
}