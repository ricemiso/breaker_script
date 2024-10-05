using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_LanpParth2 : P_LanpParth
{
    protected override bool IsAttackConditionMet()
    {
        return player1Controller.isAttack && player1Controller.isAttack2;
    }
}