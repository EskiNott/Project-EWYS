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
                        if(outline != null)
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
                Debug.Log(RayResult[0].gameObject.name);
            }
        }
    }
}
