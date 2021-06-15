using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ExampleProject
{
    public class EnemyAttackData 
    {
        public Subject<Unit> attack;
        public float attackInterval;
        public EnemyAttackData()
        {
            attack = new Subject<Unit>();
            attackInterval = 2;
        }
    }
}
