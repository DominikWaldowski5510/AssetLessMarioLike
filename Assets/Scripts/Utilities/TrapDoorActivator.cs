using UnityEngine;

//Disables the door when player collides with trap door trigger
public class TrapDoorActivator : MonoBehaviour
{
    [SerializeField]
    private GameObject trapPlatform = null;             //reference to trap object

    //releases the trap door when player steps on the collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            trapPlatform.SetActive(false);
        }
    }
}
