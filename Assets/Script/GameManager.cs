using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class GameManager : MonoSingleton<GameManager>
{
    private List<RaycastResult> RayResult;
    private bool gamePause;
    private void Start()
    {
        gamePause = false;
        RayResult = new();
    }

    private void LateUpdate()
    {
        RaycastManager();
    }

    public void GamePause(bool Pause)
    {
        gamePause = Pause;
    }

    private void RaycastManager()
    {
        if(FlowManager.Instance.StartGame && !gamePause && !FocusManager.Instance.isFocusing)
        {
            RaycastHit hit = RaycastPhysical();
            if (hit.collider != null)
            {
                GameObject go = hit.collider.gameObject;
                if (go.CompareTag("InteractableItem"))
                {
                    InteractableItem II = go.GetComponent<InteractableItem>();
                    Outline outline = go.GetComponent<Outline>();
                    if (II.IsInteractable)
                    {
                        if(outline != null && !II.DisableOutline)
                        {
                            outline.enabled = true;
                        }
                        if (Input.GetMouseButtonDown(0))
                        {
                            II.OnInteract();
                        }
                    }
                }
            }
        }

    }

    public static RaycastHit RaycastPhysical()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast((Ray)ray, out hit);
        return hit;
    }

    private void UIPointAtControl()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) { position = Input.mousePosition }, RayResult);
            if (RayResult.Count > 0)
            {
                
            }
        }
    }

    /// <summary>
    /// 将List乱序
    /// </summary>
    /// <typeparam name="T">List类型</typeparam>
    /// <param name="originList">原List</param>
    /// <returns>乱序后的一个新List</returns>
    public static List<T> ShuffleList<T>(List<T> originList)
    {
        List<T> _list = new List<T>();
        foreach (var item in originList)
        {
            _list.Insert(UnityEngine.Random.Range(0, _list.Count), item);
        }
        return _list;
    }

    public static bool IsChildHasParent(Transform child, Transform endPoint, Transform target)
    {
        bool found = false;
        Transform test = child;
        if (test == target)
        {
            found = true;
        }
        else
        {
            while (test != null && endPoint != null && test != endPoint)
            {
                if (test == target)
                {
                    found = true;
                    break;
                }
                else
                {
                    test = test.parent;
                }
            }
        }
        return found;
    }
}
