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
	//現在のダメージのバーの長さ
	[SerializeField] float currentDamageBar = 0f;
	//目指すダメージのバーの長さ
	[SerializeField] public float nextDamageBar;
	//バーが移動するスピード
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

		// Mathf.LerpでcurrentDamageBarからnextDamageBarに移動させる
		currentDamageBar = Mathf.Lerp(currentDamageBar, nextDamageBar,
			speed * Time.deltaTime);
	}

	public void Damage(float dam)
	{
		nextDamageBar += currentHPBar * dam;

		// nextDamageBarが0未満にならないように制御する
		if (nextDamageBar < 0)
		{
			nextDamageBar = 0;
		}

		// nextDamageBarがmaxHPBarを超えないように制御する
		if (nextDamageBar >= maxHPBar)
		{
			nextDamageBar = 50;
		}
	}
}