using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    [SerializeField] private bool PanelActive;
    [SerializeField] public CanvasGroup canvasGroup;
    public GameObject[] Childs;
    public float changeSpeed = 1.0f;

    public bool GetPanelActiveStat()
    {
        return PanelActive;
    }

    public void SetPanelActive(bool active)
    {
        PanelActive = active;
    }
    public void SetChildActive(int index, bool Active)
    {
        Childs[index].SetActive(Active);
    }

}
