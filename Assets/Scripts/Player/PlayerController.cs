using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region playerVariables
    // the player for us to work with
    Player birdPlayer;
    Rigidbody birdBody;
    RigidbodyConstraints originalConstraints;
    public int numFeathers;
    #endregion

    #region movementVariables
    public float moveSpeed;

    float xInput;
    float yInput;
    public float maxHeight;
    [SerializeField]
    private float minHeight;
    [SerializeField]
    private float leftBorder;
    [SerializeField]
    private float rightBorder;
    #endregion

    #region attackVariables
    public float Damage;
    public float attackRange;
    private bool isAttacking;
    private float peckAttackTimer;
    private float dashAttackTimer;

    // The following booleans could replace isAttacking later
    private bool isPeckReady;
    private bool isDashReady;
    #endregion

    #region manaVariables
    public float totalMana;
    public float currMana;
    public Slider ManaBar;
    public float peckCooldown;
    public float dashCooldown;
    public float peckManaCost;
    public float dashManaCost;
    public float manaRegenerationRate;
    #endregion

    // Uncomment this section when animations are ready
    #region Animations
    Animator pecking;
    //Animator dashing;
    #endregion

    #region unityFunctions
    // Initialize the bird and relevant variables
    private void Awake()
    {
        birdPlayer = GetComponent<Player>();
        birdBody = GetComponent<Rigidbody>();
        originalConstraints = birdBody.constraints;
        isAttacking = false;
        peckAttackTimer = 0;
        dashAttackTimer = 0;

        currMana = totalMana;
        ManaBar.value = currMana / totalMana;

        
        pecking = GetComponent<Animator>();
        //dashing = GetComponent<Animator>();
        
    }

    private void Update()
    {
        //Debug.Log("Direction of player: " + transform.forward);
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        if (birdBody.position.y >= maxHeight && yInput > 0
            || birdBody.position.y <= minHeight && yInput < 0)
        {
            yInput = 0f;
        } 
        if (birdBody.position.x <= leftBorder && xInput < 0
            || birdBody.position.x >= rightBorder && xInput > 0)
        {
            xInput = 0f;
        }

        Move();

        currMana += manaRegenerationRate * Time.deltaTime;
        ManaBar.value = currMana / totalMana;

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking && currMana >= peckManaCost && peckAttackTimer <= 0)
        {
            peckAttackTimer = peckCooldown;
            StartCoroutine(PeckAttack());
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && !isAttacking && currMana >= dashManaCost && dashAttackTimer <= 0)
        {
            dashAttackTimer = dashCooldown;
            StartCoroutine(DashAttack());
        }
        /*
        if (Input.GetKeyDown(KeyCode.F))
        {
            chooseAttack();
        }
        */
        else if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            SceneManager.LoadScene("MainMenu");
        } else
        {
            peckAttackTimer -= Time.deltaTime;
            dashAttackTimer -= Time.deltaTime;
        }
    }


    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            if (isAttacking)
            {
                //Debug.Log(col.transform.name);
                if (col.transform.CompareTag("Enemy"))
                {
                    Debug.Log("Attacking BasicEnemy");
                    col.transform.GetComponent<Enemy>().TakeDamage(Damage);
                }
            }
        }
        isAttacking = false;
    }


    /*
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            birdBody.isKinematic = true;
            birdBody.constraints = RigidbodyConstraints.FreezeAll;
            float timeInvincible = 2.5f;
            Invoke("InvincibleFrames", timeInvincible);
        }
    }

    private void InvincibleFrames()
    {
        birdBody.isKinematic = false;
        birdBody.constraints = originalConstraints;
    }
    */

    #endregion

    #region movementFunctions
    private void Move()
    {
        pecking.SetBool("Moving", true);

        if (xInput != 0 || yInput != 0)
        {
            if (xInput > 0)
            {
                Quaternion flip = transform.localRotation;
                flip.eulerAngles = new Vector3(0.0f, -90f, 0.0f);
                transform.localRotation = flip;
            }
            else if (xInput < 0)
            {
                Quaternion flip = transform.localRotation;
                flip.eulerAngles = new Vector3(0.0f, 90f, 0.0f);
                transform.localRotation = flip;
            }
            Vector3 movement_vector = new Vector3(xInput, yInput);
            movement_vector = movement_vector.normalized;
            birdBody.MovePosition(birdBody.position + movement_vector * Time.deltaTime * moveSpeed);
        } else
        {
            pecking.SetBool("Moving", false);
            Vector3 movement_vector = new Vector3(xInput, yInput);
            movement_vector = movement_vector.normalized;
            birdBody.MovePosition(birdBody.position + movement_vector * Time.deltaTime * moveSpeed);
        }
        

        
    }
    #endregion

    #region attackFunctions

    IEnumerator PeckAttack()
    {
        isAttacking = true;

        // Play pecking animation
        pecking.SetTrigger("Pecking");

        currMana -= peckManaCost;
        ManaBar.value = currMana / totalMana;

        if (currMana <= 0)
        {
            isAttacking = false;
        }

        yield return new WaitForSeconds(peckCooldown);
        Debug.Log("Peck Attack Cooldown: " + peckCooldown.ToString());

        

        isAttacking = false;
    }
    
    IEnumerator DashAttack()
    {

        if (currMana <= dashManaCost)
        {
            isAttacking = false;
            //yield return new WaitForSeconds(dashCooldown);
            yield return null;
        }

        isAttacking = true;

        // Play dashing animation
        // dashing.SetTrigger("Dashing trigger");
        float dashDistance = 5f;

        Vector3 movement_vector = new Vector3(xInput, yInput);
        movement_vector = movement_vector.normalized;
        birdBody.AddForce(movement_vector * dashDistance * moveSpeed, ForceMode.Impulse);

        currMana -= dashManaCost;
        ManaBar.value = currMana / totalMana;

        yield return new WaitForSeconds(dashCooldown);
        Debug.Log("Dash Attack Cooldown: " + dashCooldown.ToString());

        isAttacking = false;
        yield return null;
    }
    
    /*
    // chooseAttack might have parameters to set which elemental attack the bird will have equipped
    private void chooseAttack()
    {
        Debug.Log("Switching mode of attack!");
    }
    */
    #endregion

}
