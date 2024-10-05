using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitHandler : MonoBehaviour
{
	public ParticleSystem Parth;
	public GameObject player;
	private MonoBehaviour playerController;

	protected abstract string FighterName { get; }
	protected abstract System.Type ControllerType { get; }

	private void Start()
	{
		Parth.Stop();
		player = GameObject.Find(FighterName);

		playerController = (MonoBehaviour)player.GetComponent(ControllerType);
	}

	//オブジェクトと接触した瞬間に呼び出される
	private void OnTriggerEnter(Collider other)
	{
		// 攻撃した相手がEnemyの場合
		if ((other.gameObject.layer == 12 || other.gameObject.layer == 16) && (bool)playerController.GetType().GetField("isAnim").GetValue(playerController)) 
		{
			ParticleSystem particles = Instantiate(Parth, transform.position, Quaternion.identity);
			particles.Play();
			StartCoroutine(DestroyParticleAfterDelay(particles, 0.2f));
		}

		// 攻撃した相手がPlayerの場合
		if (other.gameObject.layer == 13 && (bool)playerController.GetType().GetField("isAnim").GetValue(playerController))
		{
			ParticleSystem particles2 = Instantiate(Parth, transform.position, Quaternion.identity);
			particles2.Play();
			StartCoroutine(DestroyParticleAfterDelay(particles2, 0.2f));
		}
	}

	IEnumerator DestroyParticleAfterDelay(ParticleSystem particles, float delay)
	{
		yield return new WaitForSeconds(delay);
		Destroy(particles.gameObject);
	}

}
