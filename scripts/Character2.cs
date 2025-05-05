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
        // Обработка горизонтального поворота
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _mousePositionX += _horizontalSens * Time.deltaTime; // Поворот вправо
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _mousePositionX -= _horizontalSens * Time.deltaTime; // Поворот влево
        }

        // Обработка вертикального наклона
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _mousePositionY += _verticalSens * Time.deltaTime; // Наклон вверх
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            _mousePositionY -= _verticalSens * Time.deltaTime; // Наклон вниз
        }

        // Ограничение вертикального наклона
        _mousePositionY = Mathf.Clamp(_mousePositionY, _minVerticalAngle, _maxVerticalAngle);

        _head.localEulerAngles = new Vector3(_mousePositionY, 0, 0); // Применение наклона к голове
        _body.eulerAngles = new Vector3(0, _mousePositionX, 0); // Применение поворота к телу
    }
}
