using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    //public static BarManager instance {  get; private set; }
    public Image HealthBar;
    private float CurrentHealth;
    private float MaxHealth;
    public Image EnergyBar;
    private float CurrentEnergy = 0;
    private float MaxEnergy = 10;
    public Button UltButton;
    public Button DodgeButton;

    Controller controller;

    void Start()
    {
        UltButton.gameObject.SetActive(false);
        EnergyBar.fillAmount = 0;
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
        MaxHealth = controller.GetHealth();
        CurrentHealth = controller.GetHealth();
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
            UltButton.gameObject.SetActive(true);
            
        }

        EnergyBar.fillAmount = CurrentEnergy / MaxEnergy;
        //Debug.Log("POWERRRRRR" + energy);
    }

    public void resetEnergy()
    {
        CurrentEnergy = 0;
        EnergyBar.fillAmount = 0;
        UltButton.gameObject.SetActive(false);
    }

    public void disableDodgeButton()
    {
        DodgeButton.gameObject.SetActive(false);
    }
}
