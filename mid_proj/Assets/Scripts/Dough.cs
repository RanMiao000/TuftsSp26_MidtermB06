using UnityEngine;

public class Dough : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private GameObject panel;
    private Oven oven;
    private SpriteRenderer sr;

    private int upCount = 0;
    private int downCount = 0;
    private int stage = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        oven = FindFirstObjectByType<Oven>();
        if (panel == null && transform.parent != null) panel = transform.parent.gameObject;
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
            if (sprites.Length > 0) sr.sprite = sprites[0];
        }
        else if (stage == 1 && upCount >= 5 && downCount >= 5)
        {
            stage = 2;
            if (sprites.Length > 1) sr.sprite = sprites[1];
            if (panel != null) panel.SetActive(false);
            if (oven != null) oven.StartCooking();
        }
    }
}
