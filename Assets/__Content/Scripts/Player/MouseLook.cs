using UnityEngine;
using UnityEngine.InputSystem;

namespace __Content.Scripts.Player
{
    public class MouseLook : GameComponent
    {
        private PlayerInput playerInput;
        private float playerTurnSpeed;
        private float playerLookUpSpeed;
        private float xRotation;
        private Transform playerBody;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (!IsOn)
                return;
            
            var lookAroundInput = playerInput.actions["LookAround"].ReadValue<Vector2>();

            var mouseX = lookAroundInput.x * (playerTurnSpeed * Time.deltaTime);
            var mouseY = lookAroundInput.y * (playerLookUpSpeed * Time.deltaTime);

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        
        public void Setup(PlayerInput playerInput, float playerTurnSpeed, float playerLookUpSpeed, Transform playerBody)
        {
            this.playerInput = playerInput;
            this.playerTurnSpeed = playerTurnSpeed;
            this.playerLookUpSpeed = playerLookUpSpeed;
            this.playerBody = playerBody;
        }
    }
}
