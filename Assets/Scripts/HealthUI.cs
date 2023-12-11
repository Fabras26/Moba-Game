using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider slider;
    public Slider healthHud;
    private float maxHealth;
    private float currentHealth;
    private float targetHealth;

    void Start()
    {
        slider = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    public void SetValues(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = newMaxHealth;
        targetHealth = newMaxHealth;
    }
    public void SetTargetHealth(float newMaxHealth, float newTargetHealth)
    {
        maxHealth = newMaxHealth;
        targetHealth = newTargetHealth;
        StopAllCoroutines();
        StartCoroutine(HealthBarCoroutine());
    }
    IEnumerator HealthBarCoroutine()
    {
        while (!Mathf.Approximately(currentHealth, targetHealth))
        {
            yield return new WaitForFixedUpdate();
            currentHealth = Mathf.Lerp(currentHealth, targetHealth, 5f * Time.deltaTime);
            slider.value = currentHealth / maxHealth;
            if (healthHud != null) healthHud.value = currentHealth / maxHealth;
        }
        currentHealth = targetHealth;
        slider.value = currentHealth / maxHealth;
    }
}
