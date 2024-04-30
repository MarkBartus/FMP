using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 3;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject ragdoll;

    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;

    public float turnRate = 3f;
    GameObject player;
    Animator anim;
    NavMeshAgent agent;
    float timePassed;
    float newDestinationCD = 0.5f;
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        anim.SetFloat("speed", agent.velocity.magnitude / agent.speed);

        if ( player == null )
        {
            return;
        }
        
        if (timePassed >= attackCD)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                anim.SetTrigger("attack");
                timePassed = 0;
            }
        }
        timePassed += Time.deltaTime;
        
        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }
        
        newDestinationCD -= Time.deltaTime;


        FaceTarget();
    }
    
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime * 5);

    }
    
    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
    
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        anim.SetTrigger("damage");

        if (health < 0)
        {
            Die();
        }
    }

    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }
    void Die()
    {
        Instantiate(ragdoll, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
     
}
