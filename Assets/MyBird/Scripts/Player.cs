using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{
    public class Player : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;

        //����
        [SerializeField] private float jumpForce = 5f;
        private bool keyJump = false;                   //���� Ű�Է� üũ

        //ȸ��
        private Vector3 birdRotain;
        [SerializeField] private float rotateSpeed = 5f;

        //�̵�
        [SerializeField] private float moveSpeed = 5f;

        //���
        [SerializeField] private float readyForce = 1f;

        public GameObject readyUI;       
        public GameObject gameoverUI;
        #endregion


        // Start is called before the first frame update
        void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
           //Ű�Է�
            InputBird();

            //���� ���
            ReadyBird();

            //���� ȸ��
            RotateBird();

            //���� �̵�
            MoveBird();
        }

        private void FixedUpdate()
        {            
            //����
            if (keyJump)
            {
                JumpBird();
                keyJump = false;                
            }
        }

        //��Ʈ�� �Է�
        void InputBird()
        {
            if (GameManager.IsDeath)
                return;
            
            //����: �����̽��� �Ǵ� ���콺 ��Ŭ��
            keyJump |= Input.GetKeyDown(KeyCode.Space);
            keyJump |= Input.GetMouseButtonDown(0);     
            
            if(GameManager.IsStart == false && keyJump)
            {
                MoveStartBird();
                
            }
        }

        //���� ����
        void JumpBird()
        {
            //�������� ���� �־� �������� �̵� 
            //rb2D.AddForce(Vector2.up*jump, ForceMode2D.Impulse);
            rb2D.velocity = Vector2.up * jumpForce;
        }

        //���� ȸ��
        void RotateBird()
        {
            //up +30, down -90
            float degree = 0;
            if (rb2D.velocity.y > 0f)
            {
                degree = rotateSpeed;
            }
            else
            {                
                degree = -rotateSpeed;                  
            }

            float rotationZ = Mathf.Clamp(birdRotain.z + degree, -60f, 30f);            
            birdRotain = new Vector3(0f, 0f, rotationZ);
            transform.eulerAngles = birdRotain;
        }

        //���� �̵�
        void MoveBird()
        {
            if (GameManager.IsStart == false || GameManager.IsDeath ==  true)
                return;

            transform.Translate(Vector3.right * Time.deltaTime*moveSpeed, Space.World);
        }

        //���� ���
        void ReadyBird()
        {
            if (GameManager.IsStart)
                return;

            //�������� ���� �־� ���ڸ��� �ֱ�
            if (rb2D.velocity.y < 0f)
            {
                rb2D.velocity = Vector2.up * readyForce;
            }           
        }

        //���� �ױ�
        void DeathBird()
        {
            //�ι� ���� ó�� ����
            if (GameManager.IsDeath)
                return;

            GameManager.IsDeath = true;
            gameoverUI.SetActive(true);
        }

        //���� ȹ��
        void GetPoint()
        {
            if (GameManager.IsDeath)
                return;

            GameManager.Score++;
        }

        //�̵� ����
        void MoveStartBird()
        {
            GameManager.IsStart = true;
            readyUI.SetActive(false);
        }

        //���� �浹 ó��
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.tag == "Pipe")
            {
                DeathBird();
            }
            else if(collider.tag == "Point")
            {
                GetPoint();
            }
        }

        //��� �Ұ���
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Ground")
            {
                DeathBird();
            }
        }
    }
}


/*
AddForce�� ���������� �ۿ���(�߷�)

velocity�� ������ ������ �ۿ���
 
 
 */