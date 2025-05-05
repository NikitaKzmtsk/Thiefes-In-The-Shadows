using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;


public class TextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    
{
    public TextMeshProUGUI buttonText; // ������ �� ��������� Text
    public Color highlightColor = Color.yellow; // ���� ��������� ������
    private Color originalColor; // �������� ���� ������
    void Start()
    {
        if (buttonText != null)
        {
            originalColor = buttonText.color; // ��������� �������� ���� ������
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = highlightColor; // ������ ���� ������ ��� ���������
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = originalColor; // ���������� �������� ���� ������
        }
    }
}
