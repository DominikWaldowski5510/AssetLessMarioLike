using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//handles teleporting to new areas/completing levels
public class Warp : MonoBehaviour
{
    [SerializeField] private string levelname = "";             //name of the level to load using warp

    //loads new scene
    public void WarpPlayer ()
    {
        SceneManager.LoadScene(levelname);
    }

    //loads the next level
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            SceneManager.LoadScene(levelname);
        }
    }
}
