using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class letter : InteractableItem
{
    public override void OnInteract()
    {
        base.OnInteract();
        FocusManager.Instance.CheckTextItemObject(transform, 1);
    }
}
