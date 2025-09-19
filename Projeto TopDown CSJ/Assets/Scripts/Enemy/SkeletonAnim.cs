using UnityEngine;

public class SkeletonAnim : MonoBehaviour
{
    private Animator anim;
    private PlayerAnim playerAnim;
    private Player player;
    private Skeleton skeleton;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;
    void Start()
    {
        anim = GetComponent<Animator>();
        playerAnim = FindAnyObjectByType<PlayerAnim>();
        player = playerAnim.GetComponent<Player>();
        skeleton = GetComponentInParent<Skeleton>();
    }

    public void PlayAnim(int animValue)
    {
        anim.SetInteger("transition", animValue);
    }

    public void Attack()
    {
        if (!skeleton.isDead)
        { 
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

            if (hit != null && playerAnim.IsDying == false)
            {
                playerAnim.IsHurt();
            }
        }
    }

    public void OnHit()
    {
        if (skeleton.Hp <= 0)
        {
            skeleton.isDead = true;
            anim.SetTrigger("onDeath");

            Destroy(skeleton.gameObject, 1.5f);
        }
        else
        {
            anim.SetTrigger("onHurt");
            skeleton.Hp--;
            skeleton.HealthBar.fillAmount = skeleton.Hp / skeleton.HpMax;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
