using Asteroids.Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Input
{
    public class ShipInputController : MonoBehaviour
    {
        private InputActions input;
        private ShipModel model;

        public void Init(ShipModel model)
        {
            this.model = model;
        }

        private void Awake()
        {
            input = new InputActions();

            input.PlayerShip.FisrtShootButton.performed += OnFirstGunShoot;
            input.PlayerShip.SecondShootButton.performed += OnSecondGunShoot;            
        }

        void Update()
        {
            if(input.PlayerShip.Accelerate.phase == InputActionPhase.Performed)
            {
                model.Accelerate(Time.deltaTime);
            }
            TryRotate();
        }

        private void OnDestroy()
        {
            input.PlayerShip.FisrtShootButton.performed -= OnFirstGunShoot;
            input.PlayerShip.FisrtShootButton.performed -= OnSecondGunShoot;
        }

        private void OnEnable()
        {
            input.Enable();
        }

        private void OnDisable()
        {
            input.Disable();
        }

        private void OnFirstGunShoot(InputAction.CallbackContext obj)
        {
            model.ShootGun();
        }

        private void OnSecondGunShoot(InputAction.CallbackContext obj)
        {
            model.ShootLaser();
        }

        private void TryRotate()
        {
            float direction = input.PlayerShip.Rotate.ReadValue<float>();

            if (direction != 0)
                model.Rotate(direction, Time.deltaTime);
        }
    }
}

