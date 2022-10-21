using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public bool IsInteractable;
    public Outline outline;
    public bool DisableOutline;
    private void Start()
    {
        gameObject.tag = "InteractableItem";
    }
    protected virtual void Update()
    {
        outline.enabled = false;
    }
    public virtual void OnInteract() { }
}
