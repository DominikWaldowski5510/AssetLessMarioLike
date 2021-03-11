using System.Collections;
using UnityEngine;

//handles enemy collision
public class EnemyCollision : MonoBehaviour
{

    private Pooling textPool;                       //text that displays how many points we earned
    [SerializeField] private int pointValue = 10; public int PointValue { get => pointValue; }             //value by which score will increase
    private EnemyController controller; public EnemyController Controller { get => controller;  }           //movement controller used for blocking movement

                          //controls enemy movement
    private bool hasSqushed = false;                    //checks if object has been squished down
    [SerializeField] private Transform modelController = null;
    [SerializeField] private float offset = -0.4f;              //offset for scaling the object when its squashed
    private void Awake() => controller = this.gameObject.GetComponent<EnemyController>();
    private void OnEnable() => hasSqushed = false;          //checks if enemy has been scaled down

    //collision check against player

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            PlayerSounds.instance.PlaySound(PlayerSounds.instance.nameOfSound = PlayerSounds.SoundNames.PlayerHit);
            GameManager.instance.LoseAHealth(true);
        }
    }

    
    //gets triggered when we land on top of the enemy
    public virtual void SmashEnemy()
    {
       if(hasSqushed == false)
        {
            StartCoroutine(SquashEnemyScaler());
        }
    }

    //scales object when player lands on it
    private IEnumerator SquashEnemyScaler()
    {
        hasSqushed = true;
        //sets point text fly
        textPool = GameObject.Find("PointTextPool").GetComponent<Pooling>();
        GameObject pooledObj = textPool.GetPooledObject();
        pooledObj.GetComponent<Floater>().SetPointDisplay(pointValue);
        pooledObj.transform.position = new Vector3(this.transform.position.x + 0.33f, this.transform.position.y + 0.2f, -1f);
        pooledObj.SetActive(true);
        GameManager.instance.AddPoints(pointValue);
        //sets new scale
        this.gameObject.layer = 15;
        controller.Speed = 0;
        PlayerSounds.instance.PlaySound(PlayerSounds.instance.nameOfSound = PlayerSounds.SoundNames.Squish);
        modelController.transform.localScale = new Vector3(modelController.transform.localScale.x,
            0.2f, modelController.transform.localScale.z);
        //sets new transform position to adjust to new scale
        modelController.transform.position = new Vector3(modelController.transform.position.x, modelController.transform.position.y - offset, modelController.transform.position.z);
        yield return new WaitForSeconds(1f); 
        this.gameObject.SetActive(false);
        yield return null;
    }
}
