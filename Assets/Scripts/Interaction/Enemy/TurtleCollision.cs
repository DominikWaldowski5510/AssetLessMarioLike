using System.Collections;
using UnityEngine;

//interaction for turtle enemy
public class TurtleCollision : EnemyCollision
{
    [SerializeField] private GameObject[] states = null;           //the different stages of the enemy
    private int currentState = 0;                           //current stage of the enemy
    private bool delayState = false;
    //sets default enemy settings
    private void Start()
    { 
        Controller.Speed = 1;
        ResetStates();
        currentState = 0;
        states[currentState].SetActive(true);
        delayState = false;
    }
    
    //Resets all the states by disabling the object
    private void ResetStates()
    {
        for (int i = 0; i < states.Length; i++)
        {
            states[i].SetActive(false);
        }
    }

    //overites the default smash enemy to trigger between disabling and different state
    public override void SmashEnemy()
    {
        if (currentState == 0)
        {
            ResetStates();
            currentState = 1;
            states[currentState].SetActive(true);
            Controller.Speed = 3;
            if(delayState == false)
            {
                StartCoroutine(InvincibleState());
            }
        }
        else if(currentState == 1)
        {
            currentState = 1;
            base.SmashEnemy();
        }

    }

    //triggers invincibility so the shell doesnt get automatically destroyed with single stomp
    private IEnumerator InvincibleState()
    {
        delayState = true;
        this.gameObject.layer = 15;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.layer = 12;
        delayState = false;
    }
}
