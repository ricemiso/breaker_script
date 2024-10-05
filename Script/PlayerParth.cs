using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParth : BasePlayerParth
{
    public ParticleSystem player1ParticleSystem;

    protected override string FighterName => "Player1_Fighter";
    protected override string Fighter2Name => "Player2_Fighter";
    protected override System.Type ControllerType => typeof(P_Controller_Platform);


    protected override string GetParticleLayerName()
    {
        return "ParticleLayer";
    }

    protected override void InitializeParticleSystem()
    {
        particleSystem = player1ParticleSystem;
    }
}
