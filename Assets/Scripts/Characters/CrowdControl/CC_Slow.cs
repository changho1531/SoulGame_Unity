using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC_Slow : CrowdControlBase
{
    public override CrowdControl CCtype() { return CrowdControl.Slow; }

    public float amount;

    public override void OnTriggered()
    {
        victim.moveSpeedMultiplier -= amount;
    }

    public override void OnEnded()
    {
        victim.moveSpeedMultiplier += amount;
    }
}
