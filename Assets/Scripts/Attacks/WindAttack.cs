using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WindAttack : Attack
{
    private ParticleSystem attackEffect;

    private void Awake()
    {
        attackEffect = GetComponent<ParticleSystem>();
    }

    public override void UseAttack(Vector3 spawnPos, string target)
    {
        RaycastHit hit;
        attackEffect.Play();
        if (Physics.SphereCast(spawnPos, 0.5f, transform.right, out hit, attackRange))
        {
            if (hit.collider.CompareTag(target))
            {
                switch (target)
                {
                    case "Player":
                        hit.collider.GetComponent<Player>().TakeDamage(damage);
                        break;
                    case "Enemy":
                        hit.collider.GetComponent<Enemy>().TakeDamage(damage);
                        break;
                }
            }
        }
    }
}
