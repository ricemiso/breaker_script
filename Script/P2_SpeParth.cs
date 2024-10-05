using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2_SpeParth : SpeParthBase
{
    public GameObject player2;
    private P2_Controller_Platform player2Controller;

    protected override MonoBehaviour GetPlayerController()
    {
        player2Controller = player2.GetComponent<P2_Controller_Platform>();
        return player2Controller;
    }

    protected override bool IsSpecial()
    {
        return player2Controller.Special;
    }
}
