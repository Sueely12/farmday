using UnityEngine;

public class BuildingUI : MonoBehaviour
{
    public GameObject uiPanel; // Tikinti üçün UI Panel
    public Vector3 uiOffset = new Vector3(0, 2, 0); // Tikintinin üstündə UI üçün mövqe fərqi

    private void Start()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false); // UI başlanğıcda bağlıdır

            // UI-ni tikintinin üzərində yerləşdir
            uiPanel.transform.SetParent(transform); // Tikintini valideyn olaraq təyin et
            uiPanel.transform.localPosition = uiOffset; // Tikintiyə nisbətən mövqeyini təyin et
        }
    }

    private void OnMouseDown()
    {
        // UI açılır və digər panelləri bağlamaq üçün yoxlayır
        if (uiPanel != null)
        {
            ToggleUI();
        }
    }

    private void ToggleUI()
    {
        bool isActive = uiPanel.activeSelf;

        // Əgər bu UI açıqdırsa, bağla, yoxsa aç
        uiPanel.SetActive(!isActive);

        // Digər tikintilərin UI-lərini bağla
        CloseOtherUIs();
    }

    private void CloseOtherUIs()
    {
        BuildingUI[] allBuildings = FindObjectsOfType<BuildingUI>();
        foreach (BuildingUI building in allBuildings)
        {
            if (building != this && building.uiPanel != null)
            {
                building.uiPanel.SetActive(false);
            }
        }
    }
}
