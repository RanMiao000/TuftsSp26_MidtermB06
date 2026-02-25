using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Start";

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
        Time.timeScale = 1f; // Ensure time is running if we quit from pause
        SceneManager.LoadScene(sceneToLoad);
    }
}
