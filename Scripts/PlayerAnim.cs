using UnityEngine;

public class PlayerAnim : MonoBehaviour
{   
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;


    private Player player;
    private Animator anim; // acessa Animator para manipular o valor de transition

    private Casting cast;

    private bool isHitting;
    private float recoveryTime = 1.5f; //animação de dano a cada 1s
    private float timeCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();

        cast = FindAnyObjectByType<Casting>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMove();
        OnRun();

        if(isHitting)
        {
            timeCount += Time.deltaTime;

            if(timeCount >= recoveryTime)
            {
                isHitting = false;
                timeCount = 0f;
            }
        }
        
    }

    #region Movement

    #region Attack

    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);

        if(hit != null)
        {
            // atacou o inimigo
            hit.GetComponentInChildren<AnimationControl>().OnHit();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);     
    }

    #endregion
    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0) 
        {
            if(player.isRolling)
            {
                anim.SetTrigger("IsRoll");
            }
            else
            {
                anim.SetInteger("Transition", 1);
            }
        }
        else
        {
            anim.SetInteger("Transition", 0);
        }

        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        
        if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if(player.isCutting)
        {
            anim.SetInteger("Transition", 3);
        }

        if(player.isDigging)
        {
            anim.SetInteger("Transition", 4);
        }

        if(player.isWatering)
        {
            anim.SetInteger("Transition", 5);
        }
    }

    void OnRun()
    {
        if(player.isRunning)
        {
            anim.SetInteger("Transition", 2);
        }
    }

    #endregion

    public void OnHit()
    {
        if(!isHitting)
        {
            anim.SetTrigger("hit");
            isHitting = true;
        }
        
    }

    // é chamado quando o jogador pressiona o botão de ação na água
    public void OnCastingStarted()
    {
        anim.SetTrigger("isCasting");
        player.isPaused = true;
    }

    // é chamado quando termina de executar a animação de pescaria 
    public void OnCastingEnded()
    {
        cast.OnCasting();
        player.isPaused = false;
    }

    public void OnHameringStarted()
    {
        anim.SetBool("hammering", true);
    }

    public void OnHameringEnded()
    {
        anim.SetBool("hammering", false);
    }    
}
