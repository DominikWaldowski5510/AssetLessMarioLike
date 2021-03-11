using System.Collections;
using UnityEngine;

//Used for moving platforms sideways
public class SidePlatform : MonoBehaviour
{
    [SerializeField]
    private Transform pointA = null, pointB = null;               //points between which the platform lerps
    [SerializeField] private Transform platform = null;             //The platform object that is moved
    private void Start() => StartCoroutine(PlatformMovement());

    //starts infinite corutine that moves platform to each side
    private IEnumerator PlatformMovement()
    {
        while(true)
        {
            float elapsedTime = 0;
            float waitTime = 6f;
            
            while (elapsedTime < waitTime)
            {
                platform.transform.position = Vector3.Lerp(pointA.position, pointB.position, (elapsedTime / waitTime));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();

            }
            yield return new WaitForSeconds(1.5f);
            //makes the block move down back to its original position
            elapsedTime = 0;
            while (elapsedTime < waitTime)
            {
                platform.transform.position = Vector3.Lerp(pointB.position, pointA.position, (elapsedTime / waitTime));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(1.5f);
        }
    }

    
}
