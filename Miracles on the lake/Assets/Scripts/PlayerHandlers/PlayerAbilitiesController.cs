using AbilityControllers;
using UnityEngine;

namespace PlayerHandlers
{
    public class PlayerAbilitiesController : MonoBehaviour
    {
        [SerializeField] private AnimationsController _animationsController;

        private void OnEnable()
        {
            AbilityController.OnShield += CreateShield;
            AbilityController.OnSmallPlayer += MakeSmall;
        }

        private void OnDisable()
        {
            AbilityController.OnShield -= CreateShield;
            AbilityController.OnSmallPlayer -= MakeSmall;
        }

        private void CreateShield()
        {
            _animationsController.MakeShieldAbilityAnimation();
        }

        private void MakeSmall()
        {
            _animationsController.MakeSmallAnimation();
        }
    }
}

