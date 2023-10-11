using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMotor : MonoBehaviour
{
    public static float speed = 5f;

    private Rigidbody2D myRigidbody;

    private Vector2 velocity = Vector2.zero;

    private InputsManager inputs;
    private InputAction moveAction;
    private Animator animator;
    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        manager = GameManager.GetInstance();
        inputs = InputsManager.instance;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        Vector2 vMoveValue = inputs.GetMovingInputs().normalized;

        velocity = vMoveValue * speed;

        UpdateanimationAndMove();
    }

    void UpdateanimationAndMove()
    {
        if (velocity != Vector2.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", velocity.x);
            animator.SetFloat("moveY", velocity.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(transform.position + new Vector3(velocity.x * Time.fixedDeltaTime, velocity.y * Time.fixedDeltaTime, 0));
    }

}
