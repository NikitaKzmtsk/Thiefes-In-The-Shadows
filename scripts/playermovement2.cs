using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement2 : MonoBehaviour
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
        float deltaX = 0; // �������� �����-������
        float deltaZ = 0; // �������� ������-�����

        // �������� ���������� �� O, K, L, ;
        if (Input.GetKey(KeyCode.O))
        {
            deltaZ = 1; // �������� ������
        }
        if (Input.GetKey(KeyCode.K))
        {
            deltaX = -1; // �������� �����
        }
        if (Input.GetKey(KeyCode.L))
        {
            deltaZ = -1; // �������� �����
        }
        if (Input.GetKey(KeyCode.Semicolon)) // ���������� ����� � �������
        {
            deltaX = 1; // �������� ������
        }

        Vector3 movement = new Vector3(deltaX, 0, deltaZ).normalized;

        // �������� ������ Shift �� Enter ��� ����
        if (Input.GetKey(KeyCode.Return)) // Enter
        {
            movement *= _moveSpeed * _runSpeedMultiplier;
            isCrouching = false;
        }

        // �������� ������ Ctrl �� ������ Shift ��� ����� ��������� ����������
        if (Input.GetKeyDown(KeyCode.RightControl))
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

        // �������� ������ �� ������ Shift
        if (Input.GetKeyDown(KeyCode.RightShift) && _isGrounded)
        {
            _velocity.y += Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        _velocity.y += _gravity * Time.deltaTime;

        _characterController.Move((movement + _velocity) * Time.deltaTime);
    }
}
