using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;
[System.Serializable]
public class Skill
{

    [Header("Visual")]
    public Image imageHud;
    public TextMeshProUGUI cooldownText;
    public Image skillshot;

    [SerializeField]
    private KeyCode key;
    [Header("Properties")]
    [SerializeField]
    private float cooldown = 5;
    private float currentCooldown;
    private bool isCooldown = false;
    [SerializeField]
    private float damage = 200;
    [SerializeField]
    private float range = 7;

    public Color gizmosColor = Color.white;
    public float Cooldown { get => cooldown; set => cooldown = value; }
    public float CurrentCooldown { get => currentCooldown; set => currentCooldown = value; }
    public bool IsCooldown { get => isCooldown; set => isCooldown = value; }
    public float Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }
    public KeyCode Key { get => key; set => key = value; }

    public void ResetCooldown()
    {
        imageHud.fillAmount = 0;
        CurrentCooldown = 0;
        skillshot.enabled = false;
        cooldownText.text = string.Empty;
    }
    public void CheckCooldown()
    {
        if (IsCooldown)
        {
            CurrentCooldown -= Time.deltaTime;
            if (CurrentCooldown <= 0f)
            {
                IsCooldown = false;
                CurrentCooldown = 0f;

                if (imageHud != null)
                {
                    imageHud.fillAmount = 0;
                }
                if (cooldownText != null)
                {
                    cooldownText.text = "";
                }
            }
            else
            {
                if (imageHud != null)
                {
                    imageHud.fillAmount = CurrentCooldown / Cooldown;
                }

                if (cooldownText != null)
                {
                    cooldownText.text = Mathf.Ceil(CurrentCooldown).ToString();
                }
            }

        }
    }
    public void CancelSkill()
    {
        Cursor.visible = true;
        if (skillshot != null) skillshot.enabled = false;
    }
    public void SkillInput()
    {
        Cursor.visible = false;
        if(skillshot != null)skillshot.enabled = true;
    }
    public void SkillReleased()
    {
        Cursor.visible = true;
        if (skillshot != null) skillshot.enabled = false;
        IsCooldown = true;
        CurrentCooldown = Cooldown;
    }
}

public class Abilities : MonoBehaviour
{
    HighlightManager highlight;

    private Animator anim;
    public List<Skill> skills;
    private bool casting;

    protected Vector3 position;
    protected RaycastHit hit;
    protected Ray ray;
    protected Movement moveScript;
    private Camera cam;
    [Header("Abilities Indicator")]
    public Canvas abilitiesCanvas;
    public LayerMask ground;

    protected Vector3 mousePosition;
    void Start()
    {
        casting = false;
        highlight = GetComponent<HighlightManager>();

        moveScript = GetComponent<Movement>();
        anim = GetComponent<Animator>();
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

        foreach (Skill skill in skills)
        {
            skill.ResetCooldown();
        }
    }
    public void CanvasSkillshot()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        for (int i = 0;  i < skills.Count; i++)
        {
            if (skills[i] != null)
            {
                OnAbilitySkillShot(i);
            }
        }

        if (abilitiesCanvas.enabled)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                mousePosition = hit.point;
            }
            Quaternion abCanvas = Quaternion.LookRotation(mousePosition - transform.position);
            abCanvas.eulerAngles = new Vector3(0, abCanvas.eulerAngles.y, abCanvas.eulerAngles.z);

            abilitiesCanvas.transform.rotation = Quaternion.Lerp(abCanvas, abilitiesCanvas.transform.rotation, 0);
        }
    }
    #region abilitiesInputs
    public virtual void OnAbilitySkillShot(int index) { }

    private void AbilitiesInput()
    {
        foreach(Skill skill in skills)
        {
            AbilityInput(skill);
        }
    }
    public void AbilityInput(Skill skill)
    {
        if (casting) return;
        if (Input.GetKeyDown(skill.Key) && !skill.IsCooldown)
        {
            AbilityPressed(skill);
        }
        if (skill.skillshot.enabled)
        {
            if (Input.anyKeyDown) 
            {
                if (Input.GetKeyDown(skill.Key)) return;

                if (Input.GetMouseButton(1))
                {
                    CancelSkill(skill);
                }
                else
                {
                    AbilityReleased(skill);
                }
            }
            if (Input.GetKeyUp(skill.Key))
            {
                AbilityReleased(skill);
            }
        }
    }
    public virtual void CancelSkill(Skill skill)
    {
        skill.CancelSkill();
        highlight.enabled = true;

        abilitiesCanvas.enabled = false;
    }
    public virtual void AbilityPressed(Skill skill)
    {
        skill.SkillInput();
        highlight.UnableHighlight();
        highlight.enabled = false;

        abilitiesCanvas.enabled = true;
    }
    public virtual void AbilityReleased(Skill skill, bool stop = true)
    {
        if(stop) moveScript.Stop(mousePosition);


        casting = true;
        highlight.enabled = true;
        anim.SetFloat("SkillIndex", skills.IndexOf(skill) + 1);
        anim.SetTrigger("Skill");
        abilitiesCanvas.enabled = false;
        skill.SkillReleased();
        moveScript.enabled = false;
    }

    public void UseSkill(int number)
    {
        casting = false;
        switch (number)
        {
            case 1: Skill1(); break;
            case 2: Skill2(); break;
            case 3: Skill3(); break;
            case 4: Skill4(); break;
        }
    }
    public virtual void Skill1() { }
    public virtual void Skill2() { }
    public virtual void Skill3() { }
    public virtual void Skill4() { }

    #endregion


    private void Cooldowns()
    {
        foreach(Skill skill in skills)
        {
            skill.CheckCooldown();
        }
    }
    public void FinishSkillAnimation()
    {
        moveScript.enabled = true;
        moveScript.Walk();
    }

    private void OnDrawGizmos()
    {
        foreach (Skill skill in skills)
        {
            Gizmos.color = skill.gizmosColor;
            Gizmos.DrawWireSphere(transform.position + new Vector3(0, 1, 0), skill.Range);
        }
    }
}
