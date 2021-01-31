using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] Stats target;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform feetPoint;
    Rigidbody rbody;
    float timeSinceLastAttack;
    private void Start() 
    {  
        timeSinceLastAttack = weapon.attackSpeed;  
        rbody = GetComponent<Rigidbody>();  
    }
    private void Update() 
    {
        timeSinceLastAttack += Time.deltaTime;
        if (Vector3.Distance(transform.position, target.transform.position) <= weapon.attackRange)
        {
            Attack();
        }   
        else
        {
            Move();
        }
    }
    public void Attack()
    {
        if (timeSinceLastAttack >= weapon.attackSpeed)
        {
            timeSinceLastAttack = 0;
            target.TakeDamage(weapon.damage);
        }
    }
    public void Move()
    {
        if (!IsGrounded()) return;
        Vector3 newPosition = Vector3.MoveTowards
            (transform.position, target.transform.position, Time.deltaTime * moveSpeed);
        rbody.MovePosition(newPosition);
    }

    public bool IsGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(feetPoint.position, -transform.up, out hit))
        {
            if (Vector3.Distance(hit.point, feetPoint.position) < 0.1f 
                && hit.transform.tag == GameTags.Floor.ToString())
            {
                return true;
            }            
        }
        return false;
    }
    
    private void OnDrawGizmos() 
    {
        Ray ray = new Ray (feetPoint.position, -transform.up);
        Gizmos.DrawRay(ray);
    }
}
