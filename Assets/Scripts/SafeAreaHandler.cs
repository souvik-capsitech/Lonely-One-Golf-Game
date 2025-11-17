using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SafeAreaHandler : MonoBehaviour
{
    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;

        // Convert safeArea from pixels to viewport (0-1)
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        // Adjust camera orthographic size or viewport
        // Example: only for orthographic camera
        if (cam.orthographic)
        {
            float screenAspect = (float)Screen.width / Screen.height;
            float safeAspect = safeArea.width / safeArea.height;

            // Adjust orthographic size to fit safe area
            if (safeAspect < screenAspect)
            {
                cam.orthographicSize = cam.orthographicSize * (screenAspect / safeAspect);
            }
        }

        // Optional: store anchors for UI or gameplay bounds
        // Vector2 safeMin = anchorMin;
        // Vector2 safeMax = anchorMax;
    }
}
