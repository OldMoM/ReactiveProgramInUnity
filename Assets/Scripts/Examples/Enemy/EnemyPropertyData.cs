using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
namespace ExampleProject
{
    public class EnemyPropertyData
    {
        public IntReactiveProperty health;
        public EnemyPropertyData()
        {
            health = new IntReactiveProperty();
        }
    }
}
