using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{
    public class Player : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;

        //점프
        [SerializeField] private float jumpForce = 5f;
        private bool keyJump = false;                   //점프 키입력 체크

        //회전
        private Vector3 birdRotain;
        [SerializeField] private float rotateSpeed = 5f;

        //이동
        [SerializeField] private float moveSpeed = 5f;

        //대기
        [SerializeField] private float readyForce = 1f;

        //게임 UI
        public GameObject readyUI;       
        public GameObject gameoverUI;

        //사운드
        private AudioSource audioSource;
        #endregion


        // Start is called before the first frame update
        void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
           //키입력
            InputBird();

            //버드 대기
            ReadyBird();

            //버드 회전
            RotateBird();

            //버드 이동
            MoveBird();
        }

        private void FixedUpdate()
        {            
            //점프
            if (keyJump)
            {
                JumpBird();
                keyJump = false;                
            }
        }

        //컨트롤 입력
        void InputBird()
        {
            if (GameManager.IsDeath)
                return;

/*#if UNITY_EDITOR

            //점프: 스페이스바 또는 마우스 왼클릭
            keyJump |= Input.GetKeyDown(KeyCode.Space);
            keyJump |= Input.GetMouseButtonDown(0);
#else*/

            //터치 인풋 처리
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began)
                {
                    keyJump |= true;
                }
            }
//#endif

            if(GameManager.IsStart == false && keyJump)
            {
                MoveStartBird();
                
            }
        }

        //버드 점프
        void JumpBird()
        {
            //위쪽으로 힘을 주어 위쪽으로 이동 
            //rb2D.AddForce(Vector2.up*jump, ForceMode2D.Impulse);
            rb2D.velocity = Vector2.up * jumpForce;
        }

        //버드 회전
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

        //버드 이동
        void MoveBird()
        {
            if (GameManager.IsStart == false || GameManager.IsDeath ==  true)
                return;

            transform.Translate(Vector3.right * Time.deltaTime*moveSpeed, Space.World);
        }

        //버드 대기
        void ReadyBird()
        {
            if (GameManager.IsStart)
                return;

            //위쪽으로 힘을 주어 제자리에 있기
            if (rb2D.velocity.y < 0f)
            {
                rb2D.velocity = Vector2.up * readyForce;
            }           
        }

        //버드 죽기
        void DeathBird()
        {
            //두번 죽음 처리 방지
            if (GameManager.IsDeath)
                return;

            GameManager.IsDeath = true;
            gameoverUI.SetActive(true);
        }

        //점수 획득
        void GetPoint()
        {
            if (GameManager.IsDeath)
                return;

            GameManager.Score++;

            //포인트 획득 사운드 플레이
            audioSource.Play();

            //기둥을 10개 통과할 때마다 - 10점, 20점, 30점, 난이도 증가
            if(GameManager.Score % 10 == 0)
            {
                SpawnManager.levelTime += 0.05f;
            }
        }

        //이동 시작
        void MoveStartBird()
        {
            GameManager.IsStart = true;
            readyUI.SetActive(false);
        }

        //버드 충돌 처리
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

        //통과 불가능
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
AddForce는 물리적으로 작용함(중력)

velocity는 정해진 양으로 작용함
 
 
 */