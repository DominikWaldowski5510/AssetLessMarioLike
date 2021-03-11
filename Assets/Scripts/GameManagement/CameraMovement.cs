using UnityEngine;

//camera that moves forward in x axis by following the player
public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObj = null;               //object reference to active player
    private Camera cam;                         //camera script reference
    private Vector3 cameraPos;                  //position of the camera in game scene
    [SerializeField] private Vector3 startPos;      //default start position used for resetting player camera follow position

    private void Awake() => startPos = this.transform.position;


    //initializes components by dragging the references
    private void Start()
    {
        cam = this.gameObject.GetComponent<Camera>();
        cameraPos = cam.transform.position;
    }

    //resets camera position back to default position
    public void ResetPoisition()
    {
        this.transform.position = startPos;
    }

    //updates the cameras position based on current player position only in positive x coordinate
    private void LateUpdate()
    {
        if(cam.transform.position.x < playerObj.transform.position.x)
        {
            cameraPos.x = playerObj.transform.position.x;
            cam.transform.position = cameraPos;
        }
    }
}
