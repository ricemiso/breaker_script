using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CountTimer : MonoBehaviour
{
	TextMeshProUGUI CountText;
	float countdown = 4f;
	int count;

	// Start is called before the first frame update
	void Start()
	{
		CountText = GameObject.Find("CountDown").GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update()
	{
		if(countdown >= 0)
		{
			countdown -= Time.deltaTime;
			count = (int)countdown;
			CountText.text = count.ToString();
		}
		if (countdown <= 1)
		{
			CountText.text = "FIGHT!";
		}
		if (countdown <= 0)
		{
			CountText.text = "";
		}

	}
}
