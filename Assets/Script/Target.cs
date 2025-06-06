using System;
using UnityEngine;

namespace Script
{
    public class Target : MonoBehaviour
    {
        public float speed = 2f;
        public float amplitude = 2f;
        private Vector3 _startPos;
       
        
        private void Start()
        {
            _startPos = transform.position;
        }

       
        private void Update()
        {
            transform.position = _startPos + Vector3.right * (Mathf.Sin(Time.time * speed) * amplitude);
            
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
    }
}