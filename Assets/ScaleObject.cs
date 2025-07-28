using UnityEngine;

public class ScaleObject : MonoBehaviour
{
    public float scaleSpeed = 0.5f; // Speed of scaling

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;

            // Prevent from getting too small
            if (transform.localScale.x < 0.1f)
                transform.localScale = Vector3.one * 0.1f;
        }
    }
}
