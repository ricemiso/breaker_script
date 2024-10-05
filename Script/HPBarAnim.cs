using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarAnim : MonoBehaviour
{
	RawImage rawImage;
	Rect uvRect;

	//const float maxHPBar = 0.475f;
	const float maxHPBar = 0.5f;
	//const float maxHP = 100;
	const float maxHP = 100;
	

	[SerializeField] public float currentHPBar;
	//���݂̃_���[�W�̃o�[�̒���
	[SerializeField] float currentDamageBar = 0f;
	//�ڎw���_���[�W�̃o�[�̒���
	[SerializeField] public float nextDamageBar;
	//�o�[���ړ�����X�s�[�h
	[SerializeField] float speed = 2f;

	void Start()
	{
		rawImage = GetComponent<RawImage>();
		rawImage.uvRect = new Rect(0.005f, 0f, 0.49f, 1f);

		currentHPBar = maxHPBar / maxHP;
	}

	public void Update()
	{
		rawImage.uvRect = new Rect(currentDamageBar, 0f, 0.49f, 1f);

		// Mathf.Lerp��currentDamageBar����nextDamageBar�Ɉړ�������
		currentDamageBar = Mathf.Lerp(currentDamageBar, nextDamageBar,
			speed * Time.deltaTime);
	}

	public void Damage(float dam)
	{
		nextDamageBar += currentHPBar * dam;

		// nextDamageBar��0�����ɂȂ�Ȃ��悤�ɐ��䂷��
		if (nextDamageBar < 0)
		{
			nextDamageBar = 0;
		}

		// nextDamageBar��maxHPBar�𒴂��Ȃ��悤�ɐ��䂷��
		if (nextDamageBar >= maxHPBar)
		{
			nextDamageBar = 50;
		}
	}
}