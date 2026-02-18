using UnityEngine;
using UnityEngine.UI;

public class OxygenSlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private float decaySpeed = 0.05f; // Adjusted for "slow rate"
    private Player player;

    void Start()
    {
        // Auto-connect to UI: Try to find Slider on this object, or find one in the scene
        if (slider == null) slider = GetComponent<Slider>();
        if (slider == null) slider = FindFirstObjectByType<Slider>();

        player = FindFirstObjectByType<Player>();
        
        // Initialize full oxygen
        if (slider != null) 
        {
            slider.maxValue = 1f;
            slider.value = 1f;
        }
    }

    void Update()
    {
        if (slider != null)
        {
            slider.value -= decaySpeed * Time.deltaTime;
            if (slider.value <= 0 && player != null)
            {
                player.LoseLife();
            }
        }
    }

    public void Refill()
    {
        if (slider != null) slider.value = 1f;
    }
}
