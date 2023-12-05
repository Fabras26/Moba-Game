using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;
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

    public bool casting;

    protected Vector3 position;
    protected RaycastHit hit;
    protected Ray ray;
    private Camera cam;
    [Header("Abilities Indicator")]
    public Canvas abilitiesCanvas;
    public Image abilitySkillshot1;
    public Image abilitySkillshot2;
    public Image abilitySkillshot3;
    public Image abilitySkillshot4;
    //public SkillSet skills;
    void Start()
    {
        cam = Camera.main;
        ResetAllCooldown();
        OnStart();
    }

    private void Update()
    {
        AbilitiesInput();
        Cooldowns();
        CanvasSkillshot();
        OnUpdate();
    }
    
    public virtual void OnStart()
    {

    }
    public virtual void OnUpdate()
    {

    }
    void ResetAllCooldown()
    {
        abilitiesCanvas.enabled = false;
        abilitySkillshot1.enabled = false;
        abilitySkillshot2.enabled = false;
        abilitySkillshot3.enabled = false;
        abilitySkillshot4.enabled = false;

        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
        abilityImage3.fillAmount = 0;
        abilityImage4.fillAmount = 0;

        abilityText1.text = string.Empty;
        abilityText2.text = string.Empty;
        abilityText3.text = string.Empty;
        abilityText4.text = string.Empty;
    }
    public void CanvasSkillshot()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);

        OnAbilitySkillShot1();
        OnAbilitySkillShot2();
        OnAbilitySkillShot3();
        OnAbilitySkillShot4();

        if (abilitiesCanvas.enabled)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                position = hit.point;
            }
            Quaternion abCanvas = Quaternion.LookRotation(position - transform.position);
            abCanvas.eulerAngles = new Vector3(0, abCanvas.eulerAngles.y, abCanvas.eulerAngles.z);

            abilitiesCanvas.transform.rotation = Quaternion.Lerp(abCanvas, abilitiesCanvas.transform.rotation, 0);
        }
    }
    #region abilitiesInputs
    private void AbilitiesInput()
    {
        AbilityInput1();
        AbilityInput2();
        AbilityInput3();
        AbilityInput4();
    }
    public void AbilityInput(int number,KeyCode abilityKey, bool isAbilityCooldown, Image abilitySkillshot)
    {
        if (Input.GetKeyDown(abilityKey) && !isAbilityCooldown)
        {
            AbilityPressed(number);
        }
        if (abilitySkillshot.enabled)
        {
            if (Input.anyKeyDown) 
            {
                if (Input.GetKeyDown(abilityKey)) return;

                if (Input.GetMouseButton(1))
                {
                    CancelSkill(number, abilitySkillshot);
                }
                else
                {
                    AbilityReleased(number);
                }
            }
            if (Input.GetKeyUp(abilityKey))
            {
                AbilityReleased(number);
            }
        }
    }
    public virtual void CancelSkill(int number, Image abilitySkillshot)
    {
        abilitiesCanvas.enabled = false;
        abilitySkillshot.enabled = false;
    }
    private void AbilityInput1()
    {
        AbilityInput(1, abilityKey1, isAbilityCooldown1, abilitySkillshot1);

        /*if (Input.GetKeyDown(abilityKey1) && !isAbilityCooldown1)
        {
            AbilityPressed(1);
        }
        if (Input.GetKey(abilityKey1) && Input.GetMouseButtonDown(1)) abilitySkillshot1.enabled = false;
        if (Input.GetKeyUp(abilityKey1) && abilitySkillshot1.enabled)
        {
            isAbilityCooldown1 = true;
            currentAbilityCooldown1 = abilityCooldown1;
            AbilityReleased(1);
        }*/
    }
    private void AbilityInput2()
    {
        AbilityInput(2, abilityKey2, isAbilityCooldown2, abilitySkillshot2);
    }
    private void AbilityInput3()
    {
        AbilityInput(3, abilityKey3, isAbilityCooldown3, abilitySkillshot3);
    }
    private void AbilityInput4()
    {
        AbilityInput(4, abilityKey4, isAbilityCooldown4, abilitySkillshot4);
    }
    public virtual void AbilityPressed(int abilityIndex)
    {
        /*if (casting) return;
        casting = true;*/
        abilitiesCanvas.enabled = true;
        if (abilityIndex == 1)
        {
            OnAbilityPressed1();
        }
        else if (abilityIndex == 2)
        {
            OnAbilityPressed2();
        }
        else if (abilityIndex == 3)
        {
            OnAbilityPressed3();
        }
        else if (abilityIndex == 4)
        {
            OnAbilityPressed4();
        }
    }
    public virtual void AbilityReleased(int abilityIndex)
    {
        abilitiesCanvas.enabled = false;
        if (abilityIndex == 1)
        {
            OnAbilityReleased1();
        }
        else if (abilityIndex == 2)
        {
            OnAbilityReleased2();
        }
        else if (abilityIndex == 3)
        {
            OnAbilityReleased3();
        }
        else if (abilityIndex == 4)
        {
            OnAbilityReleased4();
        }
    }
    public virtual void OnAbilityPressed1() {
        abilitySkillshot1.enabled = true;

    }
    public virtual void OnAbilityPressed2() 
    {
        abilitySkillshot2.enabled = true;
    }
    public virtual void OnAbilityPressed3() { 
        abilitySkillshot3.enabled = true;
    }
    public virtual void OnAbilityPressed4() 
    {
        abilitySkillshot4.enabled = true;
    }

    public virtual void OnAbilityReleased1() 
    {
        abilitySkillshot1.enabled = false;
        isAbilityCooldown1 = true;
        currentAbilityCooldown1 = abilityCooldown1;
    }
    public virtual void OnAbilityReleased2() 
    { 
        abilitySkillshot2.enabled = false;
        isAbilityCooldown2 = true;
        currentAbilityCooldown2 = abilityCooldown2;
    }
    public virtual void OnAbilityReleased3() 
    {
        abilitySkillshot3.enabled = false;
        isAbilityCooldown3 = true;
        currentAbilityCooldown3 = abilityCooldown3;
    }
    public virtual void OnAbilityReleased4()
    {
        abilitySkillshot4.enabled = false;
        isAbilityCooldown4 = true;
        currentAbilityCooldown4 = abilityCooldown4;
    }

    public virtual void OnAbilitySkillShot1() { }
    public virtual void OnAbilitySkillShot2() { }
    public virtual void OnAbilitySkillShot3() { }
    public virtual void OnAbilitySkillShot4() { }
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
