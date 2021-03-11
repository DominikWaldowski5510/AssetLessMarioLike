using UnityEngine;
using UnityEngine.SceneManagement;

//Triggers first game scene on any button press
public class MainMenu : MonoBehaviour
{
    //starts scene load when any button is selected
    private void Update()
    {
        if(Input.anyKeyDown )
        {
            SceneManager.LoadScene(2);
        }
    }

}
