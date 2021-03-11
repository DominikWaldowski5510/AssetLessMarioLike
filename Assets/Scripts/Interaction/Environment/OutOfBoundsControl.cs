using UnityEngine;

//handles player moving out of bounds
public class OutOfBoundsControl : MonoBehaviour
{
    //Determines whenever another collider has entered this collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameManager.instance.LoseAHealth(false);                  
        }
        else if(collision.tag == "Powerup" || collision.tag == "Enemy")
        {
            collision.transform.gameObject.SetActive(false);
        }
        else if(collision.transform.tag == "Platform")
        {
            collision.transform.parent.gameObject.SetActive(false);
        }
        else if(collision.transform.tag == "Boss" && this.transform.tag != "Boss")
        {
            GameManager.instance.TriggerGameEnd();
        }
    }
}
