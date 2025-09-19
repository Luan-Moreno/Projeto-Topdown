using UnityEngine;

public class CollectWater : MonoBehaviour
{
    [SerializeField] private bool detectPlayer;
    [SerializeField] private int waterValue;
    private PlayerInventory player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerInventory>();
    }

    void Update()
    {
        if (detectPlayer && Input.GetKeyDown(KeyCode.E))
        {
            player.WaterLimit(waterValue);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectPlayer = false;
        }
    }
}
