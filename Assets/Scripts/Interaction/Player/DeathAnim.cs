using UnityEngine;

//Handles death animation of the player
public class DeathAnim : MonoBehaviour
{
    private float topPoint = 5;             //how high the player jumps when he dies
    private Vector3 startPos;               //start position of the jump
    private Vector3 endPos;                 //where the player will land after the jump
    private float journeyLength;            //how long it takes for the jump to happen
    private float startTime;                //the Start time when the jump occurs

    private void OnEnable() => Invoke("Disabler", 1.5f);           //runs the function after specified time 


    private void Disabler() =>  this.transform.gameObject.SetActive(false);      //disables the game object


    //sets the default values for the death animation
    public void SetDeathAnim(Vector3 _startPos)
    {
        startTime = Time.time;
        startPos = _startPos;
        endPos = new Vector3(startPos.x +4, startPos.y -4, startPos.z);
        journeyLength = Vector3.Distance(startPos, endPos);
    }

    //runs the death animation which is a parabola movement formula
    public void DeathAnimation()
    {
        float distCovered = (Time.time - startTime) * 5;
        journeyLength = Vector3.Distance(startPos, endPos);
        float fractionOfJourney = distCovered / journeyLength;
        transform.position = Parabola.ParabolaFunc(startPos, endPos, topPoint, fractionOfJourney);
    }
}
