using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] private bool detectPlayer;
    private PlayerInventory player;
    private PlayerAnim playerAnim;

    [SerializeField] private int fishPercentage;
    [SerializeField] private GameObject fishPrefab;

    void Start()
    {
        player = FindFirstObjectByType<PlayerInventory>();
        playerAnim = player.GetComponent<PlayerAnim>();
    }

    void Update()
    {
        if (detectPlayer && Input.GetKeyDown(KeyCode.E))
        {
            playerAnim.OnCastingStart();
        }
    }

    public void OnCasting()
    {
        int randomValue = Random.Range(1, 100);

        if (randomValue <= fishPercentage)
        {
            Instantiate(fishPrefab, player.transform.position + new Vector3(Random.Range(-2.5f, -1f), 0f, 0f), Quaternion.identity);
            Debug.Log("pescou");
        }
        else
        { 
            Debug.Log("não pescou");
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
