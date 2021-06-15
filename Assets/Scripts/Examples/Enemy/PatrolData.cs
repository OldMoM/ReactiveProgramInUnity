using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
namespace ExampleProject
{
    public class PatrolData
    {
        public FloatReactiveProperty distance;
        public List<Vector3> wayPoints;
        public int target;
        public bool reachTarget;
        public float stillTime;
        public Vector3 position;
        public PatrolData(List<Transform> wayPoints)
        {
            reachTarget = false;
            distance = new FloatReactiveProperty();
            stillTime = 2;
        }
    }
}
