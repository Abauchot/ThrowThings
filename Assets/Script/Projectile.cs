using System;
using UnityEngine;

namespace Script
{
    public class Projectile : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Vector2 _startPos;
        private bool _isLaunched;
        private GameManager _gameManager;


        public float launchForce = 10f;

       private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _startPos = transform.position;
            _gameManager = FindFirstObjectByType<GameManager>();

        }
       
// À ajouter dans votre classe Projectile
        private void Update()
        {
            // Récupérer les limites de jeu depuis le GameManager
            GameManager gameManager = FindFirstObjectByType<GameManager>();
            if (gameManager)
            {
                Rect gameArea = gameManager.gameArea;
        
                // Vérifier si le projectile est en dehors des limites
                if (transform.position.x < gameArea.xMin || transform.position.x > gameArea.xMax ||
                    transform.position.y < gameArea.yMin || transform.position.y > gameArea.yMax)
                {
                    Debug.Log("Projectile out of bounds!");
                    // Déclencher l'événement
                    GameEvents.onProjectileOutOfBounds?.Invoke();
                    // Détruire le projectile
                    Destroy(gameObject);
                }
            }
        }

        
        /*
         * This method is called when the mouse button is pressed down.
         * It captures the starting position of the mouse in world coordinates.
         */

        private void OnMouseDown()
        {
            if (_isLaunched) return;
            _startPos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        }
        
        
        /*
         * This method is called when the mouse button is released.
         * It calculates the direction from the starting position to the current mouse position,
         * applies a force to the Rigidbody2D component, and sets the body type to Dynamic.
         * It also sets a flag to indicate that the projectile has been launched and schedules its destruction if it misses the target.
         */
        private void OnMouseUp()
        {
            if (_isLaunched) return;
            Vector2 endPos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
            var direction = endPos - _startPos;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.AddForce(direction.normalized * launchForce, ForceMode2D.Impulse);
            _isLaunched = true;
            Invoke(nameof(DestroyIfMissed),3f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Target")) return;
            GameEvents.onProjectileHitTarget.Invoke();
            Destroy(other.gameObject);
        }

        /*
         * This method destroys the projectile if it has not hit a target after 3 seconds.
         */
        private void DestroyIfMissed()
        {
            

            Destroy(gameObject);
        }
        
    }
}
