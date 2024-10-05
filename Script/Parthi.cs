using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parthi : MonoBehaviour
{
	public ParticleSystem particleSystem;

	public GameObject player1;  // Player1オブジェクトを参照
	public GameObject player2;  // Player2オブジェクトを参照

	P_Controller_Platform player1Controller;
	P2_Controller_Platform player2Controller;

	public GameObject item;
	CarBreak grabItem;

	void Start()
	{
		// パーティクルシステムが設定されていない場合、親オブジェクトから取得
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
