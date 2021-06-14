using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
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
