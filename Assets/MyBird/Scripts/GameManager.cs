using UnityEngine;

namespace MyBird
{
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public static bool IsStart { get; set; }
        #endregion

        private void Start()
        {
            //√ ±‚»≠
            IsStart = false;
        }
    }
}
