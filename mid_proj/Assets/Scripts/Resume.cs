using UnityEngine;

public class Resume : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Debug.Log("Resume Clicked");
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Debug.LogError("Popup Panel is not assigned in the Inspector!");
        }
    }
}
