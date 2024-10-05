using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PanelChange : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject subPanel;
    public GameObject subPanel2;
    public GameObject finishPanel;

    public bool isDpadPressed = false;

    private enum PanelState
    {
        Main,
        Sub,
        Sub2,
        Finish
    }

    private PanelState currentState;

    void Start()
    {
        currentState = PanelState.Main;
        UpdatePanelState();
    }

    public void MainView()
    {
        currentState = PanelState.Main;
        UpdatePanelState();
    }

    public void SubView()
    {
        currentState = PanelState.Sub;
        UpdatePanelState();
    }

    public void SubView2()
    {
        currentState = PanelState.Sub2;
        UpdatePanelState();
    }

    public void FinishView()
    {
        currentState = PanelState.Finish;
        UpdatePanelState();
    }

    void UpdatePanelState()
    {
        mainPanel.SetActive(currentState == PanelState.Main);
        subPanel.SetActive(currentState == PanelState.Sub);
        subPanel2.SetActive(currentState == PanelState.Sub2);
        finishPanel.SetActive(currentState == PanelState.Finish);
    }

    void Update()
    {
        float xValue = Gamepad.current.dpad.ReadValue().x;

        // [D]キーを押す
        if (xValue > 0.5f && !isDpadPressed)
        {
            isDpadPressed = true;

            if (currentState == PanelState.Main)
            {
                SubView();
            }
            else if (currentState == PanelState.Sub)
            {
                SubView2();
            }
            else if (currentState == PanelState.Sub2)
            {
                FinishView();
            }
        }

        // [A]キーを押す
        else if (xValue < -0.5f && !isDpadPressed)
        {
            isDpadPressed = true;

            if (currentState == PanelState.Finish)
            {
                SubView2();
            }
            else if (currentState == PanelState.Sub2)
            {
                SubView();
            }
            if (currentState == PanelState.Sub)
            {
                MainView();
            }
        }

        if (xValue == 0.0f)
        {
            isDpadPressed = false;
        }
    }
}

