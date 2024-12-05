using NUnit.Framework;
using UnityEngine;

public class ItemControllerTests
{
    private GameObject gameObject;
    private ItemController itemController;
    private Item testItem;

    [SetUp]
    public void SetUp()
    {
        gameObject = new GameObject();
        itemController = gameObject.AddComponent<ItemController>();

        testItem = ScriptableObject.CreateInstance<Item>();
        testItem.id = 1;
        testItem.itemName = "Test Item";
        testItem.value = 100;
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(gameObject);
        Object.Destroy(testItem);
    }

    [Test]
    public void ItemController_AssignsItemCorrectly()
    {
        itemController.Item = testItem;
        Assert.AreEqual(testItem, itemController.Item);
    }
}
