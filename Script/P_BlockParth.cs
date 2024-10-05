using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_BlockParth : BlockParthBase
{
    public ParticleSystem particleSystem2;
    public GameObject player1;
    protected P_Controller_Platform player1Controller;


    protected override MonoBehaviour GetPlayerController()
    {
        player1Controller = player1.GetComponent<P_Controller_Platform>();
        return player1Controller;
    }

    protected override void InitializeBlockParticleSystem()
    {
        particleSystem = particleSystem2; // 親クラスのパーティクルシステムを設定
    }

    protected override bool IsBlock()
    {
        return player1Controller.isBlocking;
    }

    protected override bool IsWeakDamage()
    {
        return player1Controller.isweakdamage;
    }

    protected override bool IsChangeWeakDamage()
    {
        return player1Controller.isweakdamage = false;
    }

    protected override bool IsChangeStrongDamage()
    {
        return player1Controller.isstrongdamage = true;
    }

    protected override IEnumerator IsDisableBlockInput()
    {
        return player1Controller.DisableBlockInput();
    }
}
