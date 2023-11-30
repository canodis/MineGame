using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public FixedJoystick Joystick;
    public float rotationSpeed;

    private float horizontalInput;
    private float verticalInput;

    void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        horizontalInput = Joystick.Horizontal;
        verticalInput = Joystick.Vertical;

        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }
}
