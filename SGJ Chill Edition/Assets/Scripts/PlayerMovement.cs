using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerMovement : NetworkBehaviour
{
    public GameObject model;
    private int playerNumber = 1;
    public float modelMaxRotationSpeed = 30;

    [Header("Movement Settings")]
    public Vector3 movementSpeed = new Vector3(5, 5, 5);

    public string xMovementInputString = "L_XAxis_";
    public string yMovementInputString = "L_YAxis_";
    public string zMovementInputString = "";

    public bool invertMovementX = false;
    public bool invertMovementY = true;
    public bool invertMovementZ = false;

    [Header("Rotation Settings")]
    public Vector3 rotationSpeed = new Vector3(5, 5, 5);

    public string xRotationInputString = "";
    public string yRotationInputString = "";
    public string zRotationInputString = "R_YAxis_";

    public bool inverRotationX = false;
    public bool invertRotationY = false;
    public bool invertRotationZ = false;

    [Header("Misc Settings")]
    private bool borderCheck;
    private Vector3 border1;
    private Vector3 border2;

    CharacterController controller;
    public Animator animator;

    public float jumpSpeed = 8;
    public float gravity = 9.8f;
    private float vSpeed = 0;

    [Header("Sprint stats")]
    public float speedBoost;
    public float sprintDuration;
    public float recoveryDebuff;
    public float recoveryTime;

    public GameObject cam;
    private float cameraAngle=0;


    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        PlayerControl();
        PassiveMovement();
    }

    private void PassiveMovement()
    {
        //BorderChecks
        if (borderCheck)
            transform.position = Util.SetInBetween(transform.position, border1, border2);

    }

    private void PlayerControl()
    {
        //Movement
        Vector2 moveDir2 = new Vector2(Util.GetInputAxisSafe(xMovementInputString) * movementSpeed.x * (invertMovementX ? -1 : 1),
            Util.GetInputAxisSafe(zMovementInputString) * movementSpeed.z * (invertMovementZ ? -1 : 1));

        Vector3 movedir = new Vector3(
        moveDir2.x,
        0,
        moveDir2.y);

        animator.SetBool("running", Mathf.Abs(movedir.magnitude) > 0);
        animator.SetBool("backward", moveDir2.y < 0);
        animator.SetFloat("running direction", moveDir2.normalized.x);


        Vector3 vel = transform.TransformDirection(movedir);

        //Jumping
        if (controller.isGrounded)
        {
            vSpeed = 0; // grounded character has vSpeed = 0...
            if (Input.GetKeyDown("space"))
            { // unless it jumps:
                vSpeed = jumpSpeed;
            }
        }

        // apply gravity acceleration to vertical speed:
        vSpeed -= gravity * Time.deltaTime;
        vel.y = vSpeed; // include vertical speed in vel
                        // convert vel to displacement and Move the character:
        controller.Move(vel * Time.deltaTime);

        //Rotation
        gameObject.transform.Rotate(new Vector3(
        0,
        Util.GetInputAxisSafe(yRotationInputString) * rotationSpeed.y * (invertRotationY ? -1 : 1),
        Util.GetInputAxisSafe(zRotationInputString) * rotationSpeed.z * (invertRotationZ ? -1 : 1)));

        cameraAngle = (Util.GetInputAxisSafe(xRotationInputString) * rotationSpeed.x * (inverRotationX ? -1 : 1)) + cameraAngle;

        cameraAngle = Mathf.Clamp(cameraAngle, -80, 80);

        cam.transform.localRotation = Quaternion.Euler(new Vector3(cameraAngle, 0,0));

    }

    private void Move(Vector3 direction)
    {
        controller.Move(direction * Time.deltaTime);
    }


}
