using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeParth : SpeParthBase
{
	public GameObject player1;
	private P_Controller_Platform player1Controller;

    protected override MonoBehaviour GetPlayerController()
    {
        player1Controller = player1.GetComponent<P_Controller_Platform>();
        return player1Controller;
    }


    protected override bool IsSpecial()
    {
        return player1Controller.Special;
    }
}
