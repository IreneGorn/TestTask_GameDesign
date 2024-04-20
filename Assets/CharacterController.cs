using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    
    private bool isRunning;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement.Normalize();

        transform.Translate(movement * moveSpeed * Time.deltaTime);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }

        bool newIsRunning = horizontal != 0f || vertical != 0f;

        if (newIsRunning != isRunning)
        {
            isRunning = newIsRunning;

            if (isRunning)
            {
                animator.SetTrigger("StartRunning");
            }
            else
            {
                animator.SetTrigger("StopRunning");
            }
        }

        animator.SetBool("IsRunning", isRunning);
    }
}
