using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ExampleProject
{
    public static class Data
    {
        public static PlayerMovementData playerMovement = new PlayerMovementData(Unit.Default);
        public static InputData input = new InputData(Unit.Default);
        public static PlayerInputData playerInput = new PlayerInputData();
    }
}
