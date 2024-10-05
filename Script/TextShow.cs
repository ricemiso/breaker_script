using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextShow : TextShowBase
{
    public GameObject player1;

    P_Controller_Platform player1Controller;

    protected override void SetPlayerController()
    {
        if (player1 != null)
        {
            player1Controller = player1.GetComponent<P_Controller_Platform>();
        }
    }

    protected override bool IsParth()
    {
        return player1Controller != null && player1Controller.isParth;
    }

    protected override bool IsShow()
    {
        return player1Controller != null && player1Controller.isShow;
    }
}
