using UnityEngine;

public class ShowIngredient : MonoBehaviour
{
    public GameObject ingredient;  // 拖入这个 Button 对应的配料 Image

    public void ShowIt()
    {
        ingredient.SetActive(true);
    }
}