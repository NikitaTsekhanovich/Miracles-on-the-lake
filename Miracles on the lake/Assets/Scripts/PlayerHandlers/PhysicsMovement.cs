using GameEntities.TypesPhysicsMovement;
using UnityEngine;

namespace PlayerHandlers
{
    public class PhysicsMovement : MonoBehaviour
    {
        [SerializeField] private float _jumpPower;
        [SerializeField] private AudioSource _moveSound;
        [SerializeField] private float _directionPower;
        private Rigidbody2D _rigidbody;

        private void OnEnable()
        {
            PhysicsMovementEagle.GetPositionPlayer += GetPositionPlayer;
        }

        private void OnDisable()
        {
            PhysicsMovementEagle.GetPositionPlayer -= GetPositionPlayer;
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Movement()
        {
            // Physics2D.queriesHitTriggers = false;
            var _clickPosision = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // var hit = Physics2D.Raycast(_clickPosision, Vector2.zero);

            // Debug.Log($"Position ytka {transform.position}");
            // Debug.Log($"Position click {_clickPosision}");

            // var distanceClick = Math.Sqrt(
            //     Math.Pow(transform.position.x - _clickPosision.x, 2) + 
            //     Math.Pow(transform.position.y - _clickPosision.y, 2));

            // Debug.Log(distanceClick);

            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(new Vector2(-_clickPosision.x, -_clickPosision.y) * _directionPower);
            if (!_moveSound.isPlaying)
                _moveSound.Play();
            // _rigidbody.AddForce(Vector2.up * _jumpPower);
        }

        private Vector3 GetPositionPlayer()
        {
            return transform.position;
        }
    }
}

