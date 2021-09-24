using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    
    private PlayerController playerController;
    private float xMove = 0f;
    private bool isJumping = false;
    
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        xMove = Input.GetAxisRaw("Horizontal") * movementSpeed;

        if (Input.GetButtonDown("Jump")) isJumping = true;
    }
    private void FixedUpdate()
    {
        playerController.Move(xMove * Time.fixedDeltaTime, isJumping);
        isJumping = false;
    }
}
