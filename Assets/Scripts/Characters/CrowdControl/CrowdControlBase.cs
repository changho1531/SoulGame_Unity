using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdControlBase : MonoBehaviour
{
    public virtual CrowdControl CCtype() { return CrowdControl.None; }

    public CharacterBase attacker;
    public CharacterBase victim;

    public float leftTime;
    public bool isInfinity;

    void Start() 
    {
        victim = GetComponent<CharacterBase>();
        if(victim == null)
        {
            Destroy(this);
            return;
        };
        OnTriggered(); 
    }

    void Update()
    {
        OnUpdate();
        if(!isInfinity)
        {
            leftTime -= Time.deltaTime;
            if(leftTime <= 0)
            {
                OnEnded();
                Destroy(this);
            };
        };
    }

    public virtual void OnTriggered() { }
    public virtual void OnUpdate() { }
    public virtual void OnEnded() { }
}
