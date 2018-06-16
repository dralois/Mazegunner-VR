using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerMovement : NetworkBehaviour
{
    public int playerNumber = 1;

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
    public bool borderCheck;
    public Vector3 border1;
    public Vector3 border2;

    CharacterController controller;
    Animator animator;
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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
        print(animator.GetBool("walking"));
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
        Vector3 movedir = new Vector3(
        Util.GetInputAxisSafe(xMovementInputString) * movementSpeed.x * (invertMovementX ? -1 : 1),
        0,
        Util.GetInputAxisSafe(zMovementInputString) * movementSpeed.z * (invertMovementZ ? -1 : 1));

        if (Mathf.Abs(movedir.magnitude) > 0)
        {
            animator.SetBool("running", true);

            animator.SetBool("running backwards", movedir.z < 0);

        }
        else
        {
            animator.SetBool("running backwards", false);
            animator.SetBool("running", false);
        }

        Move(transform.TransformDirection(movedir));

        //Rotation
        gameObject.transform.Rotate(new Vector3(
        Util.GetInputAxisSafe(xRotationInputString) * rotationSpeed.x * (inverRotationX ? -1 : 1),
        Util.GetInputAxisSafe(yRotationInputString) * rotationSpeed.y * (invertRotationY ? -1 : 1),
        Util.GetInputAxisSafe(zRotationInputString) * rotationSpeed.z * (invertRotationZ ? -1 : 1)));

        //Jumping

    }

    private void Move(Vector3 direction)
    {
        controller.Move(direction * Time.deltaTime);
    }
}
