using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager2 : MonoBehaviour
{
    public Transform inventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public Camera camera1;
    public float reachDistance = 10;
    public GameObject itemPrefab; // Префаб для предмета, который будет выбрасываться
    public quickslot2 quik;




    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
    }


    void Update()
    {
        // Вычисляем среднюю точку второй половины экрана
        Vector3 screenCenter = new Vector3(Screen.width / 1.3f, Screen.height / 2, 0);

        // Создаем рейкаст из камеры в центр экрана
        Ray ray = camera1.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Physics.Raycast(ray, out hit, reachDistance))
            {
                if (hit.collider.gameObject.GetComponent<Item>() != null)
                {
                    Item item = hit.collider.gameObject.GetComponent<Item>();
                    // Проверяем, достаточно ли места в инвентаре
                    if (CanAddItems(item.item, item.amount))
                    {
                        AddItem(item.item, item.amount);
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.green);
        }

        // Новая функциональность для выбрасывания предмета
        if (Input.GetKeyDown(KeyCode.M))
        {
            DropItem();
        }
    }

    private bool CanAddItems(ItemScriptableObject _item, int _amount)
    {
        int totalCapacity = 0;

        // Считаем общую вместимость инвентаря
        foreach (InventorySlot slot in slots)
        {
            if (slot.item != null)
            {
                totalCapacity += slot.item.maxAmount;
            }
        }

        // Проверяем, достаточно ли места для добавления новых предметов
        return (totalCapacity + _amount) <= (slots.Count * _item.maxAmount);
    }

    private void DropItem()
    {
        if (quik.currentQuickslotID < slots.Count) // Убедимся, что индекс текущего слота не выходит за пределы
        {
            InventorySlot slot = slots[quik.currentQuickslotID]; // Используем текущий выбранный слот из quickslot
            if (!slot.isEmpty && slot.amount > 0) // Проверяем, что слот не пуст и количество больше 0
            {
                // Создаем объект выбрасываемого предмета
                GameObject droppedItem = Instantiate(itemPrefab, camera1.transform.position, Quaternion.identity);
                Item itemComponent = droppedItem.GetComponent<Item>();
                if (itemComponent != null)
                {
                    itemComponent.item = slot.item; // Устанавливаем предмет
                    itemComponent.amount = 1; // Устанавливаем количество предметов равным 1 (выбрасываем только один предмет)

                    // Уменьшаем количество предметов в слоте
                    slot.amount--;
                    slot.itemAmount.text = slot.amount.ToString();

                    // Проверяем, если слот стал пустым
                    if (slot.amount <= 0)
                    {
                        slot.item = null;
                        slot.isEmpty = true;
                        slot.itemAmount.text = "0"; // Обновляем текстовое поле
                        slot.SetIcon(null); // Убираем иконку
                    }
                }
            }
        }
    }


    private void AddItem(ItemScriptableObject _item, int _amount)
    {
        // Сначала пытаемся добавить к существующему предмету
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == _item && !slot.isEmpty)
            {
                // Проверяем, можно ли добавить к текущему количеству
                while (slot.amount < _item.maxAmount && _amount > 0)
                {
                    slot.amount++;
                    slot.itemAmount.text = slot.amount.ToString();
                    _amount--;
                }
                if (_amount <= 0)
                    return;
            }
        }

        // Теперь добавляем в пустой слот, если остались предметы
        foreach (InventorySlot slot in slots)
        {
            if (slot.isEmpty && _amount > 0)
            {
                int amountToAdd = Mathf.Min(_amount, _item.maxAmount); // Добавляем максимум, который можно в слот
                slot.item = _item;
                slot.amount = amountToAdd;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                slot.itemAmount.text = amountToAdd.ToString();
                _amount -= amountToAdd;

                if (_amount <= 0)
                    break;
            }
        }
    }
}
