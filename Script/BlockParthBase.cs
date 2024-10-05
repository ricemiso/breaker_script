using System.Collections;
using UnityEngine;

public abstract class BlockParthBase : MonoBehaviour
{
    public ParticleSystem particleSystem;
    protected GameObject player;
    public float scaleReductionStep = 0.1f; // パーティクルスケールの減少幅
    public float minScale = 0.1f; // パーティクルの最小スケール
    private MonoBehaviour playerController;
    private Vector3 originalScale; // パーティクルの元のスケールを保存するための変数

    private float blockStartTime; // ブロック開始時間を保存する変数
    private bool blockStarted; // ブロックが開始されたかどうかを示すフラグ

    protected abstract MonoBehaviour GetPlayerController();
    protected abstract bool IsBlock();
    protected abstract bool IsWeakDamage();
    protected abstract bool IsChangeWeakDamage();
    protected abstract bool IsChangeStrongDamage();
    protected abstract void InitializeBlockParticleSystem();
    protected abstract IEnumerator IsDisableBlockInput();

    protected virtual void Start()
    {
        playerController = GetPlayerController();

        InitializeBlockParticleSystem();

        // パーティクルの元のスケールを保存する
        originalScale = particleSystem.transform.localScale;

        StopParticles2();
    }

    protected virtual void Update()
    {
        
        if (IsBlock())
        {
            PlayParticles2();
           
            if (IsWeakDamage())
            {
                ReduceParticleScale();

                IsChangeWeakDamage();
            }

        }
        else
        {
            // 防御が解除されたらパーティクルのスケールを元に戻す
            RestoreOriginalScale();

            StopParticles2();
        }
    }

 
    public void PlayParticles2()
    {
        if (particleSystem != null && !particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
    }

    public void StopParticles2()
    {
        if (particleSystem != null && particleSystem.isPlaying)
        {
            particleSystem.Stop();
            particleSystem.Clear(); // 残りのパーティクルを即時クリア
        }
    }


    private void ReduceParticleScale()
    {
        Vector3 currentScale = particleSystem.transform.localScale;

        // 新しいスケールを計算し、最小スケールを下回らないようにする
        float newScaleX = Mathf.Max(currentScale.x - scaleReductionStep, minScale);
        float newScaleY = Mathf.Max(currentScale.y - scaleReductionStep, minScale);
        float newScaleZ = Mathf.Max(currentScale.z - scaleReductionStep, minScale);

        // スケールの値を更新
        particleSystem.transform.localScale = new Vector3(newScaleX, newScaleY, newScaleZ);

        // 最小スケールを下回った場合の処理
        if (newScaleX <= minScale || newScaleY <= minScale || newScaleZ <= minScale)
        {
            IsChangeStrongDamage();
            StartCoroutine(IsDisableBlockInput());
        }

        // デバッグメッセージを追加して、スケールの変更を確認
        Debug.Log("Reduced particle scale: " + particleSystem.transform.localScale);
    }



    private void RestoreOriginalScale()
    {
        // 元のスケールにパーティクルのスケールを戻す
        particleSystem.transform.localScale = originalScale;

        // デバッグメッセージを追加して、スケールの変更を確認
        Debug.Log("Restored original particle scale: " + particleSystem.transform.localScale);
    }

   
}
