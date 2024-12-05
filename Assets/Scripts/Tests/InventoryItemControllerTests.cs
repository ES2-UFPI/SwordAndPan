using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemControllerTests
{
    private GameObject gameObject;
    private InventoryItemController inventoryItemController;
    private Item testItem;
    
    [SetUp]
    public void SetUp()
    {
        gameObject = new GameObject();
        inventoryItemController = gameObject.AddComponent<InventoryItemController>();
        inventoryItemController.RemoveButton = gameObject.AddComponent<Button>();

        testItem = ScriptableObject.CreateInstance<Item>();
        testItem.id = 1;
        testItem.itemName = "Test Item";
        testItem.value = 100;

        var inventoryManagerObject = new GameObject();
        InventoryManager.Instance = inventoryManagerObject.AddComponent<InventoryManager>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(gameObject);
        Object.Destroy(testItem);
        Object.Destroy(InventoryManager.Instance.gameObject);
        InventoryManager.Instance = null;
    }

    [Test]
    public void RemoveItem_CallsRemoveOnInventoryManagerAndDestroysGameObject()
    {
        inventoryItemController.AddItem(testItem);
        inventoryItemController.RemoveItem();
        Assert.IsFalse(InventoryManager.Instance.Items.Contains(testItem));
        Assert.IsNull(gameObject);
    }
}
