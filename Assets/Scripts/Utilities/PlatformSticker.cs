using UnityEngine;

//parents player to platform so he doesnt slide out
public class PlatformSticker : MonoBehaviour
{
    //sets player as child object so he doesnt slide off the platform
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.parent = this.transform;
        }
    }

    //Removes the player as child of the platform so his movement is no longer restricted or bound to parent
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
