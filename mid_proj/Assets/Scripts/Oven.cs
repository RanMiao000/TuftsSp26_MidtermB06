using UnityEngine;
using System.Collections;

public class Oven : MonoBehaviour
{
    [SerializeField] private Sprite cookingSprite;
    private Sprite originalSprite;
    private SpriteRenderer sr;
    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr) originalSprite = sr.sprite;
        player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCooking()
    {
        StartCoroutine(CookRoutine());
    }

    IEnumerator CookRoutine()
    {
        if (sr && cookingSprite) sr.sprite = cookingSprite;

        yield return new WaitForSeconds(20f);

        if (sr) sr.sprite = originalSprite;
        
        // Create a unique name based on ingredients
        string pizzaName = "Plain Pizza";
        if (Button.selectedIngredients.Count > 0)
        {
            pizzaName = string.Join("-", Button.selectedIngredients) + " Pizza";
        }

        // Add to static player inventory (works even if Player isn't in this scene)
        Player.AddPizza(pizzaName);
        
        // Clear ingredients for the next pizza
        Button.selectedIngredients.Clear();
    }
}
