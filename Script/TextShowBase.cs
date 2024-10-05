using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class TextShowBase : MonoBehaviour
{
    public TextMeshProUGUI text;

    protected MonoBehaviour playerController;

    // 子クラスでプレイヤーコントローラーを設定する
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

        SetPlayerController(); // 子クラスでコントローラーを設定
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

    // `isParth` の値を取得する抽象メソッド
    protected abstract bool IsParth();

    // `isShow` の値を取得する抽象メソッド
    protected abstract bool IsShow();
}
