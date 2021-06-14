using UniRx;

namespace ExampleProject
{
    public class PlayerMovementData
    {
        public IntReactiveProperty face;
        public Vector2ReactiveProperty velocity;
        public float speed;
        public bool enabled;
        public float groundDetectSensitivity;
        public int jumpForce;

        public PlayerMovementData(Unit unit)
        {
            face = new IntReactiveProperty(1);
            velocity = new Vector2ReactiveProperty();
            enabled = true;
            speed = 5;
            groundDetectSensitivity = 0.7f;
            jumpForce = 300;
        }
    }
}
