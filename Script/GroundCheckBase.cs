using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundCheckBase : MonoBehaviour
{
    protected GameObject target;
    protected MonoBehaviour playerController;  // 親クラスではコントローラーを汎用的に扱う

    // ターゲットとコントローラーの設定は子クラスで行う
    protected abstract void SetTargetAndController();

    // Start is called before the first frame update
    void Start()
    {
        SetTargetAndController();  // 子クラスでターゲットとコントローラーを設定
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            SetIsGround(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            SetIsGround(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ground"))
        {
            SetIsGround(true);
        }
    }

    // `isGround` プロパティを設定するメソッド
    protected void SetIsGround(bool value)
    {
        if (playerController is P_Controller_Platform pController)
        {
            pController.isGround = value;
        }
        else if (playerController is P2_Controller_Platform p2Controller)
        {
            p2Controller.isGround = value;
        }
    }
}
