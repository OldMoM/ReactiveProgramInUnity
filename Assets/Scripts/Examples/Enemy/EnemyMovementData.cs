using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace ExampleProject
{
    public class EnemyMovementData
    {
        public Vector2ReactiveProperty velocity;
        public float speed;

        public EnemyMovementData()
        {
            velocity = new Vector2ReactiveProperty();
            speed = 5;
        }
    }
}
