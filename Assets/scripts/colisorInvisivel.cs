using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class colisorInvisivel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
            collision.gameObject.GetComponent<playerController>().enabled = false;
        
    }
}
