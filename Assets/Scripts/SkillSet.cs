using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSet : MonoBehaviour
{
    public bool casting;
    public virtual void AbilityPressed(int abilityIndex)
    {
        if (casting) return;
        casting = true;
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
        else if(abilityIndex == 4)
        {
            OnAbilityPressed4();
        }
    }
    public virtual void AbilityRealesed(int abilityIndex)
    {
        if (casting) return;
        casting = true;
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
    public virtual void OnAbilityPressed1() {}
    public virtual void OnAbilityPressed2() { }
    public virtual void OnAbilityPressed3() { }
    public virtual void OnAbilityPressed4() { }

    public virtual void OnAbilityReleased1() { }
    public virtual void OnAbilityReleased2() { }
    public virtual void OnAbilityReleased3() { }
    public virtual void OnAbilityReleased4() { }
}
