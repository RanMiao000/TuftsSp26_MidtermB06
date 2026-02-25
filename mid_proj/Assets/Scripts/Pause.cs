using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (popupPanel != null) popupPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("Pause Button Clicked");
        if (popupPanel != null)
        {
            popupPanel.SetActive(true);

            // Smart Sort: Check for scripts to decide what is content and what is background
            SpriteRenderer[] allRenderers = popupPanel.GetComponentsInChildren<SpriteRenderer>(true);
            foreach (SpriteRenderer sr in allRenderers)
            {
                // Ensure opacity
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);

                // Check if this is the main panel object
                if (sr.gameObject == popupPanel)
                {
                    sr.sortingOrder = 10; // Background depth
                    continue; // Don't move the panel itself
                }

                // If it has a button script, bring to front
                if (sr.GetComponentInParent<Resume>() != null || sr.GetComponentInParent<Quit>() != null)
                {
                    sr.sortingOrder = 20;
                    sr.transform.localPosition = new Vector3(sr.transform.localPosition.x, sr.transform.localPosition.y, -1f);
                }
                else
                {
                    // Otherwise, treat it as background
                    sr.sortingOrder = 10;
                    sr.transform.localPosition = new Vector3(sr.transform.localPosition.x, sr.transform.localPosition.y, 0f);
                }
            }

            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogError("Popup Panel is not assigned in the Inspector!");
        }
    }
}
