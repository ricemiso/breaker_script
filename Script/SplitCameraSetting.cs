using UnityEngine;
using System.Collections;

public class SplitCameraSetting : MonoBehaviour
{

	//�@�J�����̕������@
	public enum SplitCameraMode
	{
		horizontal,
		vertical,
		square
	};

	public SplitCameraMode mode;    //�@�J�����̕������@

	//�@�v���C���[���ʂ����ꂼ��̃J����
	public Camera player1Camera;
	public Camera player2Camera;

	// Use this for initialization
	void Start()
	{
		//�@�Q�v���C���[�p�̉�����
		if (mode == SplitCameraMode.horizontal)
		{
			//�@�J������ViewPortRect�̕ύX
			player1Camera.rect = new Rect(0f, 0f, 0.5f, 1f);
			player2Camera.rect = new Rect(0.5f, 0f, 0.5f, 1f);

			//�@�Q�v���C���[�p�̏c����
		}
		else if (mode == SplitCameraMode.vertical)
		{
			//�@�J������ViewPortRect�̕ύX
			player1Camera.rect = new Rect(0f, 0.5f, 1f, 0.5f);
			player2Camera.rect = new Rect(0f, 0f, 1f, 0.5f);

			//�@�S�v���C���[�p��4����
		}
		else if (mode == SplitCameraMode.square)
		{
			//�@�J������ViewPortRect�̕ύX
			player1Camera.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
			player2Camera.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);

		}
	}
}