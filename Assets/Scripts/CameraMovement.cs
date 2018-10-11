﻿using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{


    public float moveSpd = 50.0f; //WASD movement speed
    public float sensitivity = 0.07f; //Mouse angle sensitivity
    public float offscreenMovementBleedSpeed = 20.0f;
    private Vector3 prevPosition;
    private Rigidbody rb;
    void Start()
    {
        prevPosition = Input.mousePosition;

        rb = this.gameObject.AddComponent<Rigidbody>();
        rb.mass = 10;
        rb.drag = 5;
        rb.constraints = RigidbodyConstraints.FreezeRotationX 
                        | RigidbodyConstraints.FreezeRotationY 
                        | RigidbodyConstraints.FreezeRotationZ;
        rb.useGravity = true;

        SphereCollider mCollider = this.gameObject.AddComponent<SphereCollider>();
        mCollider.radius = 0.5f;
    }

    void FixedUpdate()
    {

        //TODO - weird movement when camera looking straingt up/down and we want to go further up/down because up/down vector inverts when we flipfrom angling up to down

        //calculate the difference in mouse positions from last frame to now and use it to change camera angle.
        Vector3 mouseMovement = Input.mousePosition - prevPosition;
        prevPosition = Input.mousePosition;
        Vector3 cameraMovement = (mouseMovement * sensitivity);
  

        //do the WASD movement. 
        this.rb.AddForce(GetKeyboardInput() * moveSpd * rb.mass);
    }

    private Vector3 OffscreenDirection()
    {
        Vector3 direction = Vector3.zero;
        //yaw changes
        if (Input.mousePosition.x <= 0)
        {
            direction += Vector3.left;
        }
        if (Input.mousePosition.x >= Screen.width)
        {
            direction += Vector3.right;
        }
        //pitch changes feel better at a slower speed
        if (Input.mousePosition.y <= 0)
        {
            direction += Vector3.down * 0.5f;
        }
        if (Input.mousePosition.y >= Screen.height)
        {
            direction += Vector3.up * 0.5f;
        }
        return direction;
    }

    //get the movement indicated by pressed wasd keys
    private Vector3 GetKeyboardInput()
    {
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            movement += transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement -= transform.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += transform.right;
        }
        movement.y = 0;
        return movement;
    }
}