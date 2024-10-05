using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerParth : MonoBehaviour
{
    protected ParticleSystem particleSystem;

    public GameObject player;  // Player1オブジェクトを参照
    public GameObject player2;  // Player2オブジェクトを参照

    public Camera playerCamera; // プレイヤーのカメラ
    public float activationDistance = 500.0f;  // パーティクルが再生される距離の閾値

    protected MonoBehaviour playerController;

    protected virtual void Start()
    {
        player = GameObject.Find(FighterName);
        player2 = GameObject.Find(Fighter2Name);

        playerController = (MonoBehaviour)player.GetComponent(ControllerType);

        // 子クラスでParticleSystemを設定
        InitializeParticleSystem();

        if (particleSystem == null)
        {
            Debug.LogError("ParticleSystem is not assigned.");
            return;
        }

        StopParticles();

        

        // カメラからパーティクルを非表示にするための設定
        if (playerCamera != null)
        {
            // パーティクルシステム用の新しいレイヤーを作成
            int particleLayer = GetParticleLayer();
            if (particleLayer == -1)
            {
                Debug.LogError("Please add a layer named '" + GetParticleLayerName() + "' in the Layer settings.");
            }
            else
            {
                Debug.Log("Setting particle system and children to layer: " + particleLayer);

                // パーティクルシステムとその子オブジェクトを新しいレイヤーに設定
                SetLayerRecursively(particleSystem.gameObject, particleLayer);

                // カメラのCulling Maskから新しいレイヤーを除外
                Debug.Log("Before Culling Mask: " + playerCamera.cullingMask);
                playerCamera.cullingMask &= ~(1 << particleLayer);
                Debug.Log("After Culling Mask: " + playerCamera.cullingMask);
            }
        }
    }

    protected virtual void Update()
    {

        float distance = Vector3.Distance(player.transform.position, player2.transform.position);

        if (distance > activationDistance && !(bool)playerController.GetType().GetField("isCrouch").GetValue(playerController) && !(bool)playerController.GetType().GetField("Special").GetValue(playerController))
        {
            PlayParticles();
        }
        else
        {
            StopParticles();
        }
    }


    // パーティクルを再生するメソッド
    public void PlayParticles()
    {
        if (particleSystem != null && !particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
    }

    // パーティクルを停止するメソッド
    public void StopParticles()
    {
        if (particleSystem != null && particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }
    }

    // パーティクルを一時停止するメソッド
    public void PauseParticles()
    {
        if (particleSystem != null)
        {
            particleSystem.Pause();
        }
    }

    // パーティクルをクリアするメソッド
    public void ClearParticles()
    {
        if (particleSystem != null)
        {
            particleSystem.Clear();
        }
    }

    // オブジェクトとそのすべての子オブジェクトにレイヤーを再帰的に設定するメソッド
    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null) return;

        obj.layer = newLayer;
        Debug.Log("Setting layer for: " + obj.name);

        foreach (Transform child in obj.transform)
        {
            if (child != null)
            {
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }
    }

    // コントローラーを初期化する抽象メソッド
    protected virtual System.Type ControllerType { get; }

    protected virtual string FighterName { get; }

    protected virtual string Fighter2Name { get; }

    // パーティクル用のレイヤー名を取得する抽象メソッド
    protected abstract string GetParticleLayerName();

    // ParticleSystemを初期化する抽象メソッド
    protected abstract void InitializeParticleSystem();

    // パーティクル用のレイヤーを取得するメソッド
    protected int GetParticleLayer()
    {
        return LayerMask.NameToLayer(GetParticleLayerName());
    }
}
