using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_GroundCheck : GroundCheckBase
{
    protected override void SetTargetAndController()
    {
        target = GameObject.Find("Player2_Fighter");
        playerController = target.GetComponent<P2_Controller_Platform>();
    }
}