using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __Content.Scripts.Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        [Header("Mask")]
        [SerializeField]
        private LayerMask groundMask;

        [Space(10)]
        [Header("References")]
        [SerializeField]
        private Transform groundChecker;

        private PlayerInput playerInput;
        private CharacterController controller;
        private float playerMoveSpeed;
        private float gravity;
        private Transform playerBody;
        private Vector3 velocity;
        private float groundDistance;

        private bool isOn;
        private bool isGrounded;

        private void Update()
        {
            if (!isOn || !IsOwner)
                return;

            var moveInput = playerInput.actions["Move"].ReadValue<Vector2>();

            var moveX = moveInput.x;
            var moveZ = moveInput.y;

            var move = playerBody.right * moveX + transform.forward * moveZ;
            controller.Move(move * (playerMoveSpeed * Time.deltaTime));

            isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0f)
                velocity.y = 0f;

            velocity.y += gravity;
            controller.Move(velocity * (Time.deltaTime * Time.deltaTime));
        }

        public void Setup(PlayerInput playerInput, CharacterController controller, float playerMoveSpeed, float gravity, float groundDistance, Transform playerBody)
        {
            this.playerInput = playerInput;
            this.controller = controller;
            this.playerMoveSpeed = playerMoveSpeed;
            this.gravity = gravity;
            this.groundDistance = groundDistance;
            this.playerBody = playerBody;
        }
        
        public void On()
        {
            isOn = true;
        }
        
        public void Off()
        {
            isOn = false;
        }
    }
}