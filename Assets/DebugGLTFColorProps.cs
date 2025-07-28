using UnityEngine;

public class DebugGLTFColorProps : MonoBehaviour
{
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null) return;

        foreach (Material mat in renderer.materials)
        {
            Debug.Log("Checking material: " + mat.name);

            // Check for color properties
            if (mat.HasProperty("_BaseColor"))
                Debug.Log("Has _BaseColor");

            if (mat.HasProperty("_BaseColorFactor"))
                Debug.Log("Has _BaseColorFactor");

            if (mat.HasProperty("_Color"))
                Debug.Log("Has _Color");

            // You can also try setting it:
            mat.SetColor("_BaseColor", Color.cyan); // test tint
        }
    }
}
