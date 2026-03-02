using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private Dough dough;
    [SerializeField] private float moveRange = 10.0f; // Adjustable range, 50f was likely too big
    [SerializeField] private float speed = 5.0f;
    private float originalY;
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
        float input = Input.GetAxis("Vertical");
        
        if (Mathf.Abs(input) > 0.01f)
        {
            float newY = transform.localPosition.y + (input * speed * Time.deltaTime);

            // Clamp movement 50px (units) up and down
            if (newY > originalY + moveRange) newY = originalY + moveRange;
            if (newY < originalY - moveRange) newY = originalY - moveRange;

            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);

            // Rotate around X axis between -20 and 20 based on position
            float rotationAngle = ((newY - originalY) / moveRange) * 20f;
            transform.localRotation = Quaternion.Euler(rotationAngle, 0, 0);

            // Check for cycles
            if (newY >= originalY + (moveRange - 0.1f))
            {
                if (!hitTop)
                {
                    hitTop = true;
                    if (dough != null) dough.RegisterMove(true);
                }
            }
            else if (newY <= originalY - (moveRange - 0.1f))
            {
                if (!hitBottom)
                {
                    hitBottom = true;
                    if (dough != null) dough.RegisterMove(false);
                }
            }

            // Reset flags when moving back towards center
            if (newY < originalY + (moveRange * 0.8f)) hitTop = false;
            if (newY > originalY - (moveRange * 0.8f)) hitBottom = false;
        }
    }
}
