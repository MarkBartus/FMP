using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(weapon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartDealDamage()
    {
        weapon.GetComponentInChildren<DamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        weapon.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }
}
