using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
	[SerializeField]
	//　ポーズした時に表示するUIのプレハブ
	private GameObject pauseUIPrefab;
	//　ポーズUIのインスタンス
	private GameObject pauseUIInstance;

	public bool isStop;
	//public bool isPushQ;

	// Start is called before the first frame update
	void Start()
	{
		isStop = false;
		//isPushQ = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown("q") || Input.GetKeyDown("joystick button 6"))
		{
			//isPushQ = true;
			if (!isStop)
			{
				if (pauseUIInstance == null)
				{
					pauseUIInstance = GameObject.Instantiate(pauseUIPrefab) as GameObject;
				}
				Time.timeScale = 0;
				isStop = true;
			}
			else if (isStop)
			{
				Destroy(pauseUIInstance);
				Time.timeScale = 1;
				isStop = false;
			}
		}
	}
}

