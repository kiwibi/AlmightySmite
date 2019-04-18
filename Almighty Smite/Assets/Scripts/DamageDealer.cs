using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int DamageAmount;
    [Tooltip("Damage type of this object.")]
    public DamageType damageType;

    public void OnChildTriggerEnter2D(Collider2D col)
    {
        DamageDealer damageDealer = col.gameObject.GetComponent<DamageDealer>();
        Debug.Log(damageDealer);
        if (damageDealer != null)
        {
            //Debug.Log(damageType.name + " hit: " + damageDealer.damageType.name);
            if (damageType.TakesDamageFrom.Contains(damageDealer.damageType))
            {
                gameObject.GetComponent<CityBehaviour>().DealDamage(damageDealer.DamageAmount, damageDealer.damageType);              
            }
            else
            {
                gameObject.GetComponent<CityBehaviour>().DealDamage(damageDealer.DamageAmount / 2, damageDealer.damageType);
            }
        }
    }
}
