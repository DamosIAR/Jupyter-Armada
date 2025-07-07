using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    //public static BarManager instance {  get; private set; }
    private float CurrentHealth;
    private float MaxHealth;
    private float resetSpecialSkill;
    private float CurrentEnergy = 0;
    private float MaxEnergy = 10;
    private int SkillCD;
    private float currentSkillTime;

    private float timeNormalized;

    public Image HealthBar;
    public Image EnergyBar;
    public Image SkillBar;
    public Button UltButton;
    public Button SpecialBullet;
    public float SpecialSkillCD;

    Controller controller;

    void Start()
    {
        currentSkillTime = SpecialSkillCD;
        EnergyBar.gameObject.SetActive(true);
        UltButton.gameObject.SetActive(false);
        SpecialBullet.gameObject.SetActive(true);
        //SkillBar.gameObject.SetActive(false);
        EnergyBar.fillAmount = 0;
        SkillBar.fillAmount = 0;
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        MaxHealth = controller.GetHealth();
        CurrentHealth = controller.GetHealth();
    }

    private void Update()
    {
        if(SkillCD == 1)
        {
            if (SkillBar.fillAmount > 0)
            {
                SpecialBullet.enabled = false;
                currentSkillTime -= Time.deltaTime;
                //Debug.Log(currentSkillTime);
                timeNormalized = (currentSkillTime / SpecialSkillCD);
                Debug.Log(timeNormalized);
                SkillBar.fillAmount = timeNormalized;

            }
            else
            {
                SpecialBullet.enabled = true;
                SkillCD = 0;
                currentSkillTime = SpecialSkillCD;

            }
        }
    }

    public void takeDamage(float damage)
    {
        CurrentHealth -= damage;
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
    }

    public void heal(float healAmount)
    {
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        HealthBar.fillAmount = CurrentHealth / MaxHealth;
    }

    public void addEnergy(float energy)
    {
        CurrentEnergy += energy;
        CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, MaxEnergy);

        if(CurrentEnergy >= MaxEnergy)
        {
            EnergyBar.gameObject.SetActive(false );
            UltButton.gameObject.SetActive(true);
            
        }

        EnergyBar.fillAmount = CurrentEnergy / MaxEnergy;
    }

    public void resetEnergy()
    {
        CurrentEnergy = 0;
        EnergyBar.fillAmount = 0;
        EnergyBar.gameObject.SetActive(true);
        UltButton.gameObject.SetActive(false);
    }



    public void resetSkill()
    {
        SkillBar.fillAmount = 1;

    }

    public void SpecialSkillOnCooldown()
    {
        SkillBar.fillAmount = 1;
        SkillCD = 1;
    }

    public void disableDodgeButton()
    {
        SpecialBullet.gameObject.SetActive(false);
    }
}
