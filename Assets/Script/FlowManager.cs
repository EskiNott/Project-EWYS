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
    [SerializeField] private List<GameObject> ShuffledCharms;
    public alarm_clock clock;
    public AudioPlayer LightButtonClick;

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
        BlackScreenCanvasGroup.alpha = 0;
        originalRotationVector = new(6.4f, -93.3f, 0);
        startRotationVector = new(6.4f, -74.7f, 0);
        MainCam.m_Lens.FieldOfView = 25;
        GameObjectsInit();
        ShuffledCharms = GameManager.ShuffleList<GameObject>(new List<GameObject>(Charms));
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
        if(Day <= 7)
        {
            BlackScreen = true;
            StartCoroutine(ChangeDayEvent(5));
            StartCoroutine(BlackScreenClose(7));
            StartCoroutine(OpenTheLight(8));
        }
        else
        {

        }

    }
    IEnumerator ChangeDayEvent(float Time)
    {
        yield return new WaitForSeconds(Time);
        for(int i = 0; i < Day; i++)
        {
            Letters[i].SetActive(true);
        }
        for (int i = 0; i < Day*6; i++)
        {
            BookShelf[i].SetActive(true);
        }
        for (int i = 1; i < Day; i++)
        {
            if(loopTime == 0)
            {
                Charms[i].SetActive(true);
            }
            else
            {
                ShuffledCharms[i].SetActive(true);
            }
        }
        BlackScreen = false;
    }

    IEnumerator OpenTheLight(float Time)
    {
        yield return new WaitForSeconds(Time);
        foreach (GameObject i in Lights)
        {
            i.SetActive(true);
        }
        LightButtonClick.audioSource.Play();
    }

    private void BlackScreenManage()
    {
        int target = BlackScreen ? 1 : 0;
        BlackScreenCanvasGroup.alpha = Mathf.Lerp(BlackScreenCanvasGroup.alpha, target, Time.deltaTime * 2);
        if (BlackScreen && !BlackScreenCanvasGroup.gameObject.activeSelf)
        {
            BlackScreenCanvasGroup.gameObject.SetActive(true);
        } 
        else if (BlackScreen && BlackScreenCanvasGroup.alpha <= 0.01f && BlackScreenCanvasGroup.gameObject.activeSelf)
        {
            BlackScreenCanvasGroup.alpha = 0;
            BlackScreenCanvasGroup.gameObject.SetActive(false);
        }

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
        StartCoroutine(ChangeDayEvent(0));
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
        foreach(GameObject i in Lights)
        {
            i.SetActive(false);
        }
    }
}
