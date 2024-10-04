using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{
    public class SpawnManager : MonoBehaviour
    {
        #region Variables
        //파이프 프리팹
        public GameObject spawnPrefab;

        //스폰 타이머
        [SerializeField] private float spawnTimer = 1.0f;
        private float countdown = 0f;

        [SerializeField] private float maxSpawnTimer = 1.05f;
        [SerializeField] private float minSpawnTimer = 0.95f;

        //스폰 위치
        [SerializeField] private float maxSpawnY = 3.5f;
        [SerializeField] private float minSpawnY = -1.5f;
        #endregion

        private void Start()
        {
            //초기화
            countdown = spawnTimer;
        }

        private void Update()
        {
            //게임 진행 체크
            if (GameManager.IsStart == false || GameManager.IsDeath == true)
                return;

            //스폰 타이머
            if(countdown <= 0f)
            {
                //스폰 
                SpawnPipe();

                //타이머 초기화
                //countdown = spawnTimer;
                countdown = Random.Range(minSpawnTimer, maxSpawnTimer);
            }
            countdown -= Time.deltaTime;
        }

        void SpawnPipe()
        {
            float spawnY = transform.position.y + Random.Range(maxSpawnY, minSpawnY);
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, 0f);   
            Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
        }
    }
}