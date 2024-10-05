using System.Collections;
using UnityEngine;

public class P2_BlockParth : BlockParthBase
{
    public ParticleSystem particleSystem2;
    public GameObject player2;
    protected P2_Controller_Platform player2Controller;


    protected override MonoBehaviour GetPlayerController()
    {
        player2Controller = player2.GetComponent<P2_Controller_Platform>();
        return player2Controller;
    }

    protected override void InitializeBlockParticleSystem()
    {
        particleSystem = particleSystem2; // 親クラスのパーティクルシステムを設定
    }

    protected override bool IsBlock()
    {
        return player2Controller.isBlocking;
    }

    protected override bool IsWeakDamage()
    {
        return player2Controller.isweakdamage;
    }

    protected override bool IsChangeWeakDamage()
    {
        return player2Controller.isweakdamage = false;
    }

    protected override bool IsChangeStrongDamage()
    {
        return player2Controller.isstrongdamage = true;
    }

    protected override IEnumerator IsDisableBlockInput()
    {
        return player2Controller.DisableBlockInput();
    }
}
