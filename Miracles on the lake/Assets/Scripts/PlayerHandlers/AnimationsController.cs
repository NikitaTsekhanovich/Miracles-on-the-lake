using UnityEngine;
using DG.Tweening;
using System;

namespace PlayerHandlers
{
    public class AnimationsController : MonoBehaviour
    {
        [SerializeField] private CapsuleCollider2D _collider;
        [SerializeField] private SpriteRenderer _playerImage;
        [SerializeField] private SpriteRenderer _shield;

        public static Action OnClick;
        public static Action OffClick;

        public void DealDamageAnimation()
        {
            _collider.enabled = false;

            OffClick?.Invoke();
            var shakePlayer = DOTween.Sequence()
                .Append(transform.DOShakePosition(1f, new Vector3(0.1f, 0.1f, 0)))
                .AppendCallback(() => OnClick?.Invoke());

            var dealDamageAnimation = DOTween.Sequence()
                .Append(_playerImage.DOColor(new Color(1f, 1f, 1f, 0.5f), 0.2f))
                .Append(_playerImage.DOColor(new Color(1f, 1f, 1f, 1f), 0.2f))
                .SetLoops(8)
                .OnComplete(() => _collider.enabled = true);
        }

        public void MakeShieldAbilityAnimation()
        {
            _collider.enabled = false;

            var maleShieldAnimation = DOTween.Sequence()
                .Append(_shield.transform.DOScale(new Vector3(0.9f, 0.8f, 0f), 1f))
                .AppendCallback(IdleShieldAnimation);
        }

        private void IdleShieldAnimation()
        {
            var idleShieldAnimation = DOTween.Sequence()
                .Append(_shield.transform.DOScale(new Vector3(0.8f, 0.7f, 0f), 1f))
                .Append(_shield.transform.DOScale(new Vector3(0.9f, 0.8f, 0f), 1f))
                .SetLoops(5)
                .OnComplete(EndShieldAnimation);
        }

        private void EndShieldAnimation()
        {
            var endShieldAnimation = DOTween.Sequence()
                .Append(_shield.transform.DOScale(new Vector3(0f, 0f, 0f), 1f))
                .AppendCallback(() => _collider.enabled = true);
        }

        public void MakeSmallAnimation()
        {
            var makeSmallAnimation = DOTween.Sequence()
                .Append(transform.DOScale( new Vector3(0.3f, 0.3f, 0.3f), 1f))
                .AppendInterval(8f)
                .Append(transform.DOScale( new Vector3(0.7f, 0.7f, 0.7f), 1f));
        }
    }
}
