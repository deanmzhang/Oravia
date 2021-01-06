using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region Health Variables
    [SerializeField]
    [Tooltip("The total health of this enemy.")]
    protected float totalHealth;

    [SerializeField]
    [Tooltip("purify effect")]
    protected ParticleSystem purify;

    protected float currHealth;

    private bool corrupted;

    #endregion

    #region Attack Variables
    [SerializeField]
    [Tooltip("How much damage this enemy deals.")]
    protected float damageDealt;
    #endregion

    #region Movement Variables
    [SerializeField]
    [Tooltip("The move speed of this enemy.")]
    protected float moveSpeed;
    #endregion

    #region Unity Functions
    public virtual void Awake()
    {
        corrupted = true;
        currHealth = totalHealth;
    }
    #endregion

    #region Health Functions
    public virtual void Heal(float amount)
    {
        currHealth += amount;
        if (currHealth >= totalHealth)
        {
            currHealth = totalHealth;
        }
    }

    public virtual void TakeDamage(float amount)
    {
        currHealth -= amount;
        if (currHealth <= 0)
        {
            Purify();
        }
    }

    protected void Purify()
    {
        corrupted = false;

        // Need enemy to disappear when defeated
        Instantiate(purify, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    #endregion
}
