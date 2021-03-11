using UnityEngine;

//handles collision between objects that give points to the player such as powerups, coin or the end flag
public class Coin : MonoBehaviour
{
    [SerializeField] private int pointValue = 10;                //value by which score will increase
    private Pooling textPool;                               //text that displays how many points we  earned
    //handles collision between the collectable and the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            textPool = GameObject.Find("PointTextPool").GetComponent<Pooling>();
           GameObject pooledObj =  textPool.GetPooledObject();
            pooledObj.GetComponent<Floater>().SetPointDisplay(pointValue);
            pooledObj.transform.position = new Vector3(this.transform.position.x + 0.23f, this.transform.position.y + 0.3f, -1f);
            pooledObj.SetActive(true);
            GameManager.instance.AddPoints(pointValue);
            if(this.gameObject.tag == "Coin")
            {
                PlayerSounds.instance.PlaySound(PlayerSounds.instance.nameOfSound = PlayerSounds.SoundNames.Coin);
            }
            if (this.gameObject.tag == "Powerup")
            {
                GameManager.instance.AddPowerup();
                PlayerSounds.instance.PlaySound(PlayerSounds.instance.nameOfSound = PlayerSounds.SoundNames.Powerup);
            }
            if (this.gameObject.tag == "Powerup2")
            {
                GameManager.instance.AddPowerup();
                PlayerSounds.instance.PlaySound(PlayerSounds.instance.nameOfSound = PlayerSounds.SoundNames.Powerup2);
            }
            if (this.gameObject.tag != "Flag")
            {
                this.gameObject.SetActive(false);
            }
           
        }
    }
}
