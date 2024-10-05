using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GroundCheckBase : MonoBehaviour
{
    protected GameObject target;
    protected MonoBehaviour playerController;  // �e�N���X�ł̓R���g���[���[��ėp�I�Ɉ���

    // �^�[�Q�b�g�ƃR���g���[���[�̐ݒ�͎q�N���X�ōs��
    protected abstract void SetTargetAndController();

    // Start is called before the first frame update
    void Start()
    {
        SetTargetAndController();  // �q�N���X�Ń^�[�Q�b�g�ƃR���g���[���[��ݒ�
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

    // `isGround` �v���p�e�B��ݒ肷�郁�\�b�h
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
