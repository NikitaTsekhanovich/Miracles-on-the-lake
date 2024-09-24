using UnityEngine;

namespace GameEntities
{
    public class PhysicsMovementEntity : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _rigidbody;
        [SerializeField] protected float _speed;
    }
}

