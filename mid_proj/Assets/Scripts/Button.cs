using UnityEngine;
using System.Collections.Generic;

public class Button : MonoBehaviour
{
    public static List<string> selectedIngredients = new List<string>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.X))
        {
            string ingredient = gameObject.tag;
            if (!selectedIngredients.Contains(ingredient))
            {
                selectedIngredients.Add(ingredient);
                Debug.Log("Added ingredient: " + ingredient);
            }
        }
    }
}
