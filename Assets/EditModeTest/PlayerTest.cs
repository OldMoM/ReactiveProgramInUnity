using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UniRx;
using ExampleProject;
public class PlayerTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerMoveRight_0_5()
    {
        //Arrange
        PlayerMovementModule.PlayerMove(Data.input, Data.playerMovement, Data.playerInput);
        //Act
        Data.input.horizentalFlag.Value = 1;
        //Asset
        var actualVelocity = Data.playerMovement.velocity.Value;
        var expectedVelocity = new Vector2(5, 0);
        Assert.AreEqual(expectedVelocity, actualVelocity);
    }
    [Test]
    public void PlayerMoveLeft_0_neg5()
    {
        //Arrange
        PlayerMovementModule.PlayerMove(Data.input, Data.playerMovement, Data.playerInput);
        //Act
        Data.input.horizentalFlag.Value = -1;
        //Asset
        var actualVelocity = Data.playerMovement.velocity.Value;
        var expectedVelocity = new Vector2(-5, 0);
        Assert.AreEqual(expectedVelocity, actualVelocity);
    }

}
