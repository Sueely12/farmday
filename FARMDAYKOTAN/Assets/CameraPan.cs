using UnityEngine;

public class CameraPanZoom : MonoBehaviour
{
    public float panSpeed = 10f; // Kamera hərəkət sürəti
    public Vector2 panLimitX = new Vector2(-50, 50); // X oxu limitləri
    public Vector2 panLimitY = new Vector2(-50, 50); // Y oxu limitləri
    public float zoomSpeed = 2f; // Zoom sürəti
    public float minZoom = 5f; // Ən yaxın zoom səviyyəsi
    public float maxZoom = 20f; // Ən uzaq zoom səviyyəsi

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Əgər tikinti hərəkət etmirsə, kameranı idarə et
        if (!DragAndDrop.isObjectDragging)
        {
            HandlePan(); // Hərəkət
            HandleZoom(); // Zoom
        }
    }

    void HandlePan()
    {
        if (Input.GetMouseButton(0)) // Sol klik və ya toxunma davam edir
        {
            Vector3 difference = new Vector3(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"), 0);
            Vector3 newPosition = transform.position + difference * panSpeed * Time.deltaTime;

            // Limitləri tətbiq et
            newPosition.x = Mathf.Clamp(newPosition.x, panLimitX.x, panLimitX.y);
            newPosition.y = Mathf.Clamp(newPosition.y, panLimitY.x, panLimitY.y);
            transform.position = newPosition;
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // PC üçün zoom
        float newSize = mainCamera.orthographicSize - scroll * zoomSpeed;
        mainCamera.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
    }
}
