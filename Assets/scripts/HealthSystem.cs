using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] public float health = 10;
    [SerializeField] GameObject hitVFX;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if ( health < 0 )
        {

            Die();
        }
    }

    void Die()
    {
        anim.SetTrigger("dead");
        Destroy(this.gameObject,5f);
    }   
    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
