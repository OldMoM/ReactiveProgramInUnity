using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ExampleProject;
using System.Linq;
using UniRx;
using System;

public class PlayerInputEventTest
{
    [UnityTest]
    public IEnumerator TwoSameKindList_true()
    {
        var list1 = new List<PlayerBehaviorType>() { PlayerBehaviorType.SHOT, PlayerBehaviorType.RUN };
        var list2 = new List<PlayerBehaviorType>() { PlayerBehaviorType.RUN ,PlayerBehaviorType.SHOT};

        var compareResult = PlayerEventTranlatorModule.CompareList<PlayerBehaviorType>(list1, list2);

        Assert.IsTrue(compareResult);

        yield return null;
    }
    [UnityTest]
    public IEnumerator TwoDifferentList_false()
    {
        var list1 = new List<PlayerBehaviorType>() { PlayerBehaviorType.SHOT, PlayerBehaviorType.RUN ,PlayerBehaviorType.DUCK};
        var list2 = new List<PlayerBehaviorType>() { PlayerBehaviorType.SHOT, PlayerBehaviorType.RUN };

        var compareResult = PlayerEventTranlatorModule.CompareList<PlayerBehaviorType>(list1, list2);

        Assert.IsFalse(compareResult);

        yield return null;
    }
    [UnityTest]
    public IEnumerator PressShotAndRunKey_GetShot_Run()
    {
        PlayerEventTranlatorModule.ProcessInputEvent(in Data.input, Data.playerInput);

        Data.input.isShooting.Value = true;
        Data.input.horizentalFlag.Value = 1f;

        var expected = new List<PlayerBehaviorType>() { PlayerBehaviorType.RUN,
                                                        PlayerBehaviorType.SHOT,
                                                        PlayerBehaviorType.IDLE };
        var actual = Data.playerInput.eventReceiver;
        var compareResult = PlayerEventTranlatorModule.CompareList<PlayerBehaviorType>(expected, actual);

        Assert.IsTrue(compareResult);
        yield return null;
    }
    [UnityTest]
    public IEnumerator OnShotAndRunKeyPressing_releaseShotKey_GetRun()
    {
        PlayerEventTranlatorModule.ProcessInputEvent(in Data.input, Data.playerInput);

        Data.input.isShooting.Value = true;
        Data.input.horizentalFlag.Value = 1f;

        Data.input.isShooting.Value = false;

        var actual = Data.playerInput.eventReceiver;
        var expected = new List<PlayerBehaviorType>() { PlayerBehaviorType.RUN, 
                                                        PlayerBehaviorType.IDLE,
                                                        PlayerBehaviorType.IDLE };

        Assert.AreEqual(actual, expected);
        yield return null;
    }
    [UnityTest]
    public IEnumerator OnShotAndRunKeyPressing_OutRun_Shot_IDLE()
    {
        PlayerEventTranlatorModule.ProcessInputEvent(in Data.input, Data.playerInput);

        Data.playerInput.onEventReceiverChanged
            .Skip(1)
            .Subscribe(x =>
            {
                Debug.Log("get message");
                var expected = new PlayerBehaviorType[] { PlayerBehaviorType.RUN, PlayerBehaviorType.SHOT, PlayerBehaviorType.IDLE };
                var actual = x;
                foreach (var item in x)
                {
                    Debug.Log(item);
                }
                Assert.AreEqual(expected, actual);
                return;
            });

        Data.input.horizentalFlag.Value = 1;
        Data.input.isShooting.Value = true;

        yield return new WaitForSeconds(0.1f);
    }
    [UnityTest]
    public IEnumerator OnBehaviorEventGenertated_RunShot()
    {
        PlayerEventTranlatorModule.ProcessReceivedEvent(Data.playerInput);

        Data.playerInput.onEventReceiverChanged.OnNext(new PlayerBehaviorType[] { PlayerBehaviorType.RUN, PlayerBehaviorType.SHOT, PlayerBehaviorType.IDLE });

        var actual = Data.playerInput.behavior.Value;
        var expected = PlayerBehaviorType.RUN_SHOT;

        Assert.AreEqual(expected, actual);
        yield return null;
    }
    [UnityTest]
    public IEnumerator GetEvent_RunIdleIdle_Run()
    {
        PlayerEventTranlatorModule.ProcessReceivedEvent(Data.playerInput);

        Data.playerInput.onEventReceiverChanged.OnNext(new PlayerBehaviorType[]
        {
            PlayerBehaviorType.RUN,
            PlayerBehaviorType.IDLE,
            PlayerBehaviorType.IDLE
        });

        var actual = Data.playerInput.behavior.Value;
        var expected = PlayerBehaviorType.RUN;

        Assert.AreEqual(expected, actual);
        yield return null;
    }
    [UnityTest]
    public IEnumerator JumbBtnPressed_InputEventReceiver_Jump_Idle_Idle()
    {
        PlayerEventTranlatorModule.ProcessInputEvent(in Data.input, Data.playerInput);

        Data.input.onJumpBtnPressed.OnNext(Unit.Default);

        var actual = Data.playerInput.eventReceiver;
        var expected = new PlayerBehaviorType[]
        {
            PlayerBehaviorType.IDLE,
            PlayerBehaviorType.IDLE,
            PlayerBehaviorType.JUMP
        };

        Assert.AreEqual(expected, actual);

        yield return null;
    }
    [UnityTest]
    public IEnumerator Transfer_IdleIdleJump_To_JumpAndThenGrounded()
    {
        PlayerEventTranlatorModule.ProcessReceivedEvent(Data.playerInput);

        Data.playerInput.onEventReceiverChanged.OnNext(new PlayerBehaviorType[]
        {
            PlayerBehaviorType.IDLE,
            PlayerBehaviorType.IDLE,
            PlayerBehaviorType.JUMP,
        });

        var actual1 = Data.playerInput.behavior.Value;
        var expected1 = PlayerBehaviorType.JUMP;

        Assert.AreEqual(expected1, actual1);

        yield return new WaitForSeconds(1);

        Data.playerInput.onEventReceiverChanged.OnNext(new PlayerBehaviorType[]
        {
            PlayerBehaviorType.GROUNDED,
            PlayerBehaviorType.GROUNDED,
            PlayerBehaviorType.IDLE
        });

        var actual2 = Data.playerInput.behavior.Value;
        var expected2 = PlayerBehaviorType.IDLE;

        Assert.AreEqual(expected2, actual2);
    }
    [UnityTest]
    public IEnumerator CheckIllegalTransmition_JUMP_SHOT()
    {
        PlayerEventTranlatorModule.ChecklegalTransmition(Data.playerInput);
        Data.playerInput.behavior.Value = PlayerBehaviorType.JUMP_SHOT;

        Data.playerInput.legalBehaviorChecker.Value = new PlayerBehaviorType[]
        {
            PlayerBehaviorType.JUMP_SHOT,
            PlayerBehaviorType.JUMP
        };

        var expected = PlayerBehaviorType.JUMP_SHOT;
        var actual = Data.playerInput.behavior.Value;
        Assert.AreEqual(expected, actual);

        yield return null;
    }
    [UnityTest]
    public IEnumerator CheckIllegalTransmition_RUN()
    {
        PlayerEventTranlatorModule.ChecklegalTransmition(Data.playerInput);

        Data.playerInput.legalBehaviorChecker.Value = new PlayerBehaviorType[]
        {
            PlayerBehaviorType.JUMP_SHOT,
            PlayerBehaviorType.RUN
        };

        var expected = PlayerBehaviorType.RUN;
        var actual = Data.playerInput.behavior.Value;
        Assert.AreEqual(expected, actual);

        yield return null;
    }
    [UnityTest]
    public IEnumerator CompareArray_false()
    {
        var actual = new int[] { 1, 1, 1 };
        var expected = new int[] { 1, 1, 1 };

        Assert.IsFalse(actual == expected);
        yield return null;
    }
    [UnityTest]
    public IEnumerator ValueTuple_EqualTest_true()
    {
        var tuple1 = (PlayerBehaviorType.DUCK, PlayerBehaviorType.IDLE);
        var tuple2 = (PlayerBehaviorType.DUCK, PlayerBehaviorType.IDLE);

        var equal = ValueTuple.Equals(tuple1, tuple2);
        Assert.IsTrue(equal);
        yield return null;
    }
}
