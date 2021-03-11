using System.Collections;
using UnityEngine;

//handles winning of the level by interacting with the flag
public class FlagController : MonoBehaviour
{
    private PlayerMovement playerMovement;              //reference to player controller
    [SerializeField] private Transform flagObj = null;         //reference to flag object so it can be moved down
    private bool hasTriggeredFlag = false;              //flag trigger to prevent multiple interactions
    [SerializeField] private Transform flagEngPos = null;                    //endPosition for the flag object
    //Sets default flag value
    private void Start()
    {
        hasTriggeredFlag = false;
    }
    //handles collision when player enters(forces game to 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            playerMovement = collision.transform.GetComponent<PlayerMovement>();
            playerMovement.StopMovement();
            if(hasTriggeredFlag == false)
            {
                StartCoroutine(FlagMotion());
            }
        }
    }

    //motion of the flag going up and down
    private IEnumerator FlagMotion()
    {
        Vector3 startPos = flagObj.transform.position;
        Vector3 endPos = flagEngPos.transform.position;
        PlayerSounds.instance.PlaySound(PlayerSounds.instance.nameOfSound = PlayerSounds.SoundNames.FlagPass);
        float elapsedTime = 0;
        float waitTime = 1;
        while (elapsedTime < waitTime)
        {
            flagObj.transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }
        yield return null;
    }
}
