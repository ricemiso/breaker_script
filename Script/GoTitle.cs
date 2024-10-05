using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GoTitle : MonoBehaviour
{
	public KeyCode key = KeyCode.Return;
	public TextMeshProUGUI text;

	private bool isWaiting = false;
	private bool canInput = false;

	private void Start()
	{
		text.enabled = false;

		// If the TextMeshProUGUI component is not set, get it from the parent GameObject
		if (text == null)
		{
			text = GetComponent<TextMeshProUGUI>();
		}

		if (text == null)
		{
			Debug.LogError("TextMeshProUGUI is not assigned and not found on the GameObject.");
			return;
		}

		// Disable input for 5 seconds
		StartCoroutine(DisableInputTemporarily());
	}

	private IEnumerator DisableInputTemporarily()
	{
		canInput = false;
		yield return new WaitForSeconds(5f); // Disable input for 5 seconds
		text.enabled = true;
		canInput = true;
	}


	public void Activate()
	{
		SceneManager.LoadScene("Title");
	}

	void Update()
	{
		if (canInput &&Input.anyKey)
		{
			Activate();
		}
	}
}
