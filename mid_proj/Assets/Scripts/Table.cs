using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (popupPanel != null) popupPanel.SetActive(false);
        Button.selectedIngredients.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (popupPanel != null && popupPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                popupPanel.SetActive(false);
            }
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Table Clicked");
        if (popupPanel != null)
        {
            popupPanel.SetActive(true);

            // 1. Handle the Panel's own renderer (Background)
            SpriteRenderer panelSr = popupPanel.GetComponent<SpriteRenderer>();
            if (panelSr != null)
            {
                panelSr.color = new Color(panelSr.color.r, panelSr.color.g, panelSr.color.b, 1f);
                panelSr.sortingOrder = 10; // Push background to back
            }

            // 2. Handle ALL children (Ingredients, Pin, Dough)
            SpriteRenderer[] allRenderers = popupPanel.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer childSr in allRenderers)
            {
                if (childSr != panelSr)
                {
                    // Ensure opacity
                    childSr.color = new Color(childSr.color.r, childSr.color.g, childSr.color.b, 1f);
                    // Only bump up if it's behind or on the same layer as the panel
                    if (childSr.sortingOrder <= 10) childSr.sortingOrder = 20;
                }
            }
        }
        else
        {
            Debug.LogError("Popup Panel is not assigned in the Inspector!");
        }
    }
}
