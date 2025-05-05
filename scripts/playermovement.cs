using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _moveSpeed = 19.0f;
    [SerializeField] private float _jumpHeight = 20.0f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _runSpeedMultiplier = 2.0f;
    [SerializeField] private float _crouchSpeedMultiplier = 0.5f;
    [SerializeField] private float _sitDownSpeed = 10.0f;

    public bool isCrouching = false;
    private Vector3 _velocity;
    private bool _isGrounded;

    private void Update()
    {
        _isGrounded = _characterController.isGrounded;
        MoveInput();
    }

    private void MoveInput()
    {
        Vector3 movement = Vector3.zero;

        // Проверяем нажатие клавиш WASD
        if (Input.GetKey(KeyCode.W))
        {
            movement.z += 1; // Вперед
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.z -= 1; // Назад
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x -= 1; // Влево
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x += 1; // Вправо
        }

        movement.Normalize(); // Нормализуем вектор движения

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= _moveSpeed * _runSpeedMultiplier;
            isCrouching = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
        }

        if (isCrouching)
        {
            if (_characterController.height >= 1)
            {
                _characterController.height -= _sitDownSpeed * Time.deltaTime;
            }
            movement *= _moveSpeed * _crouchSpeedMultiplier;
        }
        else
        {
            movement *= _moveSpeed;

            if (_characterController.height <= 2)
            {
                _characterController.height += _sitDownSpeed * Time.deltaTime;
            }
        }

        movement = transform.TransformDirection(movement);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0;
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y += Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;

        _characterController.Move((movement + _velocity) * Time.deltaTime);
    }
}
