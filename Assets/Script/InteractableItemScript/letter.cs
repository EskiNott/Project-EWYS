using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class letter : InteractableItem
{
    [SerializeField] private AudioPlayer audioPlayer;
    public override void OnInteract()
    {
        base.OnInteract();
        FocusManager.Instance.CheckTextItemObject(transform, 1f);
        FlowManager.Instance.clock.IsInteractable = true;
        audioPlayer.audioSource.Play();
    }
}
