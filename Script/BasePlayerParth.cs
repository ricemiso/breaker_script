using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayerParth : MonoBehaviour
{
    protected ParticleSystem particleSystem;

    public GameObject player;  // Player1�I�u�W�F�N�g���Q��
    public GameObject player2;  // Player2�I�u�W�F�N�g���Q��

    public Camera playerCamera; // �v���C���[�̃J����
    public float activationDistance = 500.0f;  // �p�[�e�B�N�����Đ�����鋗����臒l

    protected MonoBehaviour playerController;

    protected virtual void Start()
    {
        player = GameObject.Find(FighterName);
        player2 = GameObject.Find(Fighter2Name);

        playerController = (MonoBehaviour)player.GetComponent(ControllerType);

        // �q�N���X��ParticleSystem��ݒ�
        InitializeParticleSystem();

        if (particleSystem == null)
        {
            Debug.LogError("ParticleSystem is not assigned.");
            return;
        }

        StopParticles();

        

        // �J��������p�[�e�B�N�����\���ɂ��邽�߂̐ݒ�
        if (playerCamera != null)
        {
            // �p�[�e�B�N���V�X�e���p�̐V�������C���[���쐬
            int particleLayer = GetParticleLayer();
            if (particleLayer == -1)
            {
                Debug.LogError("Please add a layer named '" + GetParticleLayerName() + "' in the Layer settings.");
            }
            else
            {
                Debug.Log("Setting particle system and children to layer: " + particleLayer);

                // �p�[�e�B�N���V�X�e���Ƃ��̎q�I�u�W�F�N�g��V�������C���[�ɐݒ�
                SetLayerRecursively(particleSystem.gameObject, particleLayer);

                // �J������Culling Mask����V�������C���[�����O
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


    // �p�[�e�B�N�����Đ����郁�\�b�h
    public void PlayParticles()
    {
        if (particleSystem != null && !particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
    }

    // �p�[�e�B�N�����~���郁�\�b�h
    public void StopParticles()
    {
        if (particleSystem != null && particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }
    }

    // �p�[�e�B�N�����ꎞ��~���郁�\�b�h
    public void PauseParticles()
    {
        if (particleSystem != null)
        {
            particleSystem.Pause();
        }
    }

    // �p�[�e�B�N�����N���A���郁�\�b�h
    public void ClearParticles()
    {
        if (particleSystem != null)
        {
            particleSystem.Clear();
        }
    }

    // �I�u�W�F�N�g�Ƃ��̂��ׂĂ̎q�I�u�W�F�N�g�Ƀ��C���[���ċA�I�ɐݒ肷�郁�\�b�h
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

    // �R���g���[���[�����������钊�ۃ��\�b�h
    protected virtual System.Type ControllerType { get; }

    protected virtual string FighterName { get; }

    protected virtual string Fighter2Name { get; }

    // �p�[�e�B�N���p�̃��C���[�����擾���钊�ۃ��\�b�h
    protected abstract string GetParticleLayerName();

    // ParticleSystem�����������钊�ۃ��\�b�h
    protected abstract void InitializeParticleSystem();

    // �p�[�e�B�N���p�̃��C���[���擾���郁�\�b�h
    protected int GetParticleLayer()
    {
        return LayerMask.NameToLayer(GetParticleLayerName());
    }
}
