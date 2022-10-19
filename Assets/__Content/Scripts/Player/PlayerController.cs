using System;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace __Content.Scripts.Player
{
    public class PlayerController : NetworkBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private float playerMoveSpeed = 7f;

        [SerializeField]
        private float playerTurnSpeed = 80f;

        [SerializeField]
        private float playerLookUpSpeed = 80f;

        [Space(10)]
        [Header("Gravity")]
        [SerializeField]
        private float gravity = -9.81f;

        [SerializeField]
        private float groundDistance = .001f;

        [Space(10)]
        [Header("References")]
        [SerializeField]
        private Camera playerCamera;

        [SerializeField]
        private PlayerMovement playerMovement;

        [SerializeField]
        private MouseLook mouseLook;

        private PlayerInput playerInput;
        private CharacterController controller;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (!IsOwner)
            {
                Destroy(playerCamera);
                return;
            }

            controller = GetComponent<CharacterController>();
            playerInput = GetComponent<PlayerInput>();

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