using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExampleProject
{
    public class GameContext : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            InputModule.OnMoveBtnPressed(Data.input);
            InputModule.OnJumpBtnPressed(Data.input);

            InputModule.Test(ref Data.test);
        }
    }
}
