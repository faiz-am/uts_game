using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerTPS : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 2f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -20f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator animator;

    private CharacterController controller;

    private Vector2 moveInput;

    private float verticalVelocity;

    private bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GroundCheck();

        Movement();

        Jump();

        Gravity();

        UpdateAnimator();
    }

    private void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundMask
        );

        Debug.Log("Grounded: " + isGrounded);
    }

    private void Movement()
    {
        moveInput = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            moveInput.y = 1;

        if (Keyboard.current.sKey.isPressed)
            moveInput.y = -1;

        if (Keyboard.current.aKey.isPressed)
            moveInput.x = -1;

        if (Keyboard.current.dKey.isPressed)
            moveInput.x = 1;

        Vector3 move = new Vector3(
            moveInput.x,
            0,
            moveInput.y
        );

        if (move.magnitude > 0.1f)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection =
                camForward * move.z +
                camRight * move.x;

            Quaternion targetRotation =
                Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );

            controller.Move(
                moveDirection.normalized *
                moveSpeed *
                Time.deltaTime
            );

            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    private void Jump()
    {
        if (
            Keyboard.current.spaceKey.wasPressedThisFrame
            && isGrounded
        )
        {
            Debug.Log("JUMP!");

            verticalVelocity =
                Mathf.Sqrt(jumpHeight * -2f * gravity);

            animator.SetTrigger("jump");
        }
    }

    private void Gravity()
    {
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 move = new Vector3(
            0,
            verticalVelocity,
            0
        );

        controller.Move(move * Time.deltaTime);
    }

    private void UpdateAnimator()
    {
        animator.SetBool("isGrounded", isGrounded);

        animator.SetFloat("yVelocity", verticalVelocity);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundDistance
        );
    }
}