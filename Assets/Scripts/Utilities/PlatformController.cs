using System.Collections;
using UnityEngine;

//controls platforms that move up and down
public class PlatformController : MonoBehaviour
{
    [SerializeField] private Pooling platforms = null;                                //pooling platforms
    [SerializeField] private Transform platformSpawnPoint = null;            //where the platform spawns from
    [SerializeField] private float spawnDelayBetweenPlayforms = 3;              //how often each platform spawns between each other
    private void Start() => StartCoroutine(SpawnPlatform());

    //spawns platforms constantly
   private IEnumerator SpawnPlatform()
   {
        while(true)
        {
            GameObject newPlatform = platforms.GetPooledObject();
            newPlatform.transform.position = platformSpawnPoint.transform.position;
            newPlatform.transform.rotation = platformSpawnPoint.transform.rotation;
            newPlatform.SetActive(true);
            yield return new WaitForSeconds(spawnDelayBetweenPlayforms);
        }
        
   }
}
