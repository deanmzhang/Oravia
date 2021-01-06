using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region healthVariables
    public float totalHealth;
    private float currHealth;
    private bool corrupted;
    public Slider HPBar;
    public float healthRegenerationRate;
    #endregion

    #region unityFunctions
    private void Awake()
    {
        corrupted = false;
        currHealth = totalHealth;

        HPBar.value = currHealth / totalHealth;
    }

    private void Update()
    {
        if (currHealth + healthRegenerationRate*Time.deltaTime <= totalHealth)
        {
            currHealth += healthRegenerationRate * Time.deltaTime;
        }
        HPBar.value = currHealth / totalHealth;
    }
    #endregion

    #region healthFunctions
    public void Heal(float amount)
    {
        // Increase the player's health
        currHealth += amount;
        // Make sure currHealth doesn't exceed totalHealth
        currHealth = Mathf.Min(currHealth, totalHealth);
        Debug.Log("Player's health is now " + currHealth.ToString());
        HPBar.value = currHealth / totalHealth;
    }

    public void TakeDamage(float amount)
    {
        // Decrease the player's health
        currHealth -= amount;
        Debug.Log("Player's health is now " + currHealth.ToString());

        // Change UI
        HPBar.value = currHealth / totalHealth;
        
        // Check if player will die
        if (currHealth <= 0)
        {
            Die();
        }
    }

    // Destroys bird object. Maybe trigger end scene later on??
    private void Die()
    {
        corrupted = true;
        Destroy(this.gameObject);

        GameObject levelMusic = GameObject.Find("Background Music");
        if (levelMusic != null)
        {
            levelMusic.SetActive(false);
        }

        SceneManager.LoadScene("DeathScene");

        // Maybe implement something later on here to end the game (Proj1 used a GameManager to end the game).
    }
    #endregion
}
