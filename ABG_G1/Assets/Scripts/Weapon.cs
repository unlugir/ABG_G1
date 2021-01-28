using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon", menuName = "ABG/Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    [SerializeField] public float attackRange;
    [SerializeField] public float damage;
    [SerializeField] public float attackSpeed;
}
