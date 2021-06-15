using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ExampleProject
{
    public class InputData
    {
        public FloatReactiveProperty horizentalFlag;
        public FloatReactiveProperty verticalFlag;
        public BoolReactiveProperty isShooting;
        public Subject<Unit> onJumpBtnPressed;

        public InputData()
        {
            horizentalFlag = new FloatReactiveProperty();
            onJumpBtnPressed = new Subject<Unit>();
            isShooting = new BoolReactiveProperty(false);
            verticalFlag = new FloatReactiveProperty();
        }
    }
}
