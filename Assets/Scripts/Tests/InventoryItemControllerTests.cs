using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using Moq;

public class InventoryItemControllerTests
{
    private GameObject gameObject;
    private InventoryItemController inventoryItemController;
    private Item testItem;
    private Mock<IInventoryManager> mockInventoryManager;

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

        // Configura o Mock para o InventoryManager
        mockInventoryManager = new Mock<IInventoryManager>();
        InventoryManager.Instance = mockInventoryManager.Object;
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(gameObject);
        Object.Destroy(testItem);
    }

    [Test]
    public void RemoveItem_CallsRemoveOnInventoryManagerAndDestroysGameObject()
    {
        // Configura o Item no InventoryItemController
        inventoryItemController.AddItem(testItem);

        // Simula a chamada do método RemoveItem
        inventoryItemController.RemoveItem();

        // Verifica se o método Remove foi chamado no InventoryManager
        mockInventoryManager.Verify(im => im.Remove(testItem), Times.Once);

        // Verifica se o GameObject foi destruído
        Assert.IsNull(gameObject);
    }
}

public interface IInventoryManager
{
    void Remove(Item item);
}
