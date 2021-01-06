using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    #region Attack Variables
    [SerializeField]
    [Tooltip("How much damage this attack deals.")]
    protected float damage;
    public float Damage
    {
        get
        {
            return damage;
        }
    }

    [SerializeField]
    [Tooltip("The range of this attack.")]
    protected float attackRange;
    public float AttackRange
    {
        get
        {
            return attackRange;
        }
    }

    [SerializeField]
    [Tooltip("The cooldown time of this attack.")]
    protected float cooldownTime;
    public float CooldownTime
    {
        get
        {
            return cooldownTime;
        }
    }

    //protected ParticleSystem attackEffect;
    #endregion

    #region Initialization
    //private void Awake()
    //{
    //   attackEffect = GetComponent<ParticleSystem>();
    //}
    #endregion

    #region Use Method
    public abstract void UseAttack(Vector3 spawnPos, string target);
    #endregion
}
