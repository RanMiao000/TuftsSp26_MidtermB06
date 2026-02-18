using UnityEngine;

public class Jellyfish : MonoBehaviour
{
    [SerializeField] private float speed = 2.5f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Jellyfish moves right (opposite of Shark)
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Check if the jellyfish is outside the screen to the right
        // WorldToViewportPoint converts position to 0-1 range (0,0 is bottom-left)
        Vector3 viewportPos = mainCamera.WorldToViewportPoint(transform.position);
        
        if (viewportPos.x > 1.2f) // 1.2f ensures it is fully off-screen to the right
        {
            // Respawn to the left of the screen (-0.2f) at a random height
            float randomY = Random.Range(0.1f, 0.9f);
            Vector3 newPos = mainCamera.ViewportToWorldPoint(new Vector3(-0.2f, randomY, viewportPos.z));
            newPos.z = transform.position.z; // Maintain original Z depth
            transform.position = newPos;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null) player.LoseLife();
        }
        else if (collision.gameObject.CompareTag("Rock"))
        {
            // Move above the rock collider and continue moving
            Collider2D rockCollider = collision.collider;
            // Calculate a position above the rock (using bounds.max.y) plus a small buffer
            float newY = rockCollider.bounds.max.y + 1.0f;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
