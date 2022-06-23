using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    [SerializeField] float pSpeed = 1f;
    [SerializeField] float pJumpForce = 1f;
    [SerializeField] Transform groundSensor;
    [SerializeField] LayerMask groundLayer;
    private bool facingDirection = true;
    private float horizontal;
    private Rigidbody2D pBody;
    private Animator pAnimator;
 

    void Start() {
        pAnimator = GetComponent<Animator>();
        pBody = GetComponent<Rigidbody2D>();
    }//end Start

    void Update() {
        pBody.velocity = new Vector2(horizontal * pSpeed, pBody.velocity.y);

        if (!facingDirection && horizontal > 0f) {
            Flip();
        } else if (facingDirection && horizontal < 0f) {
            Flip();
        }//end flip sprite in opposide directions
    }//end Fixed Update

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundSensor.position, 0.2f, groundLayer);
    }//end check if player is grounded

    private void Flip() {
        facingDirection = !facingDirection;
        Vector3 dummyScale = transform.localScale;
        dummyScale.x = dummyScale.x * -1f;
        transform.localScale = dummyScale;
    }//end flip sprite

    public void Movement(InputAction.CallbackContext context) {
        horizontal = context.ReadValue<Vector2>().x;
    }//end Movement

    public void Jump(InputAction.CallbackContext context) {
        if(context.performed && IsGrounded()) {
            pBody.velocity = new Vector2(pBody.velocity.x, pJumpForce);
        }//end if grounded to jump

        if(context.canceled && pBody.velocity.y > 0f) {
            pBody.velocity = new Vector2(pBody.velocity.x, pBody.velocity.y * 0.5f);
        }//end short hop if button tapped quickly
    }//end Jump
}//end PlayerController
