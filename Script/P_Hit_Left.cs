using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Hit_Left : HitHandler
{
    protected override string FighterName => "Player1_Fighter"; // プレイヤーの名前に合わせて変更
    protected override System.Type ControllerType => typeof(P_Controller_Platform);
}