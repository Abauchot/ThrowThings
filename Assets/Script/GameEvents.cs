using System;

namespace Script
{
    public static class GameEvents
    {
        public static Action OnProjectileHitTarget;

        public static void ProjectileHitTarget()
        {
            OnProjectileHitTarget?.Invoke();
        }
    }
}