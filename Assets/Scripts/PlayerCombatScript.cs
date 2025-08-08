using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCombatScript : MonoBehaviour
{
    public ThirdPersonController thirdPersonControllerScript;
    public float moveSpeed;
    public Animator playerAnimator;
    public bool trueIdleFlag;

    private float idleTimer = 0f;
    private float idleThreshold = 3f;

    private CharacterController characterController;

  
    public CinemachineVirtualCamera cameraA;
    public CinemachineVirtualCamera cameraB;

    private bool usingCameraA = true;

    public void switchCamera()
    {
        if (cameraA != null && cameraB != null)
        {
            if (usingCameraA)
            {
                cameraA.Priority = 0;
                cameraB.Priority = 10;
            }
            else
            {
                cameraA.Priority = 10;
                cameraB.Priority = 0;
            }

            usingCameraA = !usingCameraA;
        }
    }

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        trueIdleFlag = false;
    }

    void Update()
    {
        moveSpeed = new Vector3(characterController.velocity.x, 0f, characterController.velocity.z).magnitude;

        if (moveSpeed < 0.1f)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleThreshold && !trueIdleFlag)
            {
                trueIdleFlag = true;
                playerAnimator.SetBool("IsTrueIdle", true);
            }
        }
        else
        {
            idleTimer = 0f;
            if (trueIdleFlag)
            {
                trueIdleFlag = false;
                playerAnimator.SetBool("IsTrueIdle", false);
            }
        }
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CameraSwitchTrigger"))
        {
            switchCamera();
        }
    }
}
