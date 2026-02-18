using UnityEngine;
using System.Collections;

public class Oxygen : MonoBehaviour
{
    private Camera mainCamera;
    private Collider2D col;
    private Renderer rend;

    void Start()
    {
        mainCamera = Camera.main;
        col = GetComponent<Collider2D>();
        rend = GetComponent<Renderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Oxygen Collected!");
            // Refill oxygen
            OxygenSlider slider = FindFirstObjectByType<OxygenSlider>();
            if (slider != null) slider.Refill();

            // Start respawn sequence
            StartCoroutine(RespawnRoutine());
        }
    }

    IEnumerator RespawnRoutine()
    {
        // "Destroy" the object visually and physically so the player can't touch it
        if (col) col.enabled = false;
        if (rend) rend.enabled = false;

        yield return new WaitForSeconds(10f);

        // Respawn at random reachable point on screen (Viewport 0.1 to 0.9 ensures it's visible/reachable)
        float randomX = Random.Range(0.1f, 0.9f);
        float randomY = Random.Range(0.1f, 0.9f);
        Vector3 newPos = mainCamera.ViewportToWorldPoint(new Vector3(randomX, randomY, 10f));
        newPos.z = transform.position.z;
        transform.position = newPos;

        // "Re-create" the object by enabling it again
        if (col) col.enabled = true;
        if (rend) rend.enabled = true;
    }
}