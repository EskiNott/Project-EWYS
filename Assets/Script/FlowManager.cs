using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoSingleton<FlowManager>
{
    public Transform MainCamTrans;
    public Cinemachine.CinemachineVirtualCamera MainCam;
    private Vector3 originalRotationVector;
    private Vector3 startRotationVector;
    [SerializeField] private GameObject[] BookShelf;
    [SerializeField] private GameObject[] Letters;
    [SerializeField] private GameObject[] Charms;
    [SerializeField] private GameObject[] Lights;
    [SerializeField] private CanvasGroup StartMenuCanvasGroup;
    [SerializeField] private CanvasGroup BlackScreenCanvasGroup;

    private bool BlackScreen;

    public bool StartGame;
    private int loopTime;
    private int Day;

    // Start is called before the first frame update
    void Start()
    {
        loopTime = 0;
        Day = 1;
        StartGame = false;
        BlackScreen = false;
        originalRotationVector = new(6.4f, -93.3f, 0);
        startRotationVector = new(6.4f, -74.7f, 0);
        MainCam.m_Lens.FieldOfView = 25;
        GameObjectsInit();
    }

    // Update is called once per frame
    void Update()
    {
        StartFOVChange();
        StartCanvasGroupChange();
        BlackScreenManage();
    }

    public void ChangeDay()
    {
        
        Day++;
        StartCoroutine(ChangeDayEvent(5));
        StartCoroutine(BlackScreenClose(7));
    }
    IEnumerator ChangeDayEvent(float Time)
    {
        switch (Day)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
        }
        yield return new WaitForSeconds(Time);
        BlackScreen = false;
    }

    private void BlackScreenManage()
    {
        int target = BlackScreen ? 1 : 0;
        BlackScreenCanvasGroup.alpha = Mathf.Lerp(BlackScreenCanvasGroup.alpha, target, Time.deltaTime * 0.2f);
    }

    IEnumerator BlackScreenClose(float Time)
    {
        yield return new WaitForSeconds(Time);
        BlackScreen = false;
    }

    private void StartFOVChange()
    {
        if(MainCam.m_Lens.FieldOfView < 60)
        {
            if (StartGame)
            {
                MainCamTrans.rotation = Quaternion.Lerp(MainCamTrans.rotation, Quaternion.Euler(originalRotationVector), Time.deltaTime);
                MainCam.m_Lens.FieldOfView = Mathf.Lerp(MainCam.m_Lens.FieldOfView, 60, Time.deltaTime);
                if(MainCam.m_Lens.FieldOfView >= 59.9f)
                {
                    MainCam.m_Lens.FieldOfView = 60;
                    MainCamTrans.rotation = Quaternion.Euler(originalRotationVector);
                }
            }
        }

    }

    private void StartCanvasGroupChange()
    {
        if (StartMenuCanvasGroup.gameObject.activeSelf) 
        { 
            if (StartGame)
            {
                StartMenuCanvasGroup.alpha = Mathf.Lerp(StartMenuCanvasGroup.alpha, 0, Time.deltaTime);
                if(StartMenuCanvasGroup.alpha <= 0.01f)
                {
                    StartMenuCanvasGroup.gameObject.SetActive(false);
                }
            }
        }
    }

    public void StartOnClick()
    {
        StartGame = true;
        foreach(GameObject i in Lights)
        {
            i.SetActive(true);
        }
    }

    private void GameObjectsInit()
    {
        foreach(GameObject i in BookShelf)
        {
            i.SetActive(false);
        }
        foreach (GameObject i in Letters)
        {
            i.SetActive(false);
        }
        foreach (GameObject i in Charms)
        {
            i.SetActive(false);
        }
    }
}
