using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    
    private void Update()
    {
        // Получаем горизонтальное и вертикальное перемещение
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
    
        // Вычисляем вектор направления движения
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement.Normalize();
    
        // Перемещаем модель
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    
        // Поворачиваем модель в направлении движения
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    
        // Управление анимацией
        if (horizontal != 0f || vertical != 0f)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("StartRunning");
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            animator.SetTrigger("StopRunning");
        }
    }
}
