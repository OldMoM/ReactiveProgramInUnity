using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
namespace ExampleProject
{
    public class PlayerGameEventData
    {
        public PlayerBehaviorType[] eventReceiver = new PlayerBehaviorType[]
        {
            PlayerBehaviorType.IDLE,
            PlayerBehaviorType.IDLE,
            PlayerBehaviorType.IDLE
        };
        public Subject<PlayerBehaviorType[]> onEventReceiverChanged;
        public ReactiveProperty<PlayerBehaviorType> behavior = new ReactiveProperty<PlayerBehaviorType>(PlayerBehaviorType.IDLE);
        public ReactiveProperty<PlayerBehaviorType[]> legalBehaviorChecker;
        /// <summary>
        /// The on behavior changed broadcast newValue and oldValue
        /// </summary>
        //public Subject<ValueTuple<PlayerBehaviorType, PlayerBehaviorType>> onBehaviorChanged;
        public BoolReactiveProperty grounded;

        public StringReactiveProperty playAnimation;
        public PlayerGameEventData()
        {
            onEventReceiverChanged = new Subject<PlayerBehaviorType[]>();
            playAnimation = new StringReactiveProperty("player_idle");
            grounded = new BoolReactiveProperty(true);
            legalBehaviorChecker = new ReactiveProperty<PlayerBehaviorType[]>();
        }
    }
}
