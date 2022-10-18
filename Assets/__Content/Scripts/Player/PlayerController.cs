using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __Content.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private float playerMoveSpeed = 7f;
        
        [SerializeField]
        private float playerTurnSpeed = 80f;
        
        [SerializeField]
        private float playerLookUpSpeed = 80f;
        
        [SerializeField]
        private float gravity = -9.81f;
        
        [SerializeField]
        private float groundDistance = .4f;
        
        [SerializeField]
        private PlayerMovement playerMovement;
        
        [SerializeField]
        private MouseLook mouseLook;

        private PlayerInput playerInput;
        private CharacterController controller;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            playerMovement.Setup(playerInput, controller, playerMoveSpeed, gravity, groundDistance, transform);
            mouseLook.Setup(playerInput, playerTurnSpeed, playerLookUpSpeed, transform);
        }
    }
}
