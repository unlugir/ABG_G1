using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayerController : MonoBehaviour
{
    [SerializeField] public Weapon weapon;
    [SerializeField] LayerMask enemyLayer;
    [ExecuteInEditMode]
    [SerializeField] bool enableGizmo;
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DebugAttack();
        }    
    }
    public void DebugAttack()
    {
        var attackedEnemies = Physics.OverlapSphere
            (transform.position, weapon.attackRange, enemyLayer);
        foreach(var enemy in attackedEnemies)
        {
            Stats enemyStats = enemy.GetComponent<Stats>();
            if (!enemyStats.isAlive) continue;
            enemyStats.TakeDamage(weapon.damage);
            enemy.GetComponent<Rigidbody>().AddForce
                ((enemy.transform.position - transform.position  + enemy.transform.up).normalized  * 100 * Random.Range(0.5f,3f));
        }
    }
    private void OnDrawGizmos() 
    {
        if (enableGizmo)
            Gizmos.DrawWireSphere(transform.position, weapon.attackRange);    
    }
}
