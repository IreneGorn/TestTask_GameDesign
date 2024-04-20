using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    [SerializeField] private CharacterController _controller;
    
    [SerializeField] private float _speed = 6f;
    
    [SerializeField] private float _turnSmoothTime = 0.1f;
    
    [SerializeField] private Transform _cam;

    private float _turnSmoothVelocity;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                _turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * _speed * Time.deltaTime);
            
            if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Stop running")) // Проверяем, не проигрывается ли анимация окончания бега
            {
                _animator.SetBool("IsRunning", true); // Устанавливаем параметр IsRunning в true
            }
        }
        else
        {
            _animator.SetBool("IsRunning", false);
        }
        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Stop running")) // Проверяем, проигрывается ли анимация окончания бега
            {
                _animator.Play("Start running"); // Запускаем анимацию начала бега
            }
            else
            {
                _animator.SetTrigger("StartRunTrigger"); // Устанавливаем триггер StartRunTrigger, когда нажата клавиша передвижения
            }
        }
    }
}
