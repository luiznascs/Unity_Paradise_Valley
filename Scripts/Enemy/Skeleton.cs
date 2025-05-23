using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [Header("Stats")]
    public float radius;
    public LayerMask layer;
    public float totalHealth;
    public float currentHealth;
    public Image healthBar;
    public bool isDead;
    

    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControl animControl;

    private Player player;
    private bool detectPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = totalHealth;
        player = FindFirstObjectByType<Player>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        Debug.Log(player);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead && detectPlayer)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);

            if(Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                // chegou no limite de distância / skeleton para
                animControl.PlayerAnim(2);
            }
            else
            {
                // skeleton segue o player
                animControl.PlayerAnim(1);
            }

            float posX = player.transform.position.x - transform.position.x;

            if(posX > 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
    }

    private void FixedUpdate()
    {
        DetectPlayer();
    }
    public void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, layer);

        if (hit != null)
        {
            // enxergou o player
            detectPlayer = true;
        }
        else
        {
            // não está enxergando
            detectPlayer = false;
            animControl.PlayerAnim(0);
            agent.isStopped = true;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
