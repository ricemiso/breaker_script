using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Hit_Left : HitHandler
{
    protected override string FighterName => "Player1_Fighter"; // �v���C���[�̖��O�ɍ��킹�ĕύX
    protected override System.Type ControllerType => typeof(P_Controller_Platform);
}