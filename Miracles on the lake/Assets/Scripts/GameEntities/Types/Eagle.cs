using GameEntities.TypesPhysicsMovement;
using UnityEngine;

namespace GameEntities.Types
{
    public class Eagle : Entity
    {
        [SerializeField] private PhysicsMovementEagle _physicsMovementEagle;

        public PhysicsMovementEagle PhysicsMovementEagle => _physicsMovementEagle;
    }
}

