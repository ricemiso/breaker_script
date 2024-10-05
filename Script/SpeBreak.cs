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

		// ���̃I�u�W�F�N�g���폜
		Destroy(gameObject);
		// ��莞�Ԍ��brokenPrefab���폜
		Destroy(brokentransform.gameObject, 5f);

		Debug.Log("�Փ˔���");

		// brokenPrefab�̑S�Ă̎q�I�u�W�F�N�g���擾
		foreach (Transform fragment in brokentransform)
		{
			Rigidbody rb = fragment.GetComponent<Rigidbody>();

			if (rb != null)
			{
				// �Փ˂̕������v�Z
				Vector3 knockbackDirection = fragment.position - transform.position;
				knockbackDirection.Normalize(); // �����x�N�g���𐳋K��

				// �e�t���O�����g�Ƀ����_���ȗ͂������Đ�����΂�
				rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
			}
			else
			{
				Debug.LogWarning("�t���O�����g��Rigidbody���A�^�b�`����Ă��܂���: " + fragment.name);
			}
		}
	}

	private void OnTriggerEnter(Collider collision)
	{
		Debug.Log(this.gameObject.tag + "  " + collision.gameObject.tag);

		// Player�̎�Ƃ̏Փ˔���
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
