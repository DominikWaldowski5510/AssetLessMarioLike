using UnityEngine;

//disales cursor while in game
public class CursorScript : MonoBehaviour
{
    //handles cursor function
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
