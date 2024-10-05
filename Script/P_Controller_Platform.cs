using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class P_Controller_Platform : MonoBehaviour
{
	public Transform equipPosition;
	public float distance = 5f;
	public GameObject obj;
	public RuntimeAnimatorController defaultAnimator;
	public RuntimeAnimatorController weaponAnimator1;
	public RuntimeAnimatorController weaponAnimator2;
	public RuntimeAnimatorController weaponAnimator3;
	public CarBreak br;
	P2_Controller_Platform player2Controller;
	public GameObject player2;  // Player2オブジェクトを参照

	public HPBarAnim hpBar;
	public AudioClip se;
	public AudioClip pickUpSe;
	public AudioClip throwHitSe;

	private GameObject currentWeapon;
	private GameObject throwWeapon;
	private GameObject wp;
	private bool cannotGrab;
	public float knockbackForce = 30f;
	public bool isJump;
	public bool isGround = true;
	public float hitPoint = 100;
	public bool isAnimator;
	private Camera trackingCamera;
	private bool isHoldingItem; // 持っているかどうかのフラグ
	public bool isThrow;
	public bool isParth;
	public bool isShow; //武器を持っているか
	public bool isCrouch;
	public bool isDownInput;

	private Animator anim;
	private CharacterController cCon;
	private Vector3 velocity;
	private Collider handRCollider;
	private Collider handLCollider;
	public GameObject SpecialCollider;
	private Quaternion rot;
	private int clickCount;
	private float timer;
	private bool isTimer;
	public bool isAttack = false;
	public bool isAttack1 = false;
	public bool isAttack2 = false;
	public bool isAttack3 = false;
	public Vector3 direction;
	public bool isSpecialCollider = false;
	public bool isAnim = false;

	public bool Special;
	public GameObject specialWave; //必殺技のやつ
	float specialTimer = 15.0f;

	private string detectedItemTag;

	private float blockStartTime;
	public bool isBlocking;
	private bool blockInputDisabled;
	private float countdown = 3f;
	private int count;
	private float downInputTime = 0f;
	private bool canInput = true;
	private bool isDamaged = false;
	public bool isweakdamage = false;
	public bool isstrongdamage = false;
	public bool onBuilding = false;

	private Vector3 initialWeaponPosition;
	private Quaternion initialWeaponRotation;

	private bool isDodgeCooldown;

	[Header("Rotation speed")]
	public float speed_rot = 10;

	[Header("Movement speed during jump")]
	public float speed_move = 10;

	[Header("Time available for combo")]
	public int term = 10;

	//todo:変数名変更
	private int punch = 5;
	private int car = 15;
	private int car1 = 20;
	private int car2 = 25;
	private int car3 = 30;
	private int lamp = 10;
	private int prop = 10;
	private int giveDamage;

	private void Start()
	{
		anim = GetComponent<Animator>();

		cCon = GetComponent<CharacterController>();
		velocity = Vector3.zero;
		//isCrouch = false;
		isAnimator = false;

		//手のコライダーを取得
		handRCollider = GameObject.Find("hand1RCollide").GetComponent<SphereCollider>();
		handLCollider = GameObject.Find("hand1LCollide").GetComponent<SphereCollider>();
		//SpecialCollider = GameObject.Find("Specialcollider").GetComponent<GameObject>();

		SpecialCollider.SetActive(false);

		specialWave.SetActive(false);

		hpBar = GameObject.FindObjectOfType<HPBarAnim>();

		trackingCamera = GameObject.Find("Player1_Camera").GetComponent<Camera>();
		isThrow = false;
		isParth = false;

		br = GameObject.FindObjectOfType<CarBreak>();

		if (player2 == null)
		{
			player2 = GameObject.FindWithTag("Player2");
		}
		player2Controller = player2.GetComponent<P2_Controller_Platform>();

		Special = false;
	}

	public bool IsHoldingItem // プロパティを追加
	{
		get { return isHoldingItem; }
	}

	public GameObject GetWeapon()
	{
		return currentWeapon;
	}

	public GameObject GetThrowWeapon()
	{
		return throwWeapon;
	}

	private void Update()
	{


		if (countdown >= 0)
		{
			countdown -= Time.deltaTime;
			count = (int)countdown;
			return;
		}

		if (hitPoint <= 0 || transform.position.y <= -60)
		{
			StartCoroutine(WaitAndLoadScene());
			return;
		}

		if (isDamaged)
		{
			return;
		}

		if (!canInput)
		{
			return;
		}

		if (Mathf.Approximately(Time.timeScale, 0f))
		{
			return;
		}

		CheckWeapons();

		if (cannotGrab && Input.GetKeyDown("joystick 1 button 2") && !isJump)
		{
			if (currentWeapon != null)
			{
				Drop();
				isAnimator = !isAnimator;
			}
			PickUp();
		}

		if (currentWeapon != null && Input.GetKeyDown("joystick 1 button 0") && !isJump)
		{
			isAnimator = !isAnimator;
			Drop();

		}

		Rotate();
		if (!isJump)
		{
			Attack();
			Dodge();

			Block();
			Crouch();
			Skill1();
			//Skill2();
			Skill3();
			//Skill4();
			//Skill5();
			//Skill6();
			//Skill7();
			//Skill8();
			if (isBlocking)
			{
				return;
			}

			Jump();


		}

		if (isAnimator)
		{
			if (currentWeapon != null && currentWeapon.CompareTag("Item"))
			{
				anim.runtimeAnimatorController = weaponAnimator2;
				isAnim = false;
			}
			else if (currentWeapon != null && currentWeapon.CompareTag("Item6"))
			{
				anim.runtimeAnimatorController = weaponAnimator3;
				isAnim = false;
			}
			else
			{
				anim.runtimeAnimatorController = weaponAnimator1;
				isAnim = false;
			}
		}
		else
		{
			anim.runtimeAnimatorController = defaultAnimator;
			isAnim = true;
		}
	}

	IEnumerator WaitAndLoadScene()
	{
		Skill4();
		yield return new WaitForSeconds(3.0f);

		SceneManager.LoadScene("Player2Win");
	}


	private void CheckWeapons()
	{
		RaycastHit hit;
		if (Physics.Raycast(obj.transform.position, obj.transform.forward, out hit, distance)
			|| Physics.Raycast(obj.transform.position, -obj.transform.forward, out hit, distance)
			|| Physics.Raycast(obj.transform.position, obj.transform.right, out hit, distance)
			|| Physics.Raycast(obj.transform.position, -obj.transform.right, out hit, distance))
		{
			if ((hit.transform.CompareTag("Item") || hit.transform.CompareTag("Item2") ||
				hit.transform.CompareTag("Item3") || hit.transform.CompareTag("Item4") || hit.transform.CompareTag("Item5") || hit.transform.CompareTag("Item6")) && hit.transform.gameObject != player2Controller.GetWeapon())
			{
				isParth = true;
				cannotGrab = true;
				wp = hit.transform.gameObject;
				detectedItemTag = hit.transform.tag;
			}
			else
			{
				isParth = false;
				cannotGrab = false;
			}

			if (hit.transform.CompareTag("Building"))
			{
				onBuilding = true;
			}
			else
			{
				onBuilding = false;
			}
		}
		else
		{
			isParth = false;
			cannotGrab = false;
			onBuilding = false;
		}
	}

	private void PickUp()
	{
		if (wp == null)
		{
			//Debug.LogWarning("No weapon to pick up.");
			isShow = false;
			return;
		}

		currentWeapon = wp;
		throwWeapon = wp;


		isShow = true;
		// 武器のスケールをリセットしてから一回り小さくする
		//currentWeapon.transform.localScale = new Vector3(1f, 1f, 1f) * 0.7f; // 元のスケールに戻してから80%に縮小

		// 武器の位置と向きを設定
		currentWeapon.transform.position = equipPosition.position; // Y軸方向に0.2のオフセット
		currentWeapon.transform.SetParent(equipPosition);
		currentWeapon.transform.localEulerAngles = new Vector3(0, -30, 70); // Y軸方向に90度回転

		Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();

		if (rb != null)
		{
			rb.isKinematic = true;
		}
		BoxCollider bc = currentWeapon.GetComponent<BoxCollider>();
		if (bc != null)
		{
			bc.enabled = false;
		}
		//Debug.Log("Picked up: " + currentWeapon.name);

		// 手のコライダーを無効にする
		if (handRCollider != null)
		{
			handRCollider.enabled = false;
		}

		if (handLCollider != null)
		{
			handLCollider.enabled = false;
		}

		ChangeHandTag("Untagged"); // タグを変更

		isAnimator = true;
		isHoldingItem = true; // フラグを設定

		AudioSource.PlayClipAtPoint(pickUpSe, new Vector3(0, 0, 0));
	}

	public void ChangeHandTag(string newTag)
	{
		var handObjects = GetComponentsInChildren<Transform>();
		foreach (var hand in handObjects)
		{
			if (hand.CompareTag("Hand_P1"))
			{
				hand.tag = newTag;
			}
			else if (hand.CompareTag("Untagged"))
			{
				hand.tag = newTag;
			}
		}
	}

	public void Drop()
	{
		if (currentWeapon == null)
		{
			//Debug.LogWarning("No weapon to drop.");
			return;
		}

		currentWeapon.transform.SetParent(null);
		isAnimator = false;

		//// 手のコライダーを有効にする
		//if (handRCollider != null)
		//{
		//    handRCollider.enabled = true;
		//}

		//if (handLCollider != null)
		//{
		//    handLCollider.enabled = true;
		//}

		ChangeHandTag("Hand_P1"); // タグを元に戻す

		Rigidbody rb = currentWeapon.GetComponent<Rigidbody>();
		if (rb != null)
		{
			rb.isKinematic = false;
		}
		BoxCollider bc = currentWeapon.GetComponent<BoxCollider>();
		if (bc != null)
		{
			bc.enabled = true;
		}
		//Debug.Log("Dropped: " + currentWeapon.name);
		currentWeapon = null;

		isHoldingItem = false; // フラグを解除
		isShow = false;
	}



	void Rotate()
	{


		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical") * -1.0f;
		Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput);

		if (inputDirection.magnitude < 0.1f)
		{
			anim.SetBool("Run", false);
			//anim.SetBool("Walk", false);
			return;
		}

		// 下方向の入力がある場合は移動アニメーションを停止
		if (verticalInput < 0)
		{
			// 下向き入力が開始されていない場合
			if (!isDownInput)
			{
				downInputTime = Time.time; // 現在の時間を記録
				isDownInput = true; // 下向き入力フラグをtrueに設定
			}
			else
			{
				// 下向き入力が既に開始されている場合、1秒以上経過しているか確認
				if (Time.time - downInputTime >= 1f)
				{
					// 1秒以上押されていたら、isDownInputをtrueのままにする
				}
			}
		}
		else
		{
			// 下向き入力がされていない場合
			isDownInput = false;
		}

		// 入力がある場合の移動処理
		if (inputDirection.magnitude > 0f)
		{
			// 移動アニメーションを実行
			if (!canInput)
			{
				return;
			}
			Move();

			// カメラの方向に応じてプレイヤーのローカル空間での移動方向を計算
			Quaternion camRotation = Quaternion.Euler(0f, trackingCamera.transform.eulerAngles.y, 0f);
			Vector3 moveDirection = camRotation * inputDirection;

			// プレイヤーの移動方向に応じて角度を設定
			Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

			// 移動速度に応じてスムーズに回転
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
		}
		else
		{
			// 入力がない場合はアニメーションを停止
			anim.SetBool("Run", false);
			//anim.SetBool("Walk", false);

		}
	}


	void Move()
	{


		// 地面のレイヤーマスクを設定（必要に応じて変更）
		int groundLayerMask = LayerMask.GetMask("Ground");

		// 地面との接触を確認
		RaycastHit hit;
		bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1.0f, groundLayerMask);

		// 地面のレイヤーマスクを設定（必要に応じて変更）
		if (isJump)
		{

			transform.position += transform.forward * speed_move * Time.deltaTime;

			if (!isGround)
			{
				transform.Translate(Vector3.down * 3.0f * Time.deltaTime);
			}

			anim.SetBool("Run", false);
			//anim.SetBool("Walk", false);
		}
		else
		{
			if (Special == true)
			{
				transform.position += transform.forward * speed_move * Time.deltaTime * 1.1f;
				//Debug.Log("aaa2");

				// キャラクターの高さを修正
				if (!isGrounded)
				{
					Vector3 newPosition = transform.position;
					newPosition.y = hit.point.y;
					transform.position = newPosition;
				}
			}
			else if (currentWeapon != null && (currentWeapon.transform.CompareTag("Item2") || currentWeapon.transform.CompareTag("Item3")))
			{
				transform.position += transform.forward * speed_move * Time.deltaTime * 0.3f;
			}
			//else if (currentWeapon != null && (currentWeapon.transform.CompareTag("Item4") || currentWeapon.transform.CompareTag("Item5")))
			//{
			//	transform.position += transform.forward * speed_move * Time.deltaTime * 0.7f;
			//	//Debug.Log("wakewakaran");
			//}

			anim.SetBool("Run", true);
			//anim.SetBool("Walk", true);
		}

		// 高さを強制的に0に戻す
		if (!isJump && transform.position.y > 0)
		{
			Vector3 newPosition = transform.position;
			newPosition.y = 0;
			transform.position = newPosition;
		}
	}

	void Attack()
	{
		if (isTimer)
		{
			timer += Time.deltaTime;
		}


		if (Input.GetKeyDown("joystick 1 button 1")/* || Input.GetKeyDown(KeyCode.O)*/)
		{
			isAttack = true;

			//一定時間後にコライダーの機能をオフにする
			Invoke("ColliderReset", 0.3f);

			switch (clickCount)
			{

				case 0:

					anim.SetTrigger("Attack1");
					handLCollider.enabled = true;

					isTimer = true;
					isAttack1 = true;
					isAttack2 = false;
					isAttack3 = false;
					clickCount++;
					break;


				case 1:

					if (timer <= term)
					{
						anim.SetTrigger("Attack2");
						handRCollider.enabled = true;
						isAttack1 = false;
						isAttack2 = true;
						isAttack3 = false;
						clickCount++;
					}


					else
					{
						anim.SetTrigger("Attack1");
						handLCollider.enabled = true;
						isAttack1 = true;
						isAttack2 = false;
						isAttack3 = false;
						clickCount = 1;
					}


					timer = 0;
					break;


				case 2:

					if (timer <= term)
					{
						anim.SetTrigger("Attack3");
						handLCollider.enabled = true;
						isAttack1 = false;
						isAttack2 = false;
						isAttack3 = true;
						clickCount = 0;

						isTimer = false;
					}


					else
					{
						anim.SetTrigger("Attack1");
						handLCollider.enabled = true;
						isAttack1 = true;
						isAttack2 = false;
						isAttack3 = false;
						clickCount = 1;
					}

					timer = 0;
					break;
			}

		}
	}

	public void ColliderReset()
	{
		handRCollider.enabled = false;
		handLCollider.enabled = false;
		isAttack = false;
	}

	void Dodge()
	{
		float lTriInput = Input.GetAxis("LTrigger");

		if (onBuilding)
		{
			return;
		}

		if (isDodgeCooldown)
		{
			return; // クールダウン中は何もしない
		}



		if (lTriInput >= 0.6f)
		{
			anim.SetTrigger("Dodge");
			gameObject.transform.position += transform.forward * 5.0f;
			StartCoroutine(DodgeCooldownCoroutine());
		}
	}

	private IEnumerator DodgeCooldownCoroutine()
	{
		isDodgeCooldown = true; // クールダウン開始
		yield return new WaitForSeconds(1.5f); // 5秒待機
		isDodgeCooldown = false; // クールダウン終了
	}

	void Block()
	{
		if (blockInputDisabled || Special)
		{
			UnityEngine.Debug.Log("Block input disabled or Special active");
			return;
		}

		if (/*Input.GetMouseButtonDown(1) || */Input.GetKeyDown("joystick 1 button 4"))
		{
			blockStartTime = Time.time;
			anim.SetBool("Block", true);
			isBlocking = true;
			isstrongdamage = false;
		}

		if (/*Input.GetMouseButtonUp(1) || */Input.GetKeyUp("joystick 1 button 4"))
		{
			anim.SetBool("Block", false);
			isBlocking = false;
			isweakdamage = false;
		}

		UnityEngine.Debug.Log("isBlocking: " + isBlocking + ", Time: " + (Time.time - blockStartTime) + ", isstrongdamage: " + isstrongdamage);

		if (isBlocking && Time.time - blockStartTime >= 2f && !isstrongdamage)
		{
			StartCoroutine(DisableBlockInput());
		}
	}


	public IEnumerator DisableBlockInput()
	{
		blockInputDisabled = true;
		anim.SetBool("Block", false);
		isBlocking = false;
		isweakdamage = false;
		yield return new WaitForSeconds(10f); // 10秒後に再度Blockを有効にする
		blockInputDisabled = false;
		isstrongdamage = false;
		blockStartTime = Time.time;
	}

	void Crouch()
	{
		if (/*Input.GetKeyDown(KeyCode.LeftControl) || */Input.GetKeyDown("joystick 1 button 8"))
		{
			isCrouch = true;
			anim.SetBool("Crouch", true);
		}
		if (/*Input.GetKeyUp(KeyCode.LeftControl) || */Input.GetKeyUp("joystick 1 button 8"))
		{
			isCrouch = false;
			anim.SetBool("Crouch", false);
		}
	}


	void Jump()
	{
		if (Input.GetKeyUp("joystick 1 button 3") && isGround)
		{
			anim.SetBool("Block", false);
			anim.SetBool("Crouch", false);
			anim.SetTrigger("Jump");

			isJump = true;

			StartCoroutine(StopJumpCoroutine(0.833f));

		}
	}

	IEnumerator StopJumpCoroutine(float duration)
	{
		yield return new WaitForSeconds(duration);

		isJump = false;
	}


	void JumpEnd()
	{
		isJump = false;
	}

	// Skill1
	void Skill1()
	{
		float rTriInput = Input.GetAxis("RTrigger");

		if (Special == true && specialWave.active == false)
		{
			//Debug.Log("スキル発動");
			specialWave.SetActive(true);
		}

		if (specialWave.active == true)
		{
			specialTimer -= Time.deltaTime;

			if (rTriInput >= 0.6f)
			{
				Skill6();
				isSpecialCollider = true;
				SpecialCollider.SetActive(true);
				specialTimer = 0;
			}

			if (specialTimer <= 0.0f)
			{
				Special = false;
				specialWave.SetActive(false);
				isSpecialCollider = false;
				specialTimer = 15.0f;
			}
		}
	}

	// Skill2
	void Skill2()
	{
		anim.SetTrigger("Skill2");
	}

	// Skill3 投げ
	void Skill3()
	{
		if (Input.GetKeyUp("joystick 1 button 5") && !isAttack)
		{
			if (currentWeapon != null && (currentWeapon.transform.CompareTag("Item") || currentWeapon.transform.CompareTag("Item2") ||
				currentWeapon.transform.CompareTag("Item3") || currentWeapon.transform.CompareTag("Item4") || currentWeapon.transform.CompareTag("Item5") || currentWeapon.transform.CompareTag("Item6")))
			{
				isThrow = true;
				anim.SetTrigger("Skill3");
				GameObject weaponToDrop = currentWeapon;
				StartCoroutine(DelayedDropAndKnockback(weaponToDrop, trackingCamera, 0.18f)); // 0.1秒の遅延を入れる
				StartCoroutine(ResetIsThrowAfterDelay(5f)); // 5秒後に isThrow を false にする
			}
		}
	}

	IEnumerator ResetIsThrowAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		isThrow = false;
		throwWeapon = null;
	}

	private IEnumerator DelayedDropAndKnockback(GameObject weaponToDrop, Camera came, float delay)
	{
		yield return new WaitForSeconds(delay);
		Drop(); // まず武器をドロップする
		KnockbackCar(came, weaponToDrop); // ドロップされた武器を飛ばす
	}

	public void KnockbackCar(Camera came, GameObject weapon)
	{
		Vector3 knockbackDirection = came.transform.forward + Vector3.up * 0.3f;

		Rigidbody rb = weapon.GetComponent<Rigidbody>();

		if (rb != null)
		{
			// 武器の物理挙動を有効にする
			rb.isKinematic = false;

			// 武器を投げる方向に力を加える
			rb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode.Impulse); // 正規化して力を加える
		}
		else
		{
			//Debug.LogWarning("Rigidbodyがアタッチされていません: " + weapon.name);
		}


	}

	// Skill4
	void Skill4()
	{
		anim.SetTrigger("Skill4");
	}

	// Skill5 死ぬアニメーション
	void Skill5()
	{
		anim.SetTrigger("Skill5");
	}

	// Skill6 スペシャルスキル
	void Skill6()
	{
		anim.SetTrigger("Skill6");
		StartCoroutine(WaitBeforeMove(1.0f));
	}

	IEnumerator WaitBeforeMove(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		SpecialCollider.SetActive(false);
		isSpecialCollider = false;
	}

	// Skill7 弱ダメージ
	public void Skill7()
	{
		anim.SetTrigger("Skill7");
		StartCoroutine(StopInputCoroutine(0.5f));
	}

	// Skill8 強ダメージ
	void Skill8()
	{
		anim.SetTrigger("Skill8");
		StartCoroutine(StopInputCoroutine(2f)); // 2秒間入力を停止するコルーチンを開始

	}

	IEnumerator StopInputCoroutine(float duration)
	{
		isDamaged = true;
		canInput = false; // 入力を停止
		yield return new WaitForSeconds(duration); // 指定された時間（2秒間）待機
		canInput = true; // 入力を再開
		isDamaged = false;
	}

	IEnumerator StopInputCoroutine2(float duration)
	{
		canInput = false; // 入力を停止
		yield return new WaitForSeconds(duration); // 指定された時間（2秒間）待機
		canInput = true; // 入力を再開
	}

	void OnTriggerEnter(Collider other)
	{

		UnityEngine.Debug.Log(gameObject.tag + "  " + other.gameObject.tag);



		if (isBlocking)
		{
			isweakdamage = true;
			return;
		}

		if (other.gameObject.CompareTag("Special_P2"))
		{
			Skill8();
			hitPoint -= 50; //todo:要変更
			hpBar.Damage(50.0f);
		}

		switch (other.gameObject.tag)
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

		if (other.gameObject.CompareTag("Hand_P2"))
		{
			hitPoint -= punch;
			hpBar.Damage(punch);
			Skill7();
			AudioSource.PlayClipAtPoint(se, new Vector3(0, 5, 0));

			// 手からこのオブジェクトへの水平方向の方向を計算します
			Vector3 directionToHand = transform.position - other.gameObject.transform.position;
			directionToHand.y = 0; // 垂直成分を無視する
			directionToHand.Normalize(); // 正規化して単位ベクトルにする

			// 水平方向に小さなオフセットを適用して新しい位置を計算します
			float pushDistance = 2.0f; // 必要に応じて距離を調整してください
			Vector3 newPosition = transform.position + directionToHand * pushDistance;

			// 新しい位置をこのオブジェクトのTransformに適用します
			transform.position = newPosition;

			//Debug.Log("新しい位置: " + transform.position);

		}

		// 衝突したオブジェクトが手でないかつ、Player1Controllerが投げている状態で、かつ衝突したオブジェクトが "Item" または "Item2" タグである場合
		if (player2Controller.isThrow && other.gameObject == player2Controller.GetThrowWeapon()
			&& (other.gameObject.CompareTag("Item") || other.gameObject.CompareTag("Item2") || other.gameObject.CompareTag("Item3")
			|| other.gameObject.CompareTag("Item4") || other.gameObject.CompareTag("Item5") || other.gameObject.CompareTag("Item6")))
		{
			//// ダメージを与える
			//if (br != null) // br が null でないことを確認する
			//{
			//	br.breakCar();
			//}
			AudioSource.PlayClipAtPoint(throwHitSe, new Vector3(0, 0, 0));

			Vector3 damageDirection = (transform.position - other.transform.position).normalized;

			//Debug.Log(damageDirection + "衝突");

			// XY平面上の角度を計算
			float angle = Mathf.Atan2(damageDirection.y, damageDirection.x) * Mathf.Rad2Deg;

			// 角度を0〜360度の範囲に変換
			if (angle < 0)
			{
				angle += 360;
			}

			//Debug.Log(angle + "度の方向からの衝突");

			// 角度に基づいて方向を設定
			if (angle >= 315 || angle < 45)
			{
				damageDirection.x = 0;
			}
			else if (angle >= 45 && angle < 135)
			{
				damageDirection.x = 90;
			}
			else if (angle >= 135 && angle < 225)
			{
				damageDirection.x = 180;
			}
			else if (angle >= 225 && angle < 315)
			{
				damageDirection.x = 270;
			}

			// Y軸の回転を無視
			damageDirection.y = 0;

			//Debug.Log(damageDirection + " 修正後の方向");

			Skill8();
			hitPoint -= giveDamage; // 仮にダメージ量をcarに設定しています
			hpBar.Damage(giveDamage); // 仮にダメージ量をcarに設定しています
									  //Debug.Log(giveDamage);
									  //Debug.Log("接触");
			player2Controller.isThrow = false;
		}
	}

	IEnumerator RotatePlayerTowardsDirection(Vector3 direction)
	{
		Quaternion targetRotation = Quaternion.LookRotation(direction);
		while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
		{
			// 第3引数を 0 から 1 の間に調整
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 100); // 2 は回転速度の調整用
			yield return null;
		}

		// targetRotation の eulerAngles を反転させる
		Quaternion flippedRotation = targetRotation * Quaternion.Euler(0, 180, 0);
		transform.rotation = flippedRotation;

	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.CompareTag("DamageZone"))
		{
			hitPoint -= 0.1f;
			hpBar.Damage(0.1f);
		}
	}
}
