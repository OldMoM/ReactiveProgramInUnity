using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExampleProject
{
    public class CrabData
    {
        public EnemyMovementData movement;
        public EnemyAttackData attack;

        public CrabData()
        {
            movement = new EnemyMovementData();
            attack = new EnemyAttackData();
        }
    }
}
