using System.Collections;
using UnityEngine;

//Controls floating,transparency and point display of 3d mesh
public class Floater : MonoBehaviour
{
    private float floatSpeed = 2;                                   //how fast the objectflies upward
    [SerializeField] private MeshRenderer mesh = null;              //mesh component that changes the colour
    [SerializeField] private TextMesh textMesh = null;              //used for modifying the text value of the mesh
    private Color beforeLerp = new Color(1, 1, 1, 1);               //default colour(fully white with no transparency)
    private Color afterLerp = new Color(1, 1, 1, 0);                //fully transparent white

    //sets the colour to default value and starts colour change corutine
    private void OnEnable()
    {
        mesh.material.color = beforeLerp;
        StartCoroutine(ColourLerper());
    }

    //Moves the object constantly upward
    private void Update() => transform.Translate(Vector3.up * floatSpeed * Time.deltaTime);

    //function that changes between non-transparent to fully transparent colour and then disables the object
    private IEnumerator ColourLerper()
    {
        float elapsedTime = 0;
        float waitTime = 2;
        while (elapsedTime < waitTime)
        {
            mesh.material.color = Color.Lerp(beforeLerp, afterLerp, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }
        this.gameObject.SetActive(false);
        yield return null;
    }

    //changes the points text to desired amount
    public void SetPointDisplay(int pointAmount) => textMesh.text = "+" + pointAmount;

}
