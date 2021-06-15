using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExampleProject
{
    public class PlayerSystem : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            PlayerMovementModule.PlayerMove(Data.input, Data.playerMovement, Data.playerInput);
            PlayerMovementModule.PlayerJump(Data.input, Data.playerInput, Data.playerMovement, GetComponent<Rigidbody2D>());
            PlayerMovementModule.OnBehaviorChanged(Data.playerInput.behavior, Data.playerMovement);

            PlayerComponents.OnPlayerMove(Data.playerMovement, GetComponent<Rigidbody2D>());
            PlayerComponents.TurnAround(Data.playerMovement, transform);
            PlayerComponents.PlayAnimationOnBehaviorChanged(Data.playerInput, GetComponentInChildren<Animator>());

            PlayerEventTranlatorModule.ProcessInputEvent(in Data.input, Data.playerInput);
            PlayerEventTranlatorModule.ProcessReceivedEvent(Data.playerInput);
            PlayerEventTranlatorModule.ChecklegalTransmition(Data.playerInput);
            PlayerEventTranlatorModule.OnBehaviorChanged(Data.input, Data.playerInput);

            PlayerGameEventSensor.GroundSensor(GetComponent<CapsuleCollider2D>(), 
                                               Data.playerInput,
                                               GetComponent<Rigidbody2D>(),
                                               Data.playerMovement);
        }
    }
}
