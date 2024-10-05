using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_GroundCheck : GroundCheckBase
{
    protected override void SetTargetAndController()
    {
        target = GameObject.Find("Player1_Fighter");
        playerController = target.GetComponent<P_Controller_Platform>();
    }
}