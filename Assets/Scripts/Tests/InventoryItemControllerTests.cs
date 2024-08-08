using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;

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
        testItem.name = "Test Item";
        inventoryItemController.AddItem(testItem);
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(gameObject);
        Object.Destroy(testItem);
    }

    [UnityTest]
    public IEnumerator RemoveItem_RemovesItemFromInventory()
    {
        InventoryManager.Instance.Add(testItem);
        Assert.IsTrue(InventoryManager.Instance.Contains(testItem));
        inventoryItemController.RemoveItem();
        yield return null;
        Assert.IsFalse(InventoryManager.Instance.Contains(testItem));
    }

    [Test]
    public void AddItem_AddsNewItem()
    {
        Item newItem = ScriptableObject.CreateInstance<Item>();
        newItem.name = "New Test Item";
        inventoryItemController.AddItem(newItem);
        Assert.AreEqual(newItem, inventoryItemController.GetItem());
        Object.Destroy(newItem);
    }
}
