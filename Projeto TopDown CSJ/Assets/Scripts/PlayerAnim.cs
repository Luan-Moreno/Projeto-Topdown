using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Player player;
    private PlayerInventory playerInventory;
    private SpriteRenderer playerSprite;
    private Animator anim;
    private Casting cast;

    private bool isHurting;
    private float timeCounter;
    private bool isDying;
    private bool onDialogue;


    [Header("Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Damage/Death")]
    [SerializeField] private float recoveryTime = 1.5f;
    [SerializeField] private GameObject skullPrefab;

    public bool IsDying { get => isDying; set => isDying = value; }
    public bool OnDialogue { get => onDialogue; set => onDialogue = value; }

    void Start()
    {
        player = GetComponent<Player>();
        playerInventory = GetComponent<PlayerInventory>();
        playerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        cast = FindAnyObjectByType<Casting>();
    }

    void Update()
    {
        OnMove();
        OnRun();
        OnUsingTools();

        if (isHurting && !IsDying)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= recoveryTime)
            {
                isHurting = false;
                timeCounter = 0f;
            }
        }
    }

    #region Movement
    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0 && player.Speed > 0)
        {
            if (player.IsRolling)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Rolling"))
                { 
                    anim.SetTrigger("isRoll");
                }
            }
            else
            {
                anim.SetInteger("transition", 1);
            }

        }
        else
        {
            anim.SetInteger("transition", 0);
        }

        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }
    void OnRun()
    {
        if (player.direction.sqrMagnitude > 0 && player.IsRunning)
        {
            anim.SetInteger("transition", 2);
        }
    }
    #endregion

    #region Tools
    void OnUsingTools()
    {
        if (player.Speed == 0 && !onDialogue)
        { 
            if (player.IsCutting)
            {
                anim.SetInteger("transition", 3);
            }

            if (player.IsDigging)
            {
                anim.SetInteger("transition", 4);
            }

            if (player.IsWatering)
            {
                anim.SetInteger("transition", 5);
            }

            if (player.IsAttacking)
            {
                anim.SetInteger("transition", 6);
            }
        }
    }
    #endregion

    #region Casting
    public void OnCastingStart()
    {
        anim.SetTrigger("isCasting");
        player.isPaused = true;
    }
    public void OnCastingFinish()
    {
        cast.OnCasting();
        player.isPaused = false;
    }
    #endregion

    #region Hammering
    public void OnHammeringStart()
    {
        transform.eulerAngles = new Vector2(0, 0);
        anim.SetBool("isHammering", true);
        player.isPaused = true;
    }
    public void OnHammeringFinish()
    {
        anim.SetBool("isHammering", false);
        player.isPaused = false;
    }
    #endregion

    #region Attack

    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if (hit != null)
        {
            hit.GetComponentInChildren<SkeletonAnim>().OnHit();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

    public void IsHurt()
    {
        if (!isHurting && !IsDying)
        {
            anim.SetTrigger("isHurt");
            isHurting = true;
            playerInventory.CurrentHp--;
        }
        if (playerInventory.CurrentHp <= 0)
        {
            StartCoroutine(DeathSequence());
        }
    }
    IEnumerator DeathSequence()
    {
            IsDying = true;
            player.isPaused = true;
            anim.SetTrigger("isDead");
            yield return new WaitForSeconds(1f);
            playerSprite.enabled = false;
            Instantiate(skullPrefab, player.transform.position, player.transform.rotation);
    }
    #endregion

}
