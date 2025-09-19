using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private bool detectPlayer;
    [SerializeField] private int woodAmount;
    private int currentWood;
    [SerializeField] private Transform point;
    [SerializeField] private GameObject houseCollider;
    [SerializeField] private SpriteRenderer houseSprite;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private float timeToBuild;
    [SerializeField] private float timeCounter;
    [SerializeField] private bool buildStart;
    private Player player;
    private PlayerInventory playerInventory;
    private PlayerAnim playerAnim;

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        playerAnim = player.GetComponent<PlayerAnim>();
        playerInventory = player.GetComponent<PlayerInventory>();
    }

    void Update()
    {
        currentWood = playerInventory.CurrentWood;
        if (detectPlayer && Input.GetKeyDown(KeyCode.E) && currentWood >= woodAmount)
        {
            buildStart = true;
            player.transform.position = point.position;
            playerAnim.OnHammeringStart();
            houseSprite.color = startColor;
        }
        if (buildStart)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= timeToBuild)
            {
                buildStart = false;
                currentWood -= woodAmount;
                playerInventory.CurrentWood = currentWood;
                playerAnim.OnHammeringFinish();
                houseSprite.color = endColor;
                houseCollider.SetActive(true);
            }
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
