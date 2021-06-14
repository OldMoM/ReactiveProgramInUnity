using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExampleProject
{
    public class GameSystem : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            InputModule.OnMoveBtnPressed(Data.input);
            InputModule.OnJumpBtnPressed(Data.input);
        }
    }
}
