using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MyBird
{
    public class TitleUI : MonoBehaviour
    {
        #region Variables
        [SerializeField] private string loadToScene = "PlayScene";
        #endregion

        private void Update()
        {
            //치트
            if(Input.GetKeyDown(KeyCode.P))
            {
                ResetGameData();
            }
        }

        public void Play()
        {
            SceneManager.LoadScene(loadToScene);
        }

        //치트 - 게임 데이터 리셋
        void ResetGameData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}