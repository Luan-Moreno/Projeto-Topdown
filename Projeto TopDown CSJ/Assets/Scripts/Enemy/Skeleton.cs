using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float hp;
    [SerializeField] private float hpMax;
    [SerializeField] private Image healthBar;
    [SerializeField] private Transform healthBarCanvas;
    private Quaternion initialCanvasRotation;
    public bool isDead;

    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SkeletonAnim skeletonAnim;

    private float posX;
    private Player player;
    private PlayerAnim playerAnim;
    private bool detectPlayer;

    public Image HealthBar { get => healthBar; set => healthBar = value; }
    public float Hp { get => hp; set => hp = value; }
    public float HpMax { get => hpMax; set => hpMax = value; }

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        playerAnim = player.GetComponent<PlayerAnim>();
        hp = hpMax;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        if (healthBarCanvas != null) { initialCanvasRotation = healthBarCanvas.rotation; }
    }

    void Update()
    {
        if (!isDead && detectPlayer)
        {
            agent.SetDestination(player.transform.position);
            posX = transform.position.x - player.transform.position.x;

            if (posX > 0)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 0);
            }

            if (healthBarCanvas != null) { healthBarCanvas.rotation = initialCanvasRotation; }



            if (Vector2.Distance(transform.position, player.transform.position) > agent.stoppingDistance)
            {
                skeletonAnim.PlayAnim(1);
            }
            else
            {
                if (!playerAnim.IsDying)
                {
                    skeletonAnim.PlayAnim(2);
                }
                else
                {
                    skeletonAnim.PlayAnim(0);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        DetectPlayer();
    }

    private void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if (hit != null)
        {
            detectPlayer = true;
            agent.isStopped = false;
        }
        else
        {
            detectPlayer = false;
            skeletonAnim.PlayAnim(0);
            agent.isStopped = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);  
    }

}
