using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatScript : MonoBehaviour
{
    public ThirdPersonController thirdPersonControllerScript;
    public float moveSpeed;
    public Animator playerAnimator;
    public bool trueIdleFlag;

    private float idleTimer = 0f;
    private float idleThreshold = 3f;

    private CharacterController characterController;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        trueIdleFlag = false;
    }

    void Update()
    {
        // Get horizontal movement speed (ignore Y axis to avoid gravity affecting it)
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
}
