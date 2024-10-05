using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parthi : MonoBehaviour
{
	public ParticleSystem particleSystem;

	public GameObject player1;  // Player1�I�u�W�F�N�g���Q��
	public GameObject player2;  // Player2�I�u�W�F�N�g���Q��

	P_Controller_Platform player1Controller;
	P2_Controller_Platform player2Controller;

	public GameObject item;
	CarBreak grabItem;

	void Start()
	{
		// �p�[�e�B�N���V�X�e�����ݒ肳��Ă��Ȃ��ꍇ�A�e�I�u�W�F�N�g����擾
		if (particleSystem == null)
		{
			particleSystem = GetComponent<ParticleSystem>();
		}

		if (particleSystem == null)
		{
			Debug.LogError("ParticleSystem is not assigned and not found on the GameObject.");
			return;
		}

		StopParticles();

		if (player1 != null)
		{
			player1Controller = player1.GetComponent<P_Controller_Platform>();
		}
		if (player2 != null)
		{
			player2Controller = player2.GetComponent<P2_Controller_Platform>();
		}

		grabItem = item.GetComponent<CarBreak>();

		if (player1Controller == null || player2Controller == null)
		{
			Debug.LogError("Player controllers are not assigned correctly.");
		}
	}

	public void Update()
	{}

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

	private void OnTriggerEnter(Collider other)
	{
		if(!grabItem.isGrabed
			&& ((other.gameObject.CompareTag("Player") && !player1Controller.isShow)
			|| (other.gameObject.CompareTag("Player2") && !player2Controller.isShow)))
		{
			PlayParticles();
		} 

	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player2"))
		{
			StopParticles();
		}
	}
}
