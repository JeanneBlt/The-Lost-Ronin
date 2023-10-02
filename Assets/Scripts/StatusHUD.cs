using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusHUD : MonoBehaviour
{
    public Image statusHPBar;
    public Text statusHPValue;


    public void SetStatusHUD(CharacterStatus status)
    {
        float currentHealth = status.health * (100 / status.maxHealth);
        float currentMana = status.mana * (100 / status.maxMana);

        statusHPBar.fillAmount = currentHealth / 100;
        statusHPValue.text = status.health + "/" + status.maxHealth;
    }

    public void SetHP(CharacterStatus status, float hp)
    {
        StartCoroutine(GraduallySetStatusBar(status, hp, false, 10, 0.05f));
    }

    IEnumerator GraduallySetStatusBar(CharacterStatus status, float amount, bool isIncrease, int fillTimes, float fillDelay)
    {
        float percentage = 1 / (float)fillTimes;

        if (isIncrease)
        {
            for (int fillStep = 0; fillStep < fillTimes; fillStep++)
            {
                float _fAmount = amount * percentage;
                float _dAmount = _fAmount / status.maxHealth;
                status.health += _fAmount;
                statusHPBar.fillAmount += _dAmount;
                if (status.health <= status.maxHealth)
                    statusHPValue.text = status.health + "/" + status.maxHealth;
                yield return new WaitForSeconds(fillDelay);
            }
        }
        else
        {
            for (int fillStep = 0; fillStep < fillTimes; fillStep++)
            {
                float _fAmount = amount * percentage;
                float _dAmount = _fAmount / status.maxHealth;
                status.health -= _fAmount;
                statusHPBar.fillAmount -= _dAmount;
                if (status.health >= 0)
                    statusHPValue.text = status.health + "/" + status.maxHealth;

                yield return new WaitForSeconds(fillDelay);
            }
        }

    }
}
