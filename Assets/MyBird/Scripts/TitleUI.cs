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
            //ġƮ
            if(Input.GetKeyDown(KeyCode.P))
            {
                ResetGameData();
            }
        }

        public void Play()
        {
            SceneManager.LoadScene(loadToScene);
        }

        //ġƮ - ���� ������ ����
        void ResetGameData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}