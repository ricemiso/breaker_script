using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeBreak : MonoBehaviour
{
	[SerializeField] private Transform brokenPrefab;
	public CarBreak br;
	public AudioClip se;
	public float knockbackForce = 30f;
	P_Controller_Platform player1Controller;
	P2_Controller_Platform player2Controller;
	// Start is called before the first frame update
	void Start()
	{
		player1Controller = GameObject.Find("Player1_Fighter").GetComponent<P_Controller_Platform>();
		player2Controller = GameObject.Find("Player2_Fighter").GetComponent<P2_Controller_Platform>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void BreakObj()
	{
		Transform brokentransform = Instantiate(brokenPrefab, transform.position, transform.rotation);
		brokentransform.localScale = transform.localScale;

		AudioSource.PlayClipAtPoint(se, new Vector3(0, 0, 0));

		// 元のオブジェクトを削除
		Destroy(gameObject);
		// 一定時間後にbrokenPrefabを削除
		Destroy(brokentransform.gameObject, 5f);

		Debug.Log("衝突発生");

		// brokenPrefabの全ての子オブジェクトを取得
		foreach (Transform fragment in brokentransform)
		{
			Rigidbody rb = fragment.GetComponent<Rigidbody>();

			if (rb != null)
			{
				// 衝突の方向を計算
				Vector3 knockbackDirection = fragment.position - transform.position;
				knockbackDirection.Normalize(); // 方向ベクトルを正規化

				// 各フラグメントにランダムな力を加えて吹き飛ばす
				rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
			}
			else
			{
				Debug.LogWarning("フラグメントにRigidbodyがアタッチされていません: " + fragment.name);
			}
		}
	}

	private void OnTriggerEnter(Collider collision)
	{
		Debug.Log(this.gameObject.tag + "  " + collision.gameObject.tag);

		// Playerの手との衝突判定
		if (collision.gameObject.CompareTag("Hand_P1"))
		{
			player1Controller.Special = true;
			BreakObj();
		}

		else if (collision.gameObject.CompareTag("Hand_P2"))
		{
			player2Controller.Special = true;
			BreakObj();
		}

	}

}
