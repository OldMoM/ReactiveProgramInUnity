using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ExampleProject
{
    public class CrabEventTranslatorData
    {
        public BoolReactiveProperty isInsightPlayer;
        public Subject<int> onHurt;
        public BoolReactiveProperty isDied;

        public ReactiveProperty<EnemyGameEvent> gameEventReceiver;
        public Subject<EnemyGameEvent> standardEventGenerator;
        public ReactiveProperty<EnemyBehaviorType> behavior;
        public CrabEventTranslatorData()
        {
            isInsightPlayer = new BoolReactiveProperty();
            onHurt = new Subject<int>();
            isDied = new BoolReactiveProperty();

            gameEventReceiver = new ReactiveProperty<EnemyGameEvent>();
            standardEventGenerator = new Subject<EnemyGameEvent>();
            behavior = new ReactiveProperty<EnemyBehaviorType>();
        }
    }
}
