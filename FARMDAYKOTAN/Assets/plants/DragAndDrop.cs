using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset; // Mouse ilə obyekt arasındakı fərq
    private Vector3 originalPosition; // Obyektin ilkin mövqeyi
    private bool isDragging = false; // Obyekt hərəkət edir?
    private Camera mainCamera;

    public static bool isObjectDragging = false; // Tikinti hərəkət edərkən kamera dayanır

    private float pressTime = 0f; // Mouse basılma vaxtı
    public float holdTimeToDrag = 2f; // Hərəkət etdirmək üçün tələb olunan vaxt (saniyə ilə)

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position; // İlk mövqeyi yadda saxla
    }

    void OnMouseDown()
    {
        pressTime = Time.time; // Mouse basılma vaxtını qeyd et
    }

    void OnMouseDrag()
    {
        // Mouse 2 saniyədən çox basılıbsa hərəkət başlasın
        if (Time.time - pressTime >= holdTimeToDrag && !isDragging)
        {
            isDragging = true;
            isObjectDragging = true; // Tikinti hərəkət edir, kamera dayanır
            offset = transform.position - GetMouseWorldPosition();
        }

        if (isDragging)
        {
            Vector3 newPosition = GetMouseWorldPosition() + offset;

            // Yalnız X və Y oxlarında hərəkət etdir, Z sabit qalsın
            transform.position = new Vector3(newPosition.x, newPosition.y, originalPosition.z);
        }
    }

    void OnMouseUp()
    {
        // Mouse buraxıldıqda hərəkəti dayandır
        isDragging = false;
        isObjectDragging = false;

        // Tikinti yalnız zəmin üzərində qalmalıdır
        if (IsOnGround())
        {
            originalPosition = transform.position; // Mövqeyi yenilə
        }
        else
        {
            transform.position = originalPosition; // Əvvəlki mövqeyə qaytar
        }

        // Mouse basılma vaxtını sıfırla
        pressTime = 0f;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        return transform.position;
    }

    private bool IsOnGround()
    {
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1f))
        {
            return hit.collider.CompareTag("Ground");
        }
        return false;
    }
}
