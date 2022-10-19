using System;
using System.Collections;
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
        private float groundDistance = .001f;
        
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

            StartCoroutine(EnableControlsForFirstTime());
        }

        private IEnumerator EnableControlsForFirstTime()
        {
            yield return new WaitForSeconds(1f); // New input system has a bug that provides negatives values on y axis at firsts frames, we avoid it waiting one second on start
            playerMovement.On();
            mouseLook.On();
        }
    }
}
