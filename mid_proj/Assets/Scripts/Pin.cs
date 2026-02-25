using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private Dough dough;
    private float originalY;
    private bool isDragging = false;
    private Vector3 offset;
    private bool hitTop = false;
    private bool hitBottom = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalY = transform.localPosition.y;
        if (dough == null) dough = FindFirstObjectByType<Dough>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Convert mouse world position to local position relative to parent
            // This assumes the Pin is a child of the Panel
            Vector3 localMouse = transform.parent != null ? 
                transform.parent.InverseTransformPoint(mousePos) : mousePos;

            float newY = localMouse.y + offset.y;

            // Clamp movement 50px (units) up and down
            if (newY > originalY + 50f) newY = originalY + 50f;
            if (newY < originalY - 50f) newY = originalY - 50f;

            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);

            // Check for cycles
            if (newY >= originalY + 49f)
            {
                if (!hitTop)
                {
                    hitTop = true;
                    if (dough != null) dough.RegisterMove(true);
                }
            }
            else if (newY <= originalY - 49f)
            {
                if (!hitBottom)
                {
                    hitBottom = true;
                    if (dough != null) dough.RegisterMove(false);
                }
            }

            // Reset flags when moving back towards center
            if (newY < originalY + 40f) hitTop = false;
            if (newY > originalY - 40f) hitBottom = false;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 localMouse = transform.parent != null ? 
            transform.parent.InverseTransformPoint(mousePos) : mousePos;
        offset = transform.localPosition - localMouse;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void OnDisable()
    {
        isDragging = false;
    }
}
