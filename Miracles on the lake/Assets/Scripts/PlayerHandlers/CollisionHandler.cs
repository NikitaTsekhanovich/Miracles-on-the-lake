using System;
using GameEntities.Types;
using PlayerInterface;
using UnityEngine;

namespace PlayerHandlers
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider2D _collisionBlock;

        public static Action OnDealDamage;
        public static Action OnIncreaseScore;

        private void OnEnable()
        {
            PlayerInterfaceController.OffPlayerCollision += OffCollisionBlock;
            HealthHandler.OffPlayerCollision += OffCollisionBlock;
            PlayerInterfaceController.OnRestartPlayerCollision += OnCollisionBlock;
        }

        private void OnDisable()
        {
            PlayerInterfaceController.OffPlayerCollision -= OffCollisionBlock;
            HealthHandler.OffPlayerCollision -= OffCollisionBlock;
            PlayerInterfaceController.OnRestartPlayerCollision -= OnCollisionBlock;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent<Worm>(out var worm))
            {
                worm.DoDestroy();
                OnIncreaseScore?.Invoke();
            }
            else if (col.TryGetComponent<Eagle>(out var eagle))
            {
                OnDealDamage?.Invoke();
            }
        }

        private void OffCollisionBlock()
        {
            _collisionBlock.enabled = false;
        }

        private void OnCollisionBlock()
        {
            _collisionBlock.enabled = true;
        }
    }
}

