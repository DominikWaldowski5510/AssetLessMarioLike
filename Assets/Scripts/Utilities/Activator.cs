using UnityEngine;

//script that prevents enemies from moving unless they are visible by the camera
public class Activator : MonoBehaviour
{
    [SerializeField] private Renderer rend = null;              //reference to renderer 
    [SerializeField] private EnemyController scriptToEnable = null;         //reference to script that has to be disabled

    //function that is repeated constantly by 0.5f every time
    private void Start() => InvokeRepeating("Enabler", 0, 0.5f);

    //function that enables the enemy movement script when it is visible by the camera
    private void Enabler()
    {
        if (rend.isVisible)
        {
            scriptToEnable.enabled = true;
            CancelInvoke("Enabler");
        }
    }
}
