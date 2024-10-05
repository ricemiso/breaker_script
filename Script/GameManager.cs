using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
	public int Main;

	public void Activate()
	{
		SceneManager.LoadScene("MenuScene");
	}

	void Update()
	{
		if (Input.anyKey)
		{
			Activate();
		}
	}

}
