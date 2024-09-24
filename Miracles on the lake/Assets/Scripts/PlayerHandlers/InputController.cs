using System.Collections;
using AbilityControllers;
using GameLogicControllers;
using PlayerInterface;
using UnityEngine;

namespace PlayerHandlers
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private PhysicsMovement _physicsMovement;

        private bool _lockTouch;

        private void OnEnable()
        {
            PlayerInterfaceController.OnClick += DoOnClick;
            PlayerInterfaceController.OffClick += DoOffClick;
            GameStateController.OffClick += DoOffClick;
            AbilityController.OnClick += DoOnClick;
            AbilityController.OffClick += DoOffClick;
            AnimationsController.OnClick += DoOnClick;
            AnimationsController.OffClick += DoOffClick;
        }

        private void OnDisable()
        {
            PlayerInterfaceController.OnClick -= DoOnClick;
            PlayerInterfaceController.OffClick -= DoOffClick;
            GameStateController.OffClick -= DoOffClick;
            AbilityController.OnClick -= DoOnClick;
            AbilityController.OffClick -= DoOffClick;
            AnimationsController.OnClick -= DoOnClick;
            AnimationsController.OffClick -= DoOffClick;
        }

        private void Update()
        {
            CheckClickInput();
        }

        private void CheckClickInput()
        {
            if (!_lockTouch)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    _physicsMovement.Movement();
                }
            }
        }

        private void DoOnClick()
        {
            StartCoroutine(Waiter(false));
        }

        private void DoOffClick()
        {   
            _lockTouch = true;
        }

        private IEnumerator Waiter(bool lockTouch)
        {
            yield return new WaitForSeconds(0f);
            _lockTouch = lockTouch;
        }
    }
}

