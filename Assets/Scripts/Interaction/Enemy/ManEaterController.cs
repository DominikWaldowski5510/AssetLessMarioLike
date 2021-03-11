using UnityEngine;

//Controls the man eating plant
public class ManEaterController : MonoBehaviour
{
    [SerializeField] private DestroyBlock blockToDestroy = null;            //reference to blockdestroy class
    private float randomStart;                                              //randomized start time so that player wont know when plant will first appear up

    //sets random start of the plant and triggers invoke of the plant
    private void Start()
    {
        randomStart = Random.Range(0.0f, 1.0f);
        InvokeRepeating("RunManEating", randomStart, 5);
    }

    //Invoke function which makes the plant go up and down
    private void RunManEating()
    {
        blockToDestroy.TriggerBlock(false);
    }

}
