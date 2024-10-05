using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageWall : MonoBehaviour
{
	float posX;
	float posZ;
	float moveSpeed = 0.005f;
	//float moveSpeed = 0.05f;
	public bool isArrive;
	bool isMove;
	float moveTimer = 20f;
	float stopTimer = 100f;
	bool isMoving = false;

	GameObject obj;
	Pause pause;

	// Start is called before the first frame update
	void Start()
	{
		posX = transform.position.x;
		posZ = transform.position.z;

		obj = GameObject.Find("Cameras");
		pause = obj.GetComponent<Pause>();

		isArrive = false;
		isMove = false;

		StartCoroutine(WaitBeforeMove(3.0f));
	}

	// Update is called once per frame
	void Update()
	{
		if (!isMoving) return;

		if (pause.isStop || isArrive)
		{
			return;
		}

		Debug.Log("T isMove " + isMove);


		if (isMove)
		{
			moveTimer -= Time.deltaTime;
			if(moveTimer <= 0f)
			{
				isMove = false;
				moveTimer = 20f;
			}
		}
		else
		{
			stopTimer -= Time.deltaTime;
			if (stopTimer <= 0f)
			{
				isMove = true;
				stopTimer = 100f;
			}

			return;
		}

		switch (gameObject.name)
		{
			case "DamageZone":
				if (transform.position.z <= 32)
				{
					transform.position = new Vector3(-35, 5, 32);
					isArrive = true;
				}
				else
				{
					posZ -= moveSpeed;
					transform.position = new Vector3(-35, 5, posZ);
				}
				break;
			case "DamageZone2":
				if (transform.position.z >= 32)
				{
					transform.position = new Vector3(-35, 5, 32);
					isArrive = true;
				}
				else
				{
					posZ += moveSpeed;
					transform.position = new Vector3(-35, 5, posZ);
				}
				break;
			case "DamageZone3":
				if (transform.position.x >= -35)
				{
					transform.position = new Vector3(-35, 5, 32);
					isArrive = true;
				}
				else
				{
					posX += moveSpeed;
					transform.position = new Vector3(posX, 5, 32);
				}
				break;
			case "DamageZone4":
				if (transform.position.x <= -35)
				{
					transform.position = new Vector3(-35, 5, 32);
					isArrive = true;
				}
				else
				{
					posX -= moveSpeed;
					transform.position = new Vector3(posX, 5, 32);
				}
				break;
		}
	}

	IEnumerator WaitBeforeMove(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		isMoving = true;
	}
}
