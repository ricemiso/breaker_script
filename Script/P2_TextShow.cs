using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class P2_TextShow : TextShowBase
{
    public GameObject player2;

    P2_Controller_Platform player2Controller;

    protected override void SetPlayerController()
    {
        if (player2 != null)
        {
            player2Controller = player2.GetComponent<P2_Controller_Platform>();
        }
    }

    protected override bool IsParth()
    {
        return player2Controller != null && player2Controller.isParth;
    }

    protected override bool IsShow()
    {
        return player2Controller != null && player2Controller.isShow;
    }
}
