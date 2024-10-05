using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_Hit_Left : HitHandler
{
    protected override string FighterName => "Player2_Fighter"; // プレイヤーの名前に合わせて変更
    protected override System.Type ControllerType => typeof(P2_Controller_Platform);
}