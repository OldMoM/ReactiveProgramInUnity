using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace ExampleProject
{
    public static class PlayerMovementModule
    {
        public static void PlayerMove(InputData input, PlayerMovementData movement, PlayerGameEventData player)
        {
            player.behavior
                .Where(x=>movement.enabled)
                .Where(x => x == PlayerBehaviorType.RUN || x == PlayerBehaviorType.IDLE|| x==PlayerBehaviorType.RUN_SHOT)
                .Subscribe(x =>
                {
                    movement.velocity.Value = Vector2.right * input.horizentalFlag.Value * movement.speed;
                });

            player.behavior
                .Where(x => movement.enabled)
                .Where(x => x == PlayerBehaviorType.SHOT)
                .Subscribe(x =>
                {
                    movement.velocity.Value = Vector2Int.zero;
                });


            input.horizentalFlag
                .Where(x => movement.enabled)
                .Where(x => x != 0)
                .Subscribe(x =>
                {
                    movement.face.Value = (int)x;
                });
        }
        public static void PlayerJump(InputData input,PlayerGameEventData player,PlayerMovementData movement,Rigidbody2D rigid)
        {
            player.behavior
                .Where(x=> player.grounded.Value)
                .Where(x => x is PlayerBehaviorType.JUMP)
                .Subscribe(x =>
                {
                    rigid.velocity = Vector2.zero;
                    rigid.AddForce(movement.jumpForce * Vector2.up);
                });

            player.legalBehaviorChecker
                .Skip(1)
                .Where(x => player.grounded.Value)
                .Where(x => x[0] is PlayerBehaviorType.RUN_SHOT)
                .Where(x => x[1] is PlayerBehaviorType.JUMP_SHOT)
                .Subscribe(x =>
                {
                    rigid.AddForce(movement.jumpForce * Vector2.up);
                });
        }
        public static void OnBehaviorChanged(IObservable<PlayerBehaviorType> onBehaviorChanged, PlayerMovementData movement)
        {
            onBehaviorChanged.Where(x => x is PlayerBehaviorType.DUCK)
                .Subscribe(x =>
                {
                    movement.velocity.Value = Vector2.zero;
                });

            onBehaviorChanged.Where(x => x is PlayerBehaviorType.DUCK_SHOT)
                .Subscribe(x => movement.velocity.Value = Vector2.zero);

            onBehaviorChanged.Where(x => x is PlayerBehaviorType.RISE || x is PlayerBehaviorType.RISE_SHOT)
                .Subscribe(x => movement.velocity.Value = Vector2.zero);
        }
    }
}
