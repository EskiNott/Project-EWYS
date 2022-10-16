using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FocusManager : MonoSingleton<FocusManager>
{
    private bool isFocusing;
    [SerializeField] private Transform CameraTrans;
    private Transform TextItemTrans;
    private FocusingItem TextItemFocusingItem;
    public float ZoomSensitivity = 1.0f;
    public float MoveSensitivity = 1.0f;
    private float mouseCenter;
    private bool isDragging;

    [SerializeField] private GameObject TextPrefab;
    private void Start()
    {
        isFocusing = false;
        isDragging = false;
    }

    private void Update()
    {
        FocusOnObject();
        if (Input.GetKeyDown(KeyCode.T))
        {
            CheckTextItemObject(TextPrefab, 1.0f);
        }
    }

    public void CheckTextItemObject(GameObject Prefab, float Distance)
    {
        Vector3 SpawnPos = CameraTrans.forward * Distance;
        GameObject Item = Instantiate(Prefab, SpawnPos, Quaternion.identity);
        TextItemTrans = Item.transform;
        TextItemFocusingItem = Item.GetComponent<FocusingItem>();
        TextItemTrans.LookAt(CameraTrans);
        isFocusing = true;
    }

    private void FocusOnObject()
    {
        if(isFocusing && TextItemTrans != null && TextItemFocusingItem != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isFocusing = false;
                Destroy(TextItemTrans.gameObject);
                TextItemTrans = null;
                TextItemFocusingItem = null;
                return;
            }

            mouseCenter = Input.GetAxis("Mouse ScrollWheel");
            if(mouseCenter > 0) //Zoom out
            {
                if(Vector3.Distance(TextItemTrans.position,CameraTrans.position) > TextItemFocusingItem.MinFocusDistance)
                {
                    TextItemTrans.position += -CameraTrans.forward * ZoomSensitivity * 0.05f;
                }
            }
            else if (mouseCenter < 0) //Zoom in
            {
                if (Vector3.Distance(TextItemTrans.position, CameraTrans.position) < TextItemFocusingItem.MaxFocusDistance)
                {
                    TextItemTrans.position -= -CameraTrans.forward * ZoomSensitivity * 0.05f;
                }
            }

            if (Input.GetMouseButtonDown(2))
            {
                isDragging = true;
                Debug.Log("Start Draging");
            }
            else if (Input.GetMouseButtonUp(2))
            {
                Debug.Log("Stop Draging");
                isDragging = false;
            }
            else if (Input.GetMouseButton(2))
            {
                if (isDragging)
                {
                    Debug.Log("dragging!");
                    float mouseX = Input.GetAxis("Mouse X");
                    float mouseY = Input.GetAxis("Mouse Y");
                    if(mouseX != 0 || mouseY != 0)
                    {
                        Vector3 NormalX = Vector3.Cross(TextItemTrans.up, CameraTrans.forward).normalized;
                        Vector3 NormalY = Vector3.Cross(TextItemTrans.right, CameraTrans.forward).normalized;
                        TextItemTrans.position += NormalX * mouseX * 0.01f * MoveSensitivity;
                        TextItemTrans.position += NormalY * mouseY * 0.01f * MoveSensitivity;
                    }
                }
            }
        }
    }
}
