using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ExampleProject
{
    public class InputData
    {
        public FloatReactiveProperty horizentalFlag;
        //public BoolReactiveProperty isRising;
        public BoolReactiveProperty isShooting;
        //public BoolReactiveProperty isDucking;
        public Subject<Unit> onJumpBtnPressed;

        public FloatReactiveProperty verticalFlag;
        public InputData(Unit unit)
        {
            horizentalFlag = new FloatReactiveProperty();
            onJumpBtnPressed = new Subject<Unit>();

            isShooting = new BoolReactiveProperty(false);
            verticalFlag = new FloatReactiveProperty();
        }
    }
}
