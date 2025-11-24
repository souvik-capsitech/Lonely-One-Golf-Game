using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{
    public float targetSize = 3f;        // how far to zoom in
    public float originalSize = 5f;      // default size of your game camera
    public float zoomSpeed = 2f;         // zoom smoothness

    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public void TriggerZoom(Vector3 targetPos)
    {
        StopAllCoroutines();
        StartCoroutine(ZoomIn(targetPos));
    }

    public void ResetZoom()
    {
        StopAllCoroutines();
        StartCoroutine(ZoomOut());
    }

    IEnumerator ZoomIn(Vector3 targetPos)
    {
        float startSize = cam.orthographicSize;
        Vector3 startPos = transform.position;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * zoomSpeed;

            cam.orthographicSize = Mathf.Lerp(startSize, targetSize, t);

         
            transform.position = new Vector3(
                Mathf.Lerp(startPos.x, targetPos.x, t),
                startPos.y,
                startPos.z
            );

            yield return null;
        }
    }


    IEnumerator ZoomOut()
    {
        float startSize = cam.orthographicSize;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * zoomSpeed;
            cam.orthographicSize = Mathf.Lerp(startSize, originalSize, t);
            yield return null;
        }
    }
}
