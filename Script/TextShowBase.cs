using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class TextShowBase : MonoBehaviour
{
    public TextMeshProUGUI text;

    protected MonoBehaviour playerController;

    // �q�N���X�Ńv���C���[�R���g���[���[��ݒ肷��
    protected abstract void SetPlayerController();

    void Start()
    {
        text.enabled = false;

        // If the TextMeshProUGUI component is not set, get it from the parent GameObject
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        if (text == null)
        {
            Debug.LogError("TextMeshProUGUI is not assigned and not found on the GameObject.");
            return;
        }

        SetPlayerController(); // �q�N���X�ŃR���g���[���[��ݒ�
    }

    void Update()
    {
        if (IsParth() && !IsShow())
        {
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }
    }

    // `isParth` �̒l���擾���钊�ۃ��\�b�h
    protected abstract bool IsParth();

    // `isShow` �̒l���擾���钊�ۃ��\�b�h
    protected abstract bool IsShow();
}
