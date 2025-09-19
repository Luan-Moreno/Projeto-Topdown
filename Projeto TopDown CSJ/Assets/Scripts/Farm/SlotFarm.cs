using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip carrotSFX;
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] private int digAmount;
    [SerializeField] private float waterAmount; // total de água para nascer cenoura
    [SerializeField] private bool watering;
    [SerializeField] private bool detecting;

    private int initialDigAmount;
    private float currentHoleWater;
    private bool dugHole;
    private bool plantedCarrot;
    private PlayerInventory playerInventory;

    public void Start()
    {
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        initialDigAmount = digAmount;
    }

    public void Update()
    {
        if (dugHole)
        {
            if (watering)
            {
                currentHoleWater += 0.1f;
            }

            if (detecting && currentHoleWater >= waterAmount && !plantedCarrot)
            {
                audioSource.PlayOneShot(holeSFX);
                spriteRenderer.sprite = carrot;
                plantedCarrot = true;
            }

            if (detecting && Input.GetKeyDown(KeyCode.E) && plantedCarrot)
            {
                audioSource.PlayOneShot(carrotSFX);
                spriteRenderer.sprite = hole;
                playerInventory.CurrentCarrots++;
                currentHoleWater = 0;
                plantedCarrot = false;
            }
        }
    }

    public void OnHit()
    {
        digAmount--;

        if (digAmount <= initialDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
            dugHole = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shovel"))
        {
            OnHit();
        }

        if (collision.CompareTag("Water"))
        {
            watering = true;
        }

        if (collision.CompareTag("Player"))
        {
            detecting = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            watering = false;
        }

        if (collision.CompareTag("Player"))
        {
            detecting = false;
        }
    }
}
