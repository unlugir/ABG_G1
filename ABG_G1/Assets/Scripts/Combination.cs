using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combination", menuName = "ABG/Combination", order = 0)]
public class Combination : ScriptableObject {
    [SerializeField] public List<InputPatterns> patterns;
    [SerializeField] public float damageK;
    [SerializeField] ParticleSystem effect;

    public void Cast(Weapon weapon, Vector3 position)
    {
        if (effect != null)
        {
            var createdEffect =  GameObject.Instantiate(effect, position, Quaternion.identity);
            Destroy(createdEffect.gameObject, effect.main.duration);
        }
        var attackedEnemies = Physics.OverlapSphere
            (position, weapon.attackRange);
        foreach(var enemy in attackedEnemies)
        {
            if (enemy.tag != GameTags.Enemy.ToString()) continue;
            Stats enemyStats = enemy.GetComponent<Stats>();
            if (!enemyStats.isAlive) continue;
            enemyStats.TakeDamage(weapon.damage * damageK);
            enemy.GetComponent<Rigidbody>().AddForce
                ((enemy.transform.position - position  + enemy.transform.up).normalized  * 100 * Random.Range(0.5f,3f));
        }
    }
}
