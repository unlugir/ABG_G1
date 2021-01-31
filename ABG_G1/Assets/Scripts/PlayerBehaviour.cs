using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] public Weapon weapon;
    [SerializeField] LayerMask enemyLayer;
    Stats playerStats;
    private void Start() 
    {
        playerStats = GetComponent<Stats>();
    }
    public void Attack(Combination combination)
    {
        combination.Cast(weapon, transform.position);
    }
}
