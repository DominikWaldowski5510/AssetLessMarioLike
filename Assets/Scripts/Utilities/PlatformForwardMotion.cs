using UnityEngine;

//makes platform move forward
public class PlatformForwardMotion : MonoBehaviour
{
   [SerializeField] private float platformSpeed = 1.5f;         //the speed at which the object moves

    //moves the object in straight position using its forward vector
    private void Update() => transform.Translate(Vector3.forward * Time.deltaTime * platformSpeed, Space.Self);

}
