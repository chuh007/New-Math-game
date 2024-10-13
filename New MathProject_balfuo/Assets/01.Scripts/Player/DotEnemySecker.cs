using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotEnemySecker : MonoBehaviour
{
    [SerializeField]private List<Enemy> _enemys;
    private PlayerAim playerAim;
    private Transform _weaponHolder;
    private float viewAngle;
    private float viewRaidus;
    public LayerMask WhatIsEnemy;
    private void Awake()
    {
        playerAim = GetComponent<PlayerAim>();
        _weaponHolder = playerAim._weaponHolder;
        viewAngle = playerAim.viewAngle;
        viewRaidus = playerAim.viewRaidus;
    }
    private void Update()
    {
        Collider2D[] findEnemys = Physics2D.OverlapCircleAll(transform.position, viewRaidus, WhatIsEnemy);
        Vector2 rightVector = _weaponHolder.right.normalized;
        foreach (Collider2D c in findEnemys)
        {
            Vector2 toTarget = (c.transform.position - transform.position).normalized;
            float dot = Vector2.Dot(rightVector, toTarget);
            float angle = Mathf.Acos(dot)*Mathf.Rad2Deg;
            if(angle < viewAngle / 2 && Vector2.Distance(transform.position,c.transform.position)<=viewRaidus)
            {
                c.gameObject.GetComponent<EnemyMove>().StopMove();
            }
            else
            {
                c.gameObject.GetComponent<EnemyMove>().StartMove();
            }
        }
    }
}
