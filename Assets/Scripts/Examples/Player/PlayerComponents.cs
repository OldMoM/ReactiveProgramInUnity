using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ExampleProject
{
    public static class PlayerComponents
    {
        public static void OnPlayerMove(PlayerMovementData movement,Rigidbody2D rigid)
        {
            Observable.EveryFixedUpdate()
                .Subscribe(x =>
                {
                    var velocity_x = movement.velocity.Value.x;
                    var velocity_y = rigid.velocity.y;
                    rigid.velocity = new Vector2(velocity_x, velocity_y);
                    //rigid.velocity = movement.velocity.Value;
                });
        }

        public static void PlayAnimations(PlayerMovementData movement,Animator animator)
        {
            movement.velocity
                .Where(x => x.x == 0)
                .Subscribe(x =>
                {
                    animator.Play("player_idle");
                });

            movement.velocity
                .Where(x => x.x != 0)
                .Subscribe(x =>
                {
                    animator.Play("player_run");
                });
        }

        public static void TurnAround(PlayerMovementData movement,Transform trans)
        {
            movement.velocity
                .Where(x => x.x != 0)
                .Subscribe(x =>
                {
                    
                });

            movement.face
                .Subscribe(x =>
                {
                    if (x > 0)
                    {
                        trans.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        trans.rotation = Quaternion.Euler(0, -180, 0);
                    }
                });
        }


        public static void PlayAnimationOnBehaviorChanged(PlayerInputData player, Animator animator)
        {
            player.behavior
                .Subscribe(x =>
                {
                    var animationName = PlayerDataTransformTable.behaviorAnimationTransTab[x];
                    animator.Play(animationName);
                });
        }
    }
}
