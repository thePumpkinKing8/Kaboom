using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageSO", menuName = "SOs/DamageSO")]
public class DamageSO : ScriptableObject
{
    public float TurretDamage = 2f; //Individual bullets
    public float BarrelDamage = 50f; //Exploding barrels
}
