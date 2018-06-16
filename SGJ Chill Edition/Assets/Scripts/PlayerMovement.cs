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
    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
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

        if (Mathf.Abs(movedir.magnitude) > 0)
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }

        Move(transform.TransformDirection(movedir));

        float ang = Vector2.Angle(Vector2.up, moveDir2.normalized);
        Vector3 cross = Vector3.Cross(Vector2.up, moveDir2.normalized);

        if (cross.z > 0)
            ang = 360 - ang;

        if (moveDir2.magnitude == 0)
        {
            ang = 0;
        }

        model.transform.localRotation = Quaternion.RotateTowards(model.transform.localRotation,Quaternion.Euler(new Vector3(0, ang, 0)),modelMaxRotationSpeed * Time.deltaTime);

        //Rotation
        gameObject.transform.Rotate(new Vector3(
        Util.GetInputAxisSafe(xRotationInputString) * rotationSpeed.x * (inverRotationX ? -1 : 1),
        Util.GetInputAxisSafe(yRotationInputString) * rotationSpeed.y * (invertRotationY ? -1 : 1),
        Util.GetInputAxisSafe(zRotationInputString) * rotationSpeed.z * (invertRotationZ ? -1 : 1)));
        print(isLocalPlayer);
        //Jumping

    }

    private void Move(Vector3 direction)
    {
        controller.Move(direction * Time.deltaTime);
    }
}
