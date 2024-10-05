using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour
{
    public Button whiteButton;
    public Button white1Button;
    public Button redButton;
    public Button blueButton;
    public RawImage rawImage4;

    private bool redButtonVisible = false;
    private bool blueButtonVisible = false;

    private bool isRedButtonPressed = false;
    private bool isBlueButtonPressed = false;

    void Start()
    {
        whiteButton.onClick.AddListener(OnWhiteButtonClick);
        white1Button.onClick.AddListener(OnWhite1ButtonClick);

        // 最初に白と白1のボタンを表示し、赤と青のボタンを非表示にする
        whiteButton.gameObject.SetActive(true);
        white1Button.gameObject.SetActive(true);
        redButton.gameObject.SetActive(false);
        blueButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown("joystick 1 button 1"))
        {
            OnWhiteButtonClick();
        }

        if (Input.GetKeyDown("joystick 2 button 1"))
        {
            OnWhite1ButtonClick();
        }

        if (Input.GetKeyDown("joystick 1 button 0"))
        {
            OnWhiteButtonClick2();
        }

        if (Input.GetKeyDown("joystick 2 button 0"))
        {
            OnWhite1ButtonClick2();
        }

        //
        if (isRedButtonPressed && isBlueButtonPressed)
        {
            // メインシーンに移動
            SceneManager.LoadScene("Main");
        }

        // Aキーが押された時に、ResetButtons() 関数が呼ばれて元の状態に戻る
        if (Input.GetKeyDown(KeyCode.A))
        {
            ResetButtons();
        }
    }

    public void OnWhiteButtonClick()
    {
        // 白ボタンがクリックされたときに赤ボタンを表示
        redButton.gameObject.SetActive(true);
        redButtonVisible = true;

        isRedButtonPressed = true;

        // もし青ボタンが表示されていたら、赤ボタンも表示
        if (blueButtonVisible)
        {
            blueButton.gameObject.SetActive(true);
        }
        else
        {
            blueButton.gameObject.SetActive(false);
        }
    }

    public void OnWhite1ButtonClick()
    {
        // 白1ボタンがクリックされたときに青ボタンを表示
        blueButton.gameObject.SetActive(true);
        blueButtonVisible = true;

        isBlueButtonPressed = true;

        // もし赤ボタンが表示されていたら、青ボタンも表示
        if (redButtonVisible)
        {
            redButton.gameObject.SetActive(true);
        }
        else
        {
            redButton.gameObject.SetActive(false);
        }
    }

    public void OnWhiteButtonClick2()
    {
        // 白ボタンがクリックされたときに赤ボタンを表示
        redButton.gameObject.SetActive(false);
        redButtonVisible = false;

        isRedButtonPressed = false;

        // もし青ボタンが表示されていたら、赤ボタンも表示
        if (blueButtonVisible)
        {
            blueButton.gameObject.SetActive(true);
        }
        else
        {
            blueButton.gameObject.SetActive(false);
        }
    }

    public void OnWhite1ButtonClick2()
    {
        // 白1ボタンがクリックされたときに青ボタンを表示
        blueButton.gameObject.SetActive(false);
        blueButtonVisible = false;

        isBlueButtonPressed = false;

        // もし赤ボタンが表示されていたら、青ボタンも表示
        if (redButtonVisible)
        {
            redButton.gameObject.SetActive(true);
        }
        else
        {
            redButton.gameObject.SetActive(false);
        }
    }


    private void ResetButtons()
    {
        // 現在表示されているボタンを元の状態に戻す
        redButton.gameObject.SetActive(redButtonVisible);
        blueButton.gameObject.SetActive(blueButtonVisible);
    }
}