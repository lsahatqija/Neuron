using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float inputDirection;           // X value of move vector
    private float verticalVelocity;         // Y value of move vector
    private float speed = 5.0f;
    private float gravity = 25.0f;
    private bool secondJumpAvail = false;
    private Vector3 moveVector;
    private CharacterController controller;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update () {
        IsControllerGrounded();
        inputDirection = Input.GetAxis("Horizontal") * speed;               //Get input

        if (IsControllerGrounded())
        {
            verticalVelocity = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = 10.0f;
                secondJumpAvail = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (secondJumpAvail)
                {
                    verticalVelocity = 10.0f;
                    secondJumpAvail = false;
                }
            }

            verticalVelocity -= gravity * Time.deltaTime;
        }

        moveVector = new Vector3(inputDirection, verticalVelocity, 0);
        controller.Move(moveVector * Time.deltaTime);                                //Move player; frame rate independent
	}

    private bool IsControllerGrounded()
    {
        Vector3 leftRayStart;
        Vector3 rightRayStart;

        leftRayStart = controller.bounds.center;
        rightRayStart = controller.bounds.center;

        leftRayStart.x -= controller.bounds.extents.x;
        rightRayStart.x += controller.bounds.extents.x;

        if (Physics.Raycast(leftRayStart, Vector3.down, (controller.height / 2) + 0.2f))
        {
            return true;
        }

        if (Physics.Raycast(rightRayStart, Vector3.down, (controller.height / 2) + 0.2f))
        {
            return true;
        }

        return false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (controller.collisionFlags == CollisionFlags.Sides)
        {
            secondJumpAvail = true;
        }

        // Collectibles
        switch (hit.gameObject.tag)
        {
            case "Coin":
                LevelManager.Instance.CollectCoin();
                Destroy(hit.gameObject);
                break;
            case "Winbox":
                LevelManager.Instance.Win();
                break;
        }
    }

}
