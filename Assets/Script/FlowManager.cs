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
    [SerializeField] private Texture[] DeskMaterial;
    [SerializeField] private Texture[] ShelfMaterial;
    [SerializeField] private GameObject[] Desk;
    [SerializeField] private GameObject Shelf;
    public alarm_clock clock;
    public AudioPlayer LightButtonClick;

    private bool BlackScreen;

    public bool StartGame;
    public int loopTime;
    public int Day;

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
        BlackScreen = true;
        if (Day <= 7)
        {
            NormallyChangeDay();
        }
        else
        {
            StartCoroutine(EndPanelWait(5));
        }

    }

    public void NormallyChangeDay()
    {
        StartCoroutine(ChangeDayEvent(5));
        StartCoroutine(BlackScreenClose(7));
        StartCoroutine(OpenTheLight(9));
    }

    IEnumerator EndPanelWait(float Time)
    {
        yield return new WaitForSeconds(Time);
        UIManager.Instance.EndPanel.SetPanelActive(true);
        UIManager.Instance.EndPanel.SetChildActive(1, true);
        if(loopTime > 0)
        {
            UIManager.Instance.EndPanel.SetChildActive(2, true);
        }
    }

    IEnumerator ChangeDayEvent(float Time)
    {
        yield return new WaitForSeconds(Time);
        if(Day > 1)
        {
            GameObjectsInit();
        }
        Shelf.GetComponent<Renderer>().material.mainTexture = ShelfMaterial[Day - 1];
        foreach(GameObject i in Desk)
        {
            i.GetComponent<Renderer>().material.mainTexture = DeskMaterial[Day - 1];
        }
        for (int i = 0; i < Day; i++)
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
                Charms[i - 1].SetActive(true);
            }
            else
            {
                ShuffledCharms[i - 1].SetActive(true);
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
        foreach (GameObject i in Letters)
        {
            i.GetComponent<InteractableItem>().IsInteractable = true;
        }
        foreach (GameObject i in Charms)
        {
            InteractableItem II = i.GetComponent<InteractableItem>();
            if (II != null)
            {
                II.IsInteractable = true;
            }
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
        else if (!BlackScreen && BlackScreenCanvasGroup.alpha <= 0.01f && BlackScreenCanvasGroup.gameObject.activeSelf)
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
        foreach (GameObject i in Letters)
        {
            i.GetComponent<InteractableItem>().IsInteractable = true;
        }
        foreach (GameObject i in Charms)
        {
            InteractableItem II = i.GetComponent<InteractableItem>();
            if (II != null)
            {
                II.IsInteractable = true;
            }
        }
        LightButtonClick.audioSource.Play();
    }

    public void GameObjectsInit()
    {
        foreach(GameObject i in BookShelf)
        {
            i.SetActive(false);
        }
        foreach (GameObject i in Letters)
        {
            i.SetActive(false);
            i.GetComponent<InteractableItem>().IsInteractable = false;
        }
        foreach (GameObject i in Charms)
        {
            i.SetActive(false);
            InteractableItem II = i.GetComponent<InteractableItem>();
            if(II != null)
            {
                II.IsInteractable = false;
            }
        }
        foreach(GameObject i in Lights)
        {
            i.SetActive(false);
        }
    }
}
