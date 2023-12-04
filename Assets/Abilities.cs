using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Abilities : MonoBehaviour
{
    [Header("Ability 1")]
    public Image abilityImage1;
    public TextMeshProUGUI abilityText1;
    [SerializeField]
    private KeyCode abilityKey1;
    [SerializeField]
    private float abilityCooldown1 = 5;

    [Header("Ability 2")]
    public Image abilityImage2;
    public TextMeshProUGUI abilityText2;
    [SerializeField]
    private KeyCode abilityKey2;
    [SerializeField]
    private float abilityCooldown2 = 7;

    [Header("Ability 3")]
    public Image abilityImage3;
    public TextMeshProUGUI abilityText3;
    [SerializeField]
    private KeyCode abilityKey3;
    [SerializeField]
    private float abilityCooldown3 = 5;

    [Header("Ability 4")]
    public Image abilityImage4;
    public TextMeshProUGUI abilityText4;
    [SerializeField]
    private KeyCode abilityKey4;
    [SerializeField]
    private float abilityCooldown4 = 30;

    public bool isAbilityCooldown1 =false;
    public bool isAbilityCooldown2 =false;
    public bool isAbilityCooldown3 =false;
    public bool isAbilityCooldown4 =false;

    public float currentAbilityCooldown1;
    public float currentAbilityCooldown2;
    public float currentAbilityCooldown3;
    public float currentAbilityCooldown4;

    void Start()
    {
        ResetAllCooldown();
    }

    void Update()
    {
        AbilitiesInput();
        Cooldowns();
    }
   

    void ResetAllCooldown()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
        abilityImage4.fillAmount = 0;

        abilityText1.text = string.Empty;
        abilityText2.text = string.Empty;
        abilityText3.text = string.Empty;
        abilityText4.text = string.Empty;
    }
    #region abilitiesInputs
    private void AbilitiesInput()
    {
        AbilityInput1();
        AbilityInput2();
        AbilityInput3();
        AbilityInput4();
    }
    private void AbilityInput1()
    {
        if (Input.GetKeyDown(abilityKey1) && !isAbilityCooldown1)
        {
            isAbilityCooldown1 = true;
            currentAbilityCooldown1 = abilityCooldown1;
        }
    }
    private void AbilityInput2()
    {
        if (Input.GetKeyDown(abilityKey2) && !isAbilityCooldown2)
        {
            isAbilityCooldown2 = true;
            currentAbilityCooldown2 = abilityCooldown2;
        }
    }
    private void AbilityInput3()
    {
        if (Input.GetKeyDown(abilityKey3) && !isAbilityCooldown3)
        {
            isAbilityCooldown3 = true;
            currentAbilityCooldown3 = abilityCooldown3;
        }
    }
    private void AbilityInput4()
    {
        if (Input.GetKeyDown(abilityKey4) && !isAbilityCooldown4)
        {
            isAbilityCooldown4 = true;
            currentAbilityCooldown4 = abilityCooldown4;
        }
    }
    #endregion


    private void Cooldowns()
    {
        AbilityCooldown(ref currentAbilityCooldown1, abilityCooldown1, ref isAbilityCooldown1, abilityImage1, abilityText1);
        AbilityCooldown(ref currentAbilityCooldown2, abilityCooldown2, ref isAbilityCooldown2, abilityImage2, abilityText2);
        AbilityCooldown(ref currentAbilityCooldown3, abilityCooldown3, ref isAbilityCooldown3, abilityImage3, abilityText3);
        AbilityCooldown(ref currentAbilityCooldown4, abilityCooldown4, ref isAbilityCooldown4, abilityImage4, abilityText4);


    }
    private void AbilityCooldown(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image skillImage, TextMeshProUGUI skillText)
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;
            if(currentCooldown <= 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;

                if(skillImage != null)
                {
                    skillImage.fillAmount = 0;
                }
                if(skillText != null)
                {
                    skillText.text = "";
                }
            }
            else 
            {
                if (skillImage != null)
                {
                    skillImage.fillAmount = currentCooldown / maxCooldown;
                }
                
                if (skillText != null)
                {
                    skillText.text = Mathf.Ceil(currentCooldown).ToString();
                }
            }

        }
    }
}
