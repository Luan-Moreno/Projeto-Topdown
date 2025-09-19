using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float treeHealth;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private int totalWood;
    [SerializeField] private ParticleSystem leaves;
    [SerializeField] private GameObject stumpPrefab;
    private bool isCut;
    void Start()
    {

    }

    void Update()
    {

    }

    public void OnHit()
    {
        treeHealth--;

        anim.SetTrigger("isHit");
        leaves.Play();

        if (treeHealth <= 0)
        {
            for (int i = 0; i < totalWood; i++)
            {
                Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), transform.rotation);
            }

            isCut = true;
            anim.SetTrigger("cut");
        }
    }

    public void SpawnTump()
    {
        Vector3 stumpOffset = new Vector3(0.04f, -0.25f, 0);
        Instantiate(stumpPrefab, transform.position + stumpOffset, Quaternion.identity);
        Destroy(gameObject, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe") && !isCut)
        {
            OnHit();
        }
    }
}
