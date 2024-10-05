using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageWallApproachTimer : MonoBehaviour
{
	public static bool isMove;
	float moveTimer = 20f;
	float stopTimer = 100f;
	bool isMoving = false;
	public AudioClip warningSe;

	int timer = 0;

	GameObject obj;
	TextMeshProUGUI timerText;
	RawImage image;
	TextMeshProUGUI mes;

	public DamageWall wall;

	// Start is called before the first frame update
	void Start()
	{
		isMove = false;

		obj = GameObject.Find("WallTimer");
		timerText = obj.GetComponent<TextMeshProUGUI>();

		obj = GameObject.Find("mark");
		image = obj.GetComponent<RawImage>();

		obj = GameObject.Find("Message");
		mes = obj.GetComponent<TextMeshProUGUI>();

		mes.enabled = false;

		StartCoroutine(WaitBeforeMove(3.0f));
	}

	// Update is called once per frame
	void Update()
	{
		if (!isMoving) return;

		if (wall.isArrive)
		{
			timerText.enabled = false;
			image.enabled = false;
			mes.enabled = false;
			return;
		}


		if (isMove)
		{
			mes.enabled = true;
			moveTimer -= Time.deltaTime;
			if (moveTimer <= 0f)
			{
				mes.enabled = false;
				isMove = false;
				moveTimer = 20f;
			}
		}
		else
		{
			stopTimer -= Time.deltaTime;
			timer = (int)stopTimer;
			timerText.text = timer.ToString();

			if (stopTimer <= 0f)
			{
				timer = 0;
				timerText.text = timer.ToString();
				isMove = true;
				stopTimer = 100f;

				AudioSource.PlayClipAtPoint(warningSe, new Vector3(0, 0, 0));
			}

		}
	}
	IEnumerator WaitBeforeMove(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		isMoving = true;
	}
}
