using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ExampleProject
{
    public static class Data
    {
        public static PlayerMovementData playerMovement = new PlayerMovementData(Unit.Default);
        public static InputData input = new InputData();
        public static PlayerGameEventData playerInput = new PlayerGameEventData();
        public static TestData test = new TestData();
    }
}
