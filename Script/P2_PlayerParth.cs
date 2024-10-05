using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_PlayerParth : BasePlayerParth
{
    public ParticleSystem player2ParticleSystem;

    protected override string FighterName => "Player2_Fighter";
    protected override string Fighter2Name => "Player1_Fighter";
    protected override System.Type ControllerType => typeof(P2_Controller_Platform);

    protected override string GetParticleLayerName()
    {
        return "ParticleLayer2";
    }

    protected override void InitializeParticleSystem()
    {
        particleSystem = player2ParticleSystem;
    }
}
