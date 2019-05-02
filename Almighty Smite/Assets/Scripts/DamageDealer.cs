using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int DamageAmount;                                                                                                                                    //mängd dmg objektet ska gör
    [Tooltip("Damage type of this object.")]
    public DamageType damageType;                                                                                                                               //vilken sorts dmg är det

    public void OnChildTriggerEnter2D(Collider2D col)
    {
        DamageDealer damageDealer = col.gameObject.GetComponent<DamageDealer>();                                                                                //vilken sorts damage tod objektet
        if (damageDealer != null)                                                                                                                               //om det faktiskt va något som gjorde dmg
        {
            //Debug.Log(damageType.name + " hit: " + damageDealer.damageType.name);
            if (damageType.TakesDamageFrom.Contains(damageDealer.damageType))                                                                                   //om objektet ska ta full dmg från det som träffade
            {
                gameObject.GetComponent<CityBehaviour>().DealDamage(damageDealer.DamageAmount, damageDealer.damageType);                                        //kalla funktionen på huset som gör dmg
            }
            else                                                                                                                                                //om den inte skulle göra full dmg
            {
                gameObject.GetComponent<CityBehaviour>().DealDamage(damageDealer.DamageAmount / 2, damageDealer.damageType);                                    //gör hälften av dmg på huset
            }
        }
    }
}
