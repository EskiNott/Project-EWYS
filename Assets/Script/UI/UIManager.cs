using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public UIPanel PausePanel;

    private void Update()
    {
        PausePanelActiveControl();
        KeyControl();
    }

    private void KeyControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPanelActive(PausePanel, !PausePanel.GetPanelActiveStat());
            GameManager.Instance.GamePause(PausePanel.GetPanelActiveStat());
        }
    }

    private void PausePanelActiveControl()
    {
        int target;
        if (PausePanel.gameObject.activeSelf)
        {
            target = PausePanel.GetPanelActiveStat() ? 1 : 0;
            PausePanel.canvasGroup.alpha = Mathf.Lerp(PausePanel.canvasGroup.alpha, target, Time.deltaTime * PausePanel.changeSpeed);
            if (PausePanel.canvasGroup.alpha < 0.01f && !PausePanel.GetPanelActiveStat())
            {
                PausePanel.gameObject.SetActive(false);
            }
        }
    }

    public void PausePanel_Settings_OnClick()
    {
        PausePanel.SetChildActive(0, false);
        PausePanel.SetChildActive(1, false);
        PausePanel.SetChildActive(2, true);
    }

    public void PausePanel_Settings_Return_OnClick()
    {
        PausePanel.SetChildActive(0, true);
        PausePanel.SetChildActive(1, true);
        PausePanel.SetChildActive(2, false);
    }

    public void PausePanel_Continue_OnClick()
    {
        SetPanelActive(PausePanel, false);
        GameManager.Instance.GamePause(false);
    }

    public void SetPanelActive(UIPanel Panel, bool active)
    {
        if (active)
        {
            if (!Panel.gameObject.activeSelf)
            {
                Panel.gameObject.SetActive(true);
            }
            Panel.SetPanelActive(true);
        }
        else
        {
            Panel.SetPanelActive(false);
        }
    }

}
