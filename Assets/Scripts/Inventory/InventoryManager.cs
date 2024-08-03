using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    public Transform ItemContent;
    public GameObject InventoryItem;
    public Toggle EnableRemove;
    public InventoryItemController[] InventoryItems;

    private bool inventoryNeedsUpdate = false;  // Rastreamento de atualização do inventário

    private void Awake()
    {
        Instance = this;
        Debug.Log("InventoryManager instance created");
    }

    private void Update()
    {
        // Verificar se o inventário precisa ser atualizado
        if (inventoryNeedsUpdate)
        {
            Debug.Log("Updating inventory list");
            ListItems();
            inventoryNeedsUpdate = false;  // Redefinir o estado de atualização do inventário
        }
    }

    public void Add(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Add: item is null");
            return;
        }
        Items.Add(item);
        inventoryNeedsUpdate = true;  // Definir que o inventário precisa ser atualizado
        Debug.Log("Item added: " + item.itemName);
    }

    public void Remove(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Remove: item is null");
            return;
        }

        if (Items.Contains(item))
        {
            Items.Remove(item);
            inventoryNeedsUpdate = true;  // Definir que o inventário precisa ser atualizado
            Debug.Log("Item removed: " + item.itemName);
        }
        else
        {
            Debug.LogError("Remove: Item not found in inventory: " + item.itemName);
        }
    }

    public void ListItems()
    {
        Debug.Log("Listing items in inventory");

        // Clean
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var RemoveButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            if (itemName == null)
            {
                Debug.LogError("ListItems: ItemName is null");
                continue;
            }
            if (itemIcon == null)
            {
                Debug.LogError("ListItems: ItemIcon is null");
                continue;
            }
            if (RemoveButton == null)
            {
                Debug.LogError("ListItems: RemoveButton is null");
                continue;
            }

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;

            // Configurar a visibilidade do botão de remover conforme o estado de EnableRemove
            RemoveButton.gameObject.SetActive(EnableRemove.isOn);

            // Configurar o botão de remover para chamar o método de remoção com o item correto
            RemoveButton.onClick.RemoveAllListeners();
            Item localItem = item; // Usar uma variável local para capturar o item correto
            RemoveButton.onClick.AddListener(() => {
                if (localItem == null)
                {
                    Debug.LogError("RemoveButton click: localItem is null");
                }
                else
                {
                    Debug.Log("RemoveButton clicked for item: " + localItem.itemName);
                    Remove(localItem);  // Chama o método Remove diretamente
                }
            });
        }

        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        Debug.Log("EnableItemsRemove called. EnableRemove is on: " + EnableRemove.isOn);
        foreach (Transform item in ItemContent)
        {
            var RemoveButton = item.Find("RemoveButton").gameObject;
            // Ajustar a visibilidade do botão conforme o estado de EnableRemove
            RemoveButton.SetActive(EnableRemove.isOn);
            Debug.Log("Remove button " + (EnableRemove.isOn ? "activated" : "deactivated") + " for item: " + item.Find("ItemName").GetComponent<Text>().text);
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
            Debug.Log("Inventory item set: " + Items[i].itemName);
        }
    }
}
