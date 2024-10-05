using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_LanpParth2 : P2_LanpParth
{
    protected override bool IsAttackConditionMet()
    {
        return player2Controller.isAttack && player2Controller.isAttack2;
    }
}