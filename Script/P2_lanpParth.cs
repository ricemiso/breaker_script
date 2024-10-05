using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_LanpParth : LanpParthBase
{
    public GameObject player2;
    protected P2_Controller_Platform player2Controller;

    protected override MonoBehaviour GetPlayerController()
    {
        player2Controller = player2.GetComponent<P2_Controller_Platform>();
        return player2Controller;
    }

    protected override GameObject GetWeapon()
    {
        return player2Controller.GetWeapon();
    }

    protected override bool IsAttackConditionMet()
    {
        return player2Controller.isAttack && player2Controller.isAttack1;
    }
}