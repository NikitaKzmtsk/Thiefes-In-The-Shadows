using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quickslot2 : MonoBehaviour
{
    // Объект у которого дети являются слотами
    public Transform quickslotParent;
    public InventoryManager2 inventoryManager;
    public int currentQuickslotID = 0;
    public Sprite selectedSprite;
    public Sprite notSelectedSprite;

    // Update is called once per frame
    void Update()
    {
        // Используем цифры 7, 8, 9, 0 для выбора слотов
        if (Input.GetKeyDown(KeyCode.Alpha7) && quickslotParent.childCount > 0)
        {
            ChangeSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && quickslotParent.childCount > 1)
        {
            ChangeSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && quickslotParent.childCount > 2)
        {
            ChangeSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && quickslotParent.childCount > 3)
        {
            ChangeSlot(3);
        }
    }

    private void ChangeSlot(int newSlotID)
    {
        // Убираем выделение с текущего слота
        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = notSelectedSprite;

        // Обновляем текущий слот
        currentQuickslotID = newSlotID;

        // Выделяем новый слот
        quickslotParent.GetChild(currentQuickslotID).GetComponent<Image>().sprite = selectedSprite;

        // Здесь можно добавить код для использования предмета из выбранного слота
    }
}
