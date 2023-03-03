using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;
using Cinemachine;
using StarterAssets;
public class enableOnStart : NetworkBehaviour
{
    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        PlayerInput playerInput = GetComponent<PlayerInput>();
        CharacterController characterController = GetComponent<CharacterController>();
        ThirdPersonController thirdPersonController = GetComponent<ThirdPersonController>();
        playerInput.enabled = true;
        characterController.enabled = true;
        thirdPersonController.enabled = true;
    }
}
