using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;


public class TextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    
{
    public TextMeshProUGUI buttonText; // Ссылка на компонент Text
    public Color highlightColor = Color.yellow; // Цвет выделения текста
    private Color originalColor; // Исходный цвет текста
    void Start()
    {
        if (buttonText != null)
        {
            originalColor = buttonText.color; // Сохраняем исходный цвет текста
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = highlightColor; // Меняем цвет текста при наведении
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = originalColor; // Возвращаем исходный цвет текста
        }
    }
}
