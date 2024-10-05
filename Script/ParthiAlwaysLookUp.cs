using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParthiAlwaysLookUp : MonoBehaviour
{
	public ParticleSystem particle;
	private Quaternion rot;

	// Start is called before the first frame update
	void Start()
	{
		rot = particle.transform.rotation;
	}

	// Update is called once per frame
	void Update()
	{
		transform.rotation = rot;
	}
}
