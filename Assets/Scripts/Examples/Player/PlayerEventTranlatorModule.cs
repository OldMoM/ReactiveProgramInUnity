using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Linq;
using System;

namespace ExampleProject
{
    public static class PlayerEventTranlatorModule
    {
        public static void ProcessInputEvent(in InputData keyboard,PlayerGameEventData player)
        {
            keyboard.isShooting
                .Subscribe(x =>
                {
                    if (x)
                    {
                        player.eventReceiver[1] = PlayerBehaviorType.SHOT;
                       
                    }
                    else
                    {
                        player.eventReceiver[1] = PlayerBehaviorType.IDLE;
                    }

                    player.onEventReceiverChanged.OnNext(player.eventReceiver);
                });

            keyboard.horizentalFlag
                .Subscribe(x =>
                {
                    if (x != 0)
                    {
                        player.eventReceiver[0] = PlayerBehaviorType.RUN;
                    }
                    else
                    {
                        player.eventReceiver[0] = PlayerBehaviorType.IDLE;
                    }

                    player.onEventReceiverChanged.OnNext(player.eventReceiver);
                });

            keyboard.onJumpBtnPressed
                .Subscribe(x =>
                {
                    player.eventReceiver[2] = PlayerBehaviorType.JUMP;
                    player.onEventReceiverChanged.OnNext(player.eventReceiver);
                });

            player.grounded
                .Where(x=> x)
                .Subscribe(x =>
                {
                    if (player.eventReceiver[0] == PlayerBehaviorType.RUN)
                    {
                        player.eventReceiver = new PlayerBehaviorType[]
                        {
                            PlayerBehaviorType.RUN,
                            PlayerBehaviorType.IDLE,
                            PlayerBehaviorType.IDLE
                        };
                    }
                    else
                    {
                        player.eventReceiver = new PlayerBehaviorType[]
                        {
                            PlayerBehaviorType.IDLE,
                            PlayerBehaviorType.IDLE,
                            PlayerBehaviorType.IDLE
                        };
                    }
                    player.onEventReceiverChanged.OnNext(player.eventReceiver);
                });

            keyboard.verticalFlag
                .Subscribe(x =>
                {
                    if (x == 1)
                    {
                        player.eventReceiver[0] = PlayerBehaviorType.RISE;
                    }
                    else if (x == 0)
                    {
                        player.eventReceiver[0] = PlayerBehaviorType.IDLE;
                    }
                    else
                    {
                        player.eventReceiver[0] = PlayerBehaviorType.DUCK;
                    }
                    player.onEventReceiverChanged.OnNext(player.eventReceiver);
                });
        }
        public static bool CompareList<T>(IEnumerable<T> list1,IEnumerable<T> list2)
        {
            var t1 = list1.OrderByDescending(t => t).ToList();
            var t2 = list2.OrderByDescending(t => t).ToList();

            var equal = Enumerable.SequenceEqual(t1, t2);

            return equal;
        }
        public static void ProcessReceivedEvent(PlayerGameEventData player)
        {
            player.onEventReceiverChanged
                .Subscribe(x =>
                {
                    PlayerDataTranslateTable.eventBehaviorTransTab
                        .Where(y=>CompareList(y.Key,x))
                        .ToObservable()
                        .Subscribe(y =>
                        {
                            var oldBehavior = Data.playerInput.behavior.Value;
                            var newBehavior = y.Value;
                            var transmission = new PlayerBehaviorType[] { oldBehavior, newBehavior };

                            player.legalBehaviorChecker.Value = transmission;

                        });
                });
        }
        public static void ChecklegalTransmition(PlayerGameEventData player)
        {
            player.legalBehaviorChecker
                .Skip(1)
                .Subscribe(x =>
                {
                    var illegal =
                    PlayerDataTranslateTable.illegalTranTranslation.Find(y => Enumerable.SequenceEqual(x, y));

                    if (illegal == null)
                    {
                        player.behavior.Value = x[1];
                    }
                });
        }
        public static void OnBehaviorChanged(InputData keyboard, PlayerGameEventData player)
        {
            player.behavior.Where(x => x is PlayerBehaviorType.IDLE || x is PlayerBehaviorType.SHOT)
            .Subscribe(x =>
            {
                var horizentalFlag = keyboard.horizentalFlag.Value;
                if (horizentalFlag != 0)
                {
                    player.eventReceiver[0] = PlayerBehaviorType.RUN;
                }
                player.onEventReceiverChanged.OnNext(player.eventReceiver);
            });

            player.behavior.Where(x => x is PlayerBehaviorType.JUMP)
                .Subscribe(x =>
                {
                    var isShoot = keyboard.isShooting.Value;
                    if (isShoot)
                    {
                        player.eventReceiver[1] = PlayerBehaviorType.SHOT;
                    }
                });
        }
    }
}
