using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FalseDemonstration
{
    public class NaivePlayerController : MonoBehaviour
    {
        //ʵ�壨Enitity��
        public Rigidbody2D rigid;
        public Animator animator;
        //����
        public float speed;
        // Start is called before the first frame update
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            //Rigidbody2D�����߼�
            rigid.velocity = Vector2.right * Input.GetAxisRaw("Horizontal") * speed;
            //Animator�����߼�
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                animator.Play("player_idle");
            }
            else
            {
                animator.Play("player_run");
            }
        }
    }

    public class PlayerMoveData
    {
        public Vector2 velocity;
        public float jumpForce;
    }

    public static class PlayerMoveModule
    {
        public static void PlayerMove(PlayerMoveData data)
        {
            //....
        }
        public static void PlayerJump(PlayerMoveData data)
        {
            //...
        }
    }
}
