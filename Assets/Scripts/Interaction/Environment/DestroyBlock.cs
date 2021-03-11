using System.Collections;
using UnityEngine;

//handles animation of making block go up and become destroyed
public class DestroyBlock : MonoBehaviour
{
    [Range(0.5f, 2f)] [SerializeField] private float adjustmentAmount = 0.5f;
    [Range(0.15f, 2f)] [SerializeField] private float waitTime = 0.15f;
    private Pooling destructionEffect;                  //pooling for the effect that spawns when destructables get hit
    bool isActive = false;                              //checks if the corutine is active to prevent double calls
    [SerializeField] private bool hasSounds = false;                      //checks if the object plays a sound
    //function that gets called from alternative script, used to set corutine that handles destruction
    public virtual void TriggerBlock(bool isDestructable)
    {
        if (isActive == false)
        {
            StartCoroutine(BlockAction(isDestructable));
        }
    }

    //stops corutine to avoid issues when the object is disabled
    private void OnDisable() => StopCoroutine(BlockAction(false));


    //corutine that makes block move up and down as well as triggers particles and destroys object
    private IEnumerator BlockAction(bool isDestructable)
    {
        if (this.transform.gameObject.activeInHierarchy == true)
        {
            isActive = true;
            Vector3 startPos = this.transform.position;
            Vector3 endPos = new Vector3(this.transform.position.x, this.transform.position.y + adjustmentAmount, this.transform.position.z);
            //creates effect if the block can be destroyed by pooling the effect 
            if (isDestructable)
            {
                destructionEffect = GameObject.Find("DestroyBlockParticlePool").GetComponent<Pooling>();
                GameObject newEffect = destructionEffect.GetPooledObject();
                newEffect.transform.position = startPos;
                newEffect.SetActive(true);
            }
            //makes the block move up slightly
            float elapsedTime = 0;
            while (elapsedTime < waitTime)
            {
                this.transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / waitTime));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();

            }
            if (hasSounds == true)
            {
                if (isDestructable)
                {
                    PlayerSounds.instance.PlaySound(PlayerSounds.instance.nameOfSound = PlayerSounds.SoundNames.DestructionBlock);
                }
            }
            transform.position = endPos;
            //makes the block move down back to its original position
            elapsedTime = 0;
            while (elapsedTime < waitTime)
            {
                this.transform.position = Vector3.Lerp(endPos, startPos, (elapsedTime / waitTime));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();

            }
            transform.position = startPos;
            //disables object if its meant to be destructable
            if (isDestructable)
            {
                this.transform.gameObject.SetActive(false);
            }
            
        }
        isActive = false;
        yield return null;
    }
}
