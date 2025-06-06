using System;

namespace Script
{
    public static class GameEvents
    {
        public static Action onProjectileHitTarget;

        public static void ProjectileHitTarget()
        {
            onProjectileHitTarget?.Invoke();
        }
    }
}