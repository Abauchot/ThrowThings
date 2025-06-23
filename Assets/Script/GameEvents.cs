using System;

namespace Script
{
    public static class GameEvents
    {
        public static System.Action onProjectileHitTarget;
        public static System.Action onProjectileMissedTarget;
        public static System.Action onProjectileOutOfBounds;

        public static void ProjectileHitTarget()
        {
            onProjectileHitTarget?.Invoke();
        }
        
        public static void ProjectileMissedTarget()
        {
            onProjectileMissedTarget?.Invoke();
        }
        
        public static void ProjectileOutOfBounds()
        {
            onProjectileOutOfBounds?.Invoke();
        }
        
        
    }
}