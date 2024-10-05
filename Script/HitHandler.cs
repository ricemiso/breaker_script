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

	//�I�u�W�F�N�g�ƐڐG�����u�ԂɌĂяo�����
	private void OnTriggerEnter(Collider other)
	{
		// �U���������肪Enemy�̏ꍇ
		if ((other.gameObject.layer == 12 || other.gameObject.layer == 16) && (bool)playerController.GetType().GetField("isAnim").GetValue(playerController)) 
		{
			ParticleSystem particles = Instantiate(Parth, transform.position, Quaternion.identity);
			particles.Play();
			StartCoroutine(DestroyParticleAfterDelay(particles, 0.2f));
		}

		// �U���������肪Player�̏ꍇ
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
