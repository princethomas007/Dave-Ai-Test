using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    PlayerInputActions inputActions;

    Animator animator;
    int isWalkingHash;

    CharacterController characterController;

    bool isWalking;


    [SerializeField] float moveSpeed = 2;
    [SerializeField] float rotateSpeed = 180;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions?.Disable();
    }




    private void Start()
    {
        inputActions.PlayerControls.Movement.started += Movement_started;
        inputActions.PlayerControls.Movement.canceled += Movement_canceled;
    }

    private void Movement_started(InputAction.CallbackContext obj)
    {
        animator.SetBool(isWalkingHash, true);
        isWalking = true;
    }

    private void Movement_canceled(InputAction.CallbackContext obj)
    {
        animator.SetBool(isWalkingHash, false);
        isWalking = false;
    }




    private void Update()
    {
        if (isWalking)
        {
            Vector2 moveAxis = inputActions.PlayerControls.Movement.ReadValue<Vector2>();

            Vector3 moveDirection = new Vector3(moveAxis.x, 0, moveAxis.y);

            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            if (moveDirection.sqrMagnitude == 0)
                return;

            var rotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);


        }
    }



}
