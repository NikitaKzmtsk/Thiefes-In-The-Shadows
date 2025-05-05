using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2 : MonoBehaviour
{
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _body;
    [SerializeField] private float _horizontalSens = 9.0f;
    [SerializeField] private float _verticalSens = 9.0f;
    [SerializeField] private float _minVerticalAngle = -45.0f;
    [SerializeField] private float _maxVerticalAngle = 45.0f;

    private float _mousePositionX = 0f;
    private float _mousePositionY = 0f;

    private void Awake()
    {
        _mousePositionX = transform.eulerAngles.y;
        _mousePositionY = transform.eulerAngles.x;
    }

    private void Update()
    {
        LookInput();
    }

    private void LookInput()
    {
        // ��������� ��������������� ��������
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _mousePositionX += _horizontalSens * Time.deltaTime; // ������� ������
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _mousePositionX -= _horizontalSens * Time.deltaTime; // ������� �����
        }

        // ��������� ������������� �������
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _mousePositionY += _verticalSens * Time.deltaTime; // ������ �����
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _mousePositionY -= _verticalSens * Time.deltaTime; // ������ ����
        }

        // ����������� ������������� �������
        _mousePositionY = Mathf.Clamp(_mousePositionY, _minVerticalAngle, _maxVerticalAngle);

        _head.localEulerAngles = new Vector3(_mousePositionY, 0, 0); // ���������� ������� � ������
        _body.eulerAngles = new Vector3(0, _mousePositionX, 0); // ���������� �������� � ����
    }
}
