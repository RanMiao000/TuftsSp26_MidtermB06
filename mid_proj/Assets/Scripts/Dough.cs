using UnityEngine;
using UnityEngine.UI; // Required for Text

public class Dough : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject panel;
    [SerializeField] private Text warningText; // Assign a UI Text in Inspector
    private Oven oven;
    private SpriteRenderer sr;

    private int upCount = 0;
    private int downCount = 0;
    private int stage = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            // If not on the parent, look for the renderer on the child object
            sr = GetComponentInChildren<SpriteRenderer>();
        }
        oven = FindFirstObjectByType<Oven>();
        if (panel == null && transform.parent != null) panel = transform.parent.gameObject;
        if (warningText != null) warningText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterMove(bool isUp)
    {
        if (isUp) upCount++;
        else downCount++;

        if (stage == 0 && upCount >= 5 && downCount >= 5)
        {
            stage = 1;
            upCount = 0;
            downCount = 0;
            if (sr != null && sprites.Length > 0) sr.sprite = sprites[0];
        }
        else if (stage == 1 && upCount >= 5 && downCount >= 5)
        {
            // Validate Ingredients
            if (Button.selectedIngredients.Count < 4)
            {
                if (warningText != null)
                {
                    warningText.text = "Pick at least 4 ingredients from the table";
                    warningText.color = Color.red;
                    warningText.gameObject.SetActive(true);
                }
                return; // Stop here, do not proceed to cooking
            }

            stage = 2;
            if (sr != null && sprites.Length > 1) sr.sprite = sprites[1];
            if (panel != null) panel.SetActive(false);
            if (oven != null) oven.StartCooking();
        }
    }
}
