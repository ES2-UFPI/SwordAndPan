using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerStatusTests
{
    private GameObject playerGameObject;
    private PlayerStatus playerStatus;

    [SetUp]
    public void SetUp()
    {
        playerGameObject = new GameObject();
        playerStatus = playerGameObject.AddComponent<PlayerStatus>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerGameObject); // Use DestroyImmediate instead of Destroy
    }

    [Test]
    public void TakeDamage_PlayerHealthDecreases()
    {
        // Arrange
        float initialHealth = 100f;
        float damage = 10f;

        // Act
        playerStatus.TakeDamage(damage);

        // Assert
        Assert.AreEqual(initialHealth - damage, playerStatus.GetCurrentHealth());
    }

    [Test]
    public void TakeDamage_PlayerIsDamaged()
    {
        // Act
        playerStatus.TakeDamage(10f);

        // Assert
        Assert.IsTrue(playerStatus.GetIsDamaged());
    }

    [Test]
    public void RestoreHealth_PlayerHealthIncreases()
    {
        // Arrange
        float damage = 10f;
        float healthRestored = 5f;
        playerStatus.TakeDamage(damage);

        // Act
        playerStatus.RestoreHealth(healthRestored);

        // Assert
        Assert.AreEqual(95f, playerStatus.GetCurrentHealth());
    }

    [Test]
    public void RestoreHealth_PlayerHealthDoesNotExceedMax()
    {
        // Act
        playerStatus.RestoreHealth(10f);

        // Assert
        Assert.AreEqual(100f, playerStatus.GetCurrentHealth());
    }

    [UnityTest]
    public IEnumerator PlayerDies_WhenHealthIsZeroOrLess()
    {
        // Arrange
        float damage = 100f;

        // Act
        playerStatus.TakeDamage(damage);

        // Assert
        LogAssert.Expect(LogType.Log, "Player has died!");
        yield return null;
    }
}
