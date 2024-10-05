using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class CarBreak : MonoBehaviour
{
	[SerializeField] private Transform brokenPrefab;
	public float knockbackForce = 30f;
	public AudioClip breakSe;
	public AudioClip punchSe;
	public GameObject player1;
	public GameObject player2;

	private Vector3 vec;
	private Quaternion rot;

	P_Controller_Platform player1Controller;
	P2_Controller_Platform player2Controller;

	//todo:�ϐ����ύX
	private int hitcar = 10;
	private int car = 15;
	private int car1 = 20;
	private int car2 = 25;
	private int car3 = 30;
	private int lamp = 10;
	private int prop = 10;

	private int giveDamage;
	[Header("�A�C�e�����E��ꂽ���̃C�x���g")]
	public UnityEvent onPickUp;

	public bool isGrabed = false; //���g���͂܂�Ă��邩

	void Start()
	{
		vec = this.transform.position;
		rot = transform.rotation;

		// �����I�Ƀv���C���[�I�u�W�F�N�g�������Đݒ�i�v���C���[�^�O���g�p�j
		if (player1 == null)
		{
			player1 = GameObject.Find("Player1_Fighter");
		}
		if (player2 == null)
		{
			player2 = GameObject.Find("Player2_Fighter");
		}
		player1Controller = player1.GetComponent<P_Controller_Platform>();
		player2Controller = player2.GetComponent<P2_Controller_Platform>();

		switch (gameObject.tag)
		{

			case "Item":
				giveDamage = lamp;
				break;
			case "Item2":
				giveDamage = car;
				break;
			case "Item3":
				giveDamage = car1;
				break;
			case "Item4":
				giveDamage = car2;
				break;
			case "Item5":
				giveDamage = car3;
				break;
			case "Item6":
				giveDamage = prop;
				break;
		}
	}

	void Update()
	{
		if (gameObject.tag == "Building")
		{
			transform.position = vec;
			transform.rotation = rot;
		}

		if (transform.gameObject == player1Controller.GetWeapon() || transform.gameObject == player2Controller.GetWeapon())
		{
			isGrabed = true;
			Debug.Log("isGrabed" + isGrabed);
		}
		else
		{
			isGrabed = false;
		}
	}

	private void OnTriggerEnter(Collider collision)
	{

		if (collision.gameObject.CompareTag("Wave"))
		{
			breakCar();
		}

		// Player1�܂��͂��̎�Ƃ̏Փ˔���
		if (IsPlayer2HoldingItem() && player2Controller.isAttack && collision.gameObject.CompareTag("Player"))
		{
			if (!player1Controller.isBlocking)
			{
				player1Controller.hitPoint -= giveDamage;
				player1Controller.hpBar.Damage(giveDamage);
				player1Controller.Skill7();
			}

			breakCar();
			IsAnim2(false);

		}

		// Player2�܂��͂��̎�Ƃ̏Փ˔���
		if (IsPlayer1HoldingItem() && player1Controller.isAttack && collision.gameObject.CompareTag("Player2"))
		{
			if (!player2Controller.isBlocking)
			{
				player2Controller.hitPoint -= giveDamage;
				player2Controller.hpBar.Damage(giveDamage);
				player2Controller.Skill7();
			}

			breakCar();
			IsAnim1(false);

		}

		if (collision.gameObject.CompareTag("Hand_P1"))
		{
			player1Controller.ColliderReset();
			Damaged();

		}

		if (collision.gameObject.CompareTag("Hand_P2"))
		{
			player2Controller.ColliderReset();
			Damaged();
		}

	}

	public void Damaged()
	{
		if (hitcar >= 1)
		{
			AudioSource.PlayClipAtPoint(punchSe, new Vector3(0, 0, 0));
			hitcar--;
			Debug.Log(hitcar);
			//return;
		}
		else
		{
			breakCar();
		}
	}

	public void breakCar()
	{
		// brokenPrefab�𐶐�
		Transform brokentransform = Instantiate(brokenPrefab, transform.position, transform.rotation);
		brokentransform.localScale = transform.localScale;

		AudioSource.PlayClipAtPoint(breakSe, new Vector3(0, 0, 0));

		// ���̃I�u�W�F�N�g���폜
		Destroy(gameObject);
		// ��莞�Ԍ��brokenPrefab���폜
		Destroy(brokentransform.gameObject, 5f);

		if (player1Controller.GetWeapon() == gameObject && (gameObject.tag == "Item" || gameObject.tag == "Item2" ||
			 gameObject.tag == "Item3" || gameObject.tag == "Item4" ||
			 gameObject.tag == "Item5" || gameObject.tag == "Item6"))
		{
			player1Controller?.Drop();

		}

		if (player2Controller.GetWeapon() == gameObject && (gameObject.tag == "Item" || gameObject.tag == "Item2" ||
			 gameObject.tag == "Item3" || gameObject.tag == "Item4" ||
			 gameObject.tag == "Item5" || gameObject.tag == "Item6"))
		{
			player2Controller?.Drop();

		}

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

	private bool IsPlayer1HoldingItem()
	{
		if (player1 == null)
		{
			return false;
		}
		return player1Controller != null && player1Controller.IsHoldingItem;
	}

	private bool IsPlayer2HoldingItem()
	{
		if (player2 == null)
		{
			return false;
		}

		return player2Controller != null && player2Controller.IsHoldingItem;
	}

	private void IsAnim1(bool status)
	{
		if (player1 == null) return;

		if (player1Controller != null)
		{
			player1Controller.isAnimator = status;
			player1Controller.Drop();
		}
	}

	private void IsAnim2(bool status)
	{
		if (player2 == null) return;

		if (player2Controller != null)
		{
			player2Controller.isAnimator = status;
			player2Controller.Drop();
		}
	}

}