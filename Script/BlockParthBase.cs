using System.Collections;
using UnityEngine;

public abstract class BlockParthBase : MonoBehaviour
{
    public ParticleSystem particleSystem;
    protected GameObject player;
    public float scaleReductionStep = 0.1f; // �p�[�e�B�N���X�P�[���̌�����
    public float minScale = 0.1f; // �p�[�e�B�N���̍ŏ��X�P�[��
    private MonoBehaviour playerController;
    private Vector3 originalScale; // �p�[�e�B�N���̌��̃X�P�[����ۑ����邽�߂̕ϐ�

    private float blockStartTime; // �u���b�N�J�n���Ԃ�ۑ�����ϐ�
    private bool blockStarted; // �u���b�N���J�n���ꂽ���ǂ����������t���O

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

        // �p�[�e�B�N���̌��̃X�P�[����ۑ�����
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
            // �h�䂪�������ꂽ��p�[�e�B�N���̃X�P�[�������ɖ߂�
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
            particleSystem.Clear(); // �c��̃p�[�e�B�N���𑦎��N���A
        }
    }


    private void ReduceParticleScale()
    {
        Vector3 currentScale = particleSystem.transform.localScale;

        // �V�����X�P�[�����v�Z���A�ŏ��X�P�[���������Ȃ��悤�ɂ���
        float newScaleX = Mathf.Max(currentScale.x - scaleReductionStep, minScale);
        float newScaleY = Mathf.Max(currentScale.y - scaleReductionStep, minScale);
        float newScaleZ = Mathf.Max(currentScale.z - scaleReductionStep, minScale);

        // �X�P�[���̒l���X�V
        particleSystem.transform.localScale = new Vector3(newScaleX, newScaleY, newScaleZ);

        // �ŏ��X�P�[������������ꍇ�̏���
        if (newScaleX <= minScale || newScaleY <= minScale || newScaleZ <= minScale)
        {
            IsChangeStrongDamage();
            StartCoroutine(IsDisableBlockInput());
        }

        // �f�o�b�O���b�Z�[�W��ǉ����āA�X�P�[���̕ύX���m�F
        Debug.Log("Reduced particle scale: " + particleSystem.transform.localScale);
    }



    private void RestoreOriginalScale()
    {
        // ���̃X�P�[���Ƀp�[�e�B�N���̃X�P�[����߂�
        particleSystem.transform.localScale = originalScale;

        // �f�o�b�O���b�Z�[�W��ǉ����āA�X�P�[���̕ύX���m�F
        Debug.Log("Restored original particle scale: " + particleSystem.transform.localScale);
    }

   
}
