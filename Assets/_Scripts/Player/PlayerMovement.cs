using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private ResourceDetector resourceDetector;
    private CharacterController controller;
    public Transform orientation;
    public FixedJoystick Joystick;


    [Header("Movement")]
    private float moveSpeed;
    public float airMultiplier;
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;


    [Header("Ground Check")]
    public LayerMask whatIsGround;
    [SerializeField] private float gravityScale = -9.81f;
    public float playerHeight;
    public bool grounded;
    private float gravity;



    private void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        resourceDetector = GetComponentInChildren<ResourceDetector>();
        gravity = gravityScale;
        moveSpeed = walkSpeed;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);
        Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.3f), Color.red);
        MyInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {

        horizontalInput = Joystick.Horizontal;
        verticalInput = Joystick.Vertical;

        anim.SetBool("moveInput", (Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)) == 0 ? false : true);
    
        if (horizontalInput != 0 || verticalInput != 0)
        {
            anim.SetBool("mining", false);
            resourceDetector.mining = false;
            anim.SetBool("felling", false);
        }
    }

    private void MovePlayer()
    {
        if (grounded && moveDirection.y < 0)
        {
            moveDirection.y = 0f;
            gravity = gravityScale;
        }
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.y += gravity * Time.deltaTime;
        if (grounded)
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        else if (!grounded)
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime * airMultiplier);
        gravity += gravityScale;
    }

    public void takeResource()
    {
        resourceDetector.damageToResource();
    }

    public void takeTree()
    {
        resourceDetector.damageToTree();
    }
}