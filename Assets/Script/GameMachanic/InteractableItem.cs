using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public bool IsInteractable;
    private void Start()
    {
        gameObject.tag = "InteractableItem";
    }

    public virtual void OnInteract() { }
}
