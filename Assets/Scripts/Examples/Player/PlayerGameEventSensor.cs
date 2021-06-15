using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using System;

namespace ExampleProject
{
    public static class PlayerGameEventSensor
    {
        public static void GroundSensor(CapsuleCollider2D playerCapsule,PlayerGameEventData player,Rigidbody2D rigid,PlayerMovementData movement)
        {
            Observable.EveryLateUpdate()
                .SubscribeOn(Scheduler.MainThreadEndOfFrame)
                .Subscribe(x =>
                {
                    Ray2D ray = new Ray2D();
                    ContactFilter2D filter = new ContactFilter2D();
                    List<RaycastHit2D> hit = new List<RaycastHit2D>();
                    Physics2D.Raycast(rigid.position, Vector2.down, filter, hit, movement.groundDetectSensitivity);

                    var ground = from r in hit
                                 where r.transform.tag == "Ground"
                                 where r.normal == new Vector2(0, 1)
                                 select r;

                    player.grounded.Value = ground.Count() > 0;
                });  
        }
    }
}
