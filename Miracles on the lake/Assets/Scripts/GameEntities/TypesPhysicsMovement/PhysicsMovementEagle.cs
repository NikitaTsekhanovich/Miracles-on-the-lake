using System;
using UnityEngine;

namespace GameEntities.TypesPhysicsMovement
{
    public class PhysicsMovementEagle : PhysicsMovementEntity
    {
        [SerializeField] private Transform _directionPoint;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Vector3 _positionPlayer;
        private float _angle;

        public static Func<Vector3> GetPositionPlayer;

        public void DoFly()
        {
            _positionPlayer = (Vector3)GetPositionPlayer?.Invoke();
            CalculateAngle();

            _directionPoint.transform.position = _positionPlayer;
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _directionPoint.transform.position, Time.deltaTime  * _speed);
        }

        private void CalculateAngle()
        {
            var directionVector = new Vector2(_positionPlayer.x - transform.position.x, _positionPlayer.y - transform.position.y);

            _angle = Vector2.Angle(directionVector, new Vector2(1, 0));

            if (_angle <= 90f)
                transform.rotation = Quaternion.Euler(0, 180f, _angle);
            else 
                transform.rotation = Quaternion.Euler(0, 0, 180f - _angle);
        }

        public void WillTurnAround()
        {
            _spriteRenderer.flipX = true;
            _directionPoint.transform.localPosition = (-1) * _directionPoint.transform.localPosition;
        }
    }
}

