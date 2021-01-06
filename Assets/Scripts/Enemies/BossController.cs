using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : Enemy
{
    #region Boss Variables
    //[SerializeField]
    //[Tooltip("The type of boss.")]
    //private Enemy bossEnemy;

    [SerializeField]
    [Tooltip("The information for this boss' attacks.")]
    private EnemyAttackInfo[] attackInfo;

    [SerializeField]
    [Tooltip("The amount of time to wait before attacking (must be less than Frozen Time).")]
    private float attackWaitTime;

    [SerializeField]
    [Tooltip("The amount of time this boss is frozen to attack (must be longer than the attack animation).")]
    private float frozenTime;

    //[SerializeField]
    //[Tooltip("The cooldown time of this attack.")]
    //private float cooldownTime;

    //[SerializeField]
    //[Tooltip("The range of this boss' attack.")]
    //private float attackRange;

    //[SerializeField]
    //[Tooltip("Where to spawn the attack with respect to the boss.")]
    //private Vector3 attackOffset;

    [SerializeField]
    [Tooltip("The number of times this boss can attack in one position.")]
    private int attacksPerPosition;

    [SerializeField]
    [Tooltip("The positions this boss can move to.")]
    private Vector3[] movePositions;

    [SerializeField]
    [Tooltip("The health bar of this boss.")]
    private Slider HPBar;

    [SerializeField]
    [Tooltip("The bounds of this boss.")]
    private Vector3[] bounds;

    private float attackWaitTimer;

    private float frozenTimer;

    //private float cooldownTimer;

    //private Rigidbody bossRigidbody;

    //private Enemy bossEnemy;

    //private float attackTime;

    //private bool isAttacking = false;

    //private int numAttackSoFar = 0;

    private bool hasAttacked = false;

    //private ParticleSystem attackEffect;

    private int moveIndex = 0;

    private double healthThreshold = 0.75;

    private EnemyAttackInfo currAttack;

    [SerializeField]
    [Tooltip("Overall Score")]
    private OverallScore overallScore;
    #endregion

    #region Unity Functions
    // Initialize the boss and relevant variables
    public override void Awake()
    {
        base.Awake();
        //GetComponent<Renderer>().material.color = Color.white;
        //bossRigidbody = GetComponent<Rigidbody>();
        //bossEnemy = GetComponent<Enemy>();
        frozenTimer = frozenTime;
        attackWaitTimer = attackWaitTime;
        //cooldownTimer = cooldownTime;
        //attackTime = 0.6f;
        HPBar.value = currHealth / totalHealth;
        currAttack = attackInfo[0];
        //boss = Instantiate<Enemy>(bossEnemy, movePositions[i], Quaternion.identity);
        //attackEffect = GetComponentInChildren<ParticleSystem>();
        //attackEffect.Stop();
    }

    //private void Start()
    //{
    //    moveIndex = 0;
    //}

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Boss Update");
        Debug.Log("Boss direction: " + transform.forward);
        if (frozenTimer > 0)
        {
            frozenTimer -= Time.deltaTime;
            if (!hasAttacked)
            {
                attackWaitTimer -= Time.deltaTime;
            }
        } else
        {
            frozenTimer = 0;
        }

        if (attackWaitTimer <= 0)
        {
            StartCoroutine(Attack());
            hasAttacked = true;
            //isAttacking = true;
            //numAttackSoFar++;
            attackWaitTimer = attackWaitTime;
        }

        //if (isAttacking)
        //{
        //    attackTime -= Time.deltaTime;
        //    if (attackTime <= 0)
        //    {
        //        //GetComponent<Renderer>().material.color = Color.white;
        //        isAttacking = false;
        //        attackTime = 0.6f;
        //    }
        //}

        TriggerEvent();

        Move();
    }
    #endregion

    #region Movement Function
    private void Move()
    {        
        // Movement using transform
        if (transform.position == movePositions[moveIndex])
        {
            frozenTimer = frozenTime;
            //attackWaitTimer = attackWaitTime;
            hasAttacked = false;
            //numAttackSoFar = 0;
            moveIndex++;
            if (moveIndex >= movePositions.Length)
            {
                moveIndex = 0;
            }
        }

        if (frozenTimer <= 0)
        {
            Vector3 moveTo = movePositions[moveIndex];
            transform.position = Vector3.MoveTowards(transform.position, moveTo, moveSpeed * Time.deltaTime);
        }

        // Movement using rigidbody
        //Vector3 movementVector = (moveTo - bossRigidbody.position).normalized;
        //bossRigidbody.MovePosition(bossRigidbody.position + movementVector * Time.deltaTime * moveSpeed);
    }
    #endregion

    #region Attack Functions
    private IEnumerator Attack()
    {
        for (int i = 0; i < attacksPerPosition; i++)
        {
            int index = i % attackInfo.Length;
            GameObject attackGO = Instantiate(attackInfo[index].AttackGO, transform.position + attackInfo[index].AttackOffset, transform.rotation);
            Attack attack = attackGO.GetComponent<Attack>();
            attack.UseAttack(transform.position + attackInfo[index].AttackOffset, "Player");
            //Debug.Log("Attack Number: " + i);
            yield return new WaitForSeconds(attack.CooldownTime);
        }
        //attacked = true;
    }

    //private void UseAttack()
    //{
    //    //GetComponent<Renderer>().material.color = Color.black;
    //    //Debug.Log("This boss is performing an attack now.");
    //    RaycastHit hit;
    //    //attackEffect.Play();
    //    if (Physics.SphereCast(transform.position + attackOffset, 0.5f, Vector3.left, out hit, attackRange))
    //    {
    //        if (hit.collider.CompareTag("Player"))
    //        {
    //            hit.collider.GetComponent<Player>().TakeDamage(damageDealt);
    //        }
    //    }
    //}
    #endregion

    #region Trigger Event
    private void TriggerEvent()
    {
        if (HPBar.value <= healthThreshold)
        {
            attacksPerPosition++;
            healthThreshold -= 0.25;
            moveSpeed += 3;
        }
    }
    #endregion

    #region Health Functions
    public override void Heal(float amount)
    {
        currHealth += amount;
        if (currHealth >= totalHealth)
        {
            currHealth = totalHealth;
        }
        HPBar.value = currHealth / totalHealth;
        //Debug.Log("Current health of boss: " + currHealth);
    }

    public override void TakeDamage(float amount)
    {
        currHealth -= amount;
        HPBar.value = currHealth / totalHealth;
        //Debug.Log("Current health of boss: " + currHealth);
        if (currHealth <= 0)
        {
            Purify();
            GameObject levelMusic = GameObject.Find("Background Music");
            if (levelMusic != null)
            {
                levelMusic.SetActive(false);
            }
            overallScore.battleStarted = false;
            overallScore.finalResult = overallScore.timerText.text;
            Debug.Log("Time: " + overallScore.timeAlive);
            SceneManager.LoadScene("Aftermath");
        }
    }
    #endregion
}
