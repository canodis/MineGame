using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour, IDataPersistance
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
    public bool canMove = false;
    private float horizontalInput;
    private float verticalInput;
    public Vector3 moveDirection;

    [Header("Ground Check")]
    public LayerMask whatIsGround;
    [SerializeField] private float gravityScale = -15.81f;
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
        if (!canMove)
            return;
        if (grounded && moveDirection.y < 0)
        {
            moveDirection.y = 0f;
            gravity = gravityScale;
        }
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        moveDirection.y += gravity * Time.deltaTime;
        controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }

    public void takeResource()
    {
        resourceDetector.damageToResource();
    }

    public void takeTree()
    {
        resourceDetector.damageToTree();
    }

    public void LoadData(GameData data)
    {
        transform.position = data.PlayerPosition;
        StartCoroutine(TransitionToLoadedPosition(data.PlayerPosition));
    }

    public void SaveData(ref GameData data)
    {
        data.PlayerPosition = transform.position;
    }

    IEnumerator TransitionToLoadedPosition(Vector3 targetPosition)
    {
        float transitionDuration = 1.0f; // Geçiş süresi
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / transitionDuration);
            transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            yield return null;
        }

        transform.position = targetPosition; // Kesinlikle hedef konuma ayarlayın
        canMove = true; // Hareket etmeye izin ver
    }
}