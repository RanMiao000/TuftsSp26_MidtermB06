using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private GameObject[] lives;
    [SerializeField] private Transform baseObject;
    private Rigidbody2D rb;
    private Vector2 movement;
    private int currentLives;
    private bool isRespawning = false;
    
    // Static list ensures inventory persists between scenes
    public static List<string> pizzaInventory = new List<string>();
    private bool canDeliver = true;

    void Start()
    {
        // Ensure the game time is running when the scene starts
        Time.timeScale = 1f;

        rb = GetComponent<Rigidbody2D>();
        
        // Auto-find lives if not assigned
        if (lives == null || lives.Length == 0)
        {
            Life[] lifeScripts = FindObjectsByType<Life>(FindObjectsSortMode.None);
            lives = new GameObject[lifeScripts.Length];
            for (int i = 0; i < lifeScripts.Length; i++) lives[i] = lifeScripts[i].gameObject;
        }
        currentLives = lives.Length; // Starts with 3 lives if you have 3 heart objects

        // Auto-find base if not assigned
        if (baseObject == null)
        {
            GameObject baseObj = GameObject.FindGameObjectWithTag("Base");
            if (baseObj != null) baseObject = baseObj.transform;
        }
    }

    void Update()
    {
        // Get input from Arrow keys (or WASD)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY).normalized;

        // Face right by default (0 rotation). If moving left, rotate 180.
        if (moveX < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (moveX > 0)
            transform.rotation = Quaternion.identity;
    }

    void FixedUpdate()
    {
        // Move via Rigidbody to respect collisions (like the Rock)
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    public void LoseLife()
    {
        if (isRespawning) return;

        currentLives--;

        // Destroy one life object
        if (currentLives >= 0 && currentLives < lives.Length)
        {
            if (lives[currentLives] != null)
                Destroy(lives[currentLives]);
        }

        if (currentLives <= 0)
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene("EndLose");
        }
        else
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    private IEnumerator RespawnRoutine()
    {
        isRespawning = true;

        // Hide player and disable physics
        Renderer rend = GetComponent<Renderer>();
        Collider2D col = GetComponent<Collider2D>();
        if (rend) rend.enabled = false;
        if (col) col.enabled = false;
        if (rb) rb.simulated = false;

        yield return new WaitForSeconds(2f);

        if (baseObject != null) 
        {
            transform.position = baseObject.position;
        }
        else
        {
            Debug.LogWarning("Base Object not found! Player respawning at current location.");
        }
        
        // Refill oxygen on respawn so player doesn't die immediately again
        OxygenSlider slider = FindFirstObjectByType<OxygenSlider>();
        if (slider != null) slider.Refill();

        // Show player and enable physics
        if (rend) rend.enabled = true;
        if (col) col.enabled = true;
        if (rb) rb.simulated = true;

        isRespawning = false;
    }

    // Static method so Oven can call it even if Player isn't in the Kitchen scene
    public static void AddPizza(string pizzaName)
    {
        pizzaInventory.Add(pizzaName);
        Debug.Log("Pizza Baked: " + pizzaName + "! Total: " + pizzaInventory.Count);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we hit a target building for the Ocean_game_handler
        Ocean_game_handler ogh = FindFirstObjectByType<Ocean_game_handler>();
        if (ogh != null) ogh.CheckBuilding(collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Building"))
        {
            HandleDelivery();
        }
    }

    IEnumerator DeliveryCooldown()
    {
        canDeliver = false;
        yield return new WaitForSeconds(10f);
        canDeliver = true;
    }

    private void HandleDelivery()
    {
        if (canDeliver && pizzaInventory.Count > 0)
        {
            string deliveredPizza = pizzaInventory[0];
            pizzaInventory.RemoveAt(0);
            Debug.Log("Pizza Delivered: " + deliveredPizza);
            StartCoroutine(DeliveryCooldown());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Ocean_game_handler ogh = FindFirstObjectByType<Ocean_game_handler>();
        if (ogh != null) ogh.CheckBuilding(other.gameObject.tag);

        if (other.gameObject.CompareTag("Building")) HandleDelivery();
    }
}
