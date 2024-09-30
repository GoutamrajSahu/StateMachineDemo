using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFreeMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public Rigidbody2D rb;
    private Vector3 moveInput;

    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * moveSpeed;
    }
}
