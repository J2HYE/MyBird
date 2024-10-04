using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBird
{
    public class PipeKiller : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }
    }
}
