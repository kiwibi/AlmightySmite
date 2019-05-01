using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DamageType/DamageType")]
public class DamageType : ScriptableObject
{
    [Tooltip("The objects which inflict damage from this object.")]
    public List<DamageType> TakesDamageFrom = new List<DamageType>();                                                                                       //lista över vad som kan skada en en annan type
}