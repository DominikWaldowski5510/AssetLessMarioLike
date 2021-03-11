using UnityEngine;

//script used for rotating coins, in the original it would just be a 2d sprite swap but we use 3d models so its better to just rotate them
public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationRate = 35;                //rate at which the rotation happens

    //performs rotation 
    private void Update() => this.transform.Rotate(0, 0, -rotationRate * Time.deltaTime);

}
