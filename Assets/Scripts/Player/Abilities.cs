using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [Header("Peck Ability")]
    public Image peckingAbilityIcon;
    public float peckCooldown = 0.5f;
    bool isCooldown = false;
    public KeyCode peckAbility;

    [Header("Dash Ability")]
    public Image dashingAbilityIcon;
    public float dashCooldown = 2f;
    bool isCooldown2 = false;
    public KeyCode dashAbility;

    public PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        peckingAbilityIcon.fillAmount = 0;
        dashingAbilityIcon.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currMana < player.peckManaCost)
        {
            peckingAbilityIcon.fillAmount = 1;
        }

        if (player.currMana < player.dashManaCost)
        {
            dashingAbilityIcon.fillAmount = 1;
        }
        peckingAbility();
        dashingAbility();
    }

    void peckingAbility()
    {
        if (Input.GetKeyDown(peckAbility) && isCooldown == false)
        {
            isCooldown = true;
            peckingAbilityIcon.fillAmount = 1;
        }

        if (isCooldown && player.currMana >= player.peckManaCost)
        {
            peckingAbilityIcon.fillAmount -= 1 / peckCooldown * Time.deltaTime;

            if (peckingAbilityIcon.fillAmount <= 0)
            {
                peckingAbilityIcon.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    void dashingAbility()
    {
        if (Input.GetKeyDown(dashAbility) && isCooldown2 == false)
        {
            isCooldown2 = true;
            dashingAbilityIcon.fillAmount = 1;
        }

        if (isCooldown2 && player.currMana >= player.dashManaCost)
        {
            dashingAbilityIcon.fillAmount -= 1 / dashCooldown * Time.deltaTime;

            if (dashingAbilityIcon.fillAmount <= 0)
            {
                dashingAbilityIcon.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }
}
