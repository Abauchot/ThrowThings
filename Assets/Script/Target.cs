using System;
using UnityEngine;

namespace Script
{
    public class Target : MonoBehaviour
    {
        public float speed = 2f;
        public float amplitude = 2f;
        private Vector3 _startPos;
        private int _projectilLayer;
       
        
        private void Start()
        {
            _startPos = transform.position;
            _projectilLayer = LayerMask.NameToLayer("Projectile");
        }

       
        private void Update()
        {
            transform.position = _startPos + Vector3.right * (Mathf.Sin(Time.time * speed) * amplitude);
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != _projectilLayer) return;
            GameEvents.ProjectileHitTarget();
            Destroy(other.gameObject);
        }
    }
}