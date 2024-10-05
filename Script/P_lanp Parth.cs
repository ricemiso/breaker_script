using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_LanpParth : LanpParthBase
{
    public GameObject player1;
    protected P_Controller_Platform player1Controller;

    protected override MonoBehaviour GetPlayerController()
    {
        player1Controller = player1.GetComponent<P_Controller_Platform>();
        return player1Controller;
    }

    protected override GameObject GetWeapon()
    {
        return player1Controller.GetWeapon();
    }

    protected override bool IsAttackConditionMet()
    {
        return player1Controller.isAttack && player1Controller.isAttack1;
    }
}