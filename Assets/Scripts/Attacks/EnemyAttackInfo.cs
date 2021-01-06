using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyAttackInfo 
{
    #region Attack Variables
    [SerializeField]
    [Tooltip("The name of this attack.")]
    protected string name;
    public float Name
    {
        get
        {
            return Name;
        }
    }

    [SerializeField]
    [Tooltip("The prefab of this attack.")]
    private GameObject attackGO;
    public GameObject AttackGO
    {
        get
        {
            return attackGO;
        }
    }

    [SerializeField]
    [Tooltip("Where to spawn this attack with respect to this enemy.")]
    private Vector3 attackOffset;
    public Vector3 AttackOffset
    {
        get
        {
            return attackOffset;
        }
    }
    #endregion
}
