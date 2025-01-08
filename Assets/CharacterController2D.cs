using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))] //this will automatically create a rigid body component if we create any object with this script
public class CharacterController2D : MonoBehaviour
{
    private Rigidbody2D _rigidBody2d;
    [SerializeField] float speed = 2f; // serializefield to show this line in the inspector
    Vector2 motionVector;
    Animator animator;
    private Vector2 lastMotionVector;

    void Awake()
    {
        _rigidBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        motionVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        
        animator.SetFloat("horizontal", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("vertical", Input.GetAxisRaw("Vertical"));
// Lines 27 - 40 are arya. If you find a problem regarding movement just delete those lines. I dont think there should be a problem
        if (motionVector != Vector2.zero)
        {
            lastMotionVector = motionVector;
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // Set last movement direction for idle animation
        animator.SetFloat("lastHorizontal", lastMotionVector.x);
        animator.SetFloat("lastVertical", lastMotionVector.y);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidBody2d.velocity = motionVector * speed;
    }
}
