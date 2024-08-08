using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{
    private GameObject playerObject;
    private Player player;
    private CharacterController characterController;

    [SetUp]
    public void SetUp()
    {
        playerObject = new GameObject();
        playerObject.AddComponent<CharacterController>();
        player = playerObject.AddComponent<Player>();
        
        // Adicione um GameInput simulado
        var gameInput = playerObject.AddComponent<GameInput>();
        playerObject.GetComponent<Player>().gameInput = gameInput;

        characterController = playerObject.GetComponent<CharacterController>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(playerObject);
    }

    [Test]
    public void Player_IsWalking_WhenMovementOccurs()
    {
        // Arrange
        var movementVector = new Vector2(1f, 0f);
        playerObject.GetComponent<GameInput>().SetMovementVector(movementVector);

        // Act
        playerObject.GetComponent<Player>().Update();

        // Assert
        Assert.IsTrue(player.IsWalking());
    }

    [Test]
    public void Player_IsNotWalking_WhenNoMovementOccurs()
    {
        // Arrange
        var movementVector = Vector2.zero;
        playerObject.GetComponent<GameInput>().SetMovementVector(movementVector);

        // Act
        playerObject.GetComponent<Player>().Update();

        // Assert
        Assert.IsFalse(player.IsWalking());
    }

    [Test]
    public void Player_BecomesInvincible_WhenRolling()
    {
        // Act
        playerObject.GetComponent<Player>().OnRoll(null, null);

        // Assert
        Assert.IsTrue(player.IsInvincible());
    }

    [UnityTest]
    public IEnumerator Player_StopsBeingInvincible_AfterPostRollInvincibility()
    {
        // Act
        player.OnRoll(null, null);
        yield return new WaitForSeconds(0.3f);

        // Assert
        Assert.IsFalse(player.IsInvincible());
    }
}
