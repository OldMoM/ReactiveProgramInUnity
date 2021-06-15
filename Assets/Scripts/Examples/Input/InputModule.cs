using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
namespace ExampleProject
{
    public static class InputModule
    {
        public static void OnMoveBtnPressed(InputData input)
        {
            Observable
                .EveryUpdate()
                .Subscribe(x =>
                {
                    input.horizentalFlag.Value = Input.GetAxisRaw("Horizontal");
                    input.verticalFlag.Value = Input.GetAxisRaw("Vertical");
                    input.isShooting.Value = Input.GetButton("Fire1");
                });
        }

        public static void Test(ref TestData test)
        {
            test.test = 6;
            Debug.Log(test.test);
            Debug.Log(Data.test.test);

            Observable.Interval(TimeSpan.FromSeconds(1))
                .SubscribeWithState(test, (x, y) =>
                {
                    y.test = 5;
                    Debug.Log(y.test);
                });

            //Observable.Interval(TimeSpan.FromSeconds(1))
            //   .SubscribeWithState(test, Tset);

            //Observable.Interval(TimeSpan.FromSeconds(1))
            //    .Subscribe(x =>
            //    {
            //        Debug.Log(Data.test.test);
            //    });
        }

        public static void Tset(ref TestData xt, long y) { }

        public static void OnJumpBtnPressed(InputData input)
        {
            Observable
                .EveryUpdate()
                .Where(x => Input.GetButtonDown("Jump"))
                .Subscribe(x =>
                {
                    input.onJumpBtnPressed.OnNext(Unit.Default);
                });
        }
    }
}
