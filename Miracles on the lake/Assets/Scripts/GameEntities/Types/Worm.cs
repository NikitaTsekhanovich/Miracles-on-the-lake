using GameEntities.TypesPhysicsMovement;
using UnityEngine;

namespace GameEntities.Types
{
    public class Worm : Entity
    {
        [SerializeField] private PhysicsMovementWorm _physicsMovementWorm;

        public PhysicsMovementWorm PhysicsMovementWorm => _physicsMovementWorm;
    }
}

