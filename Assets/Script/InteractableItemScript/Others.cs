using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Others : InteractableItem
{
    [SerializeField] AudioPlayer audioPlayer;
    public override void OnInteract()
    {
        base.OnInteract();
        audioPlayer.audioSource.Play();
    }
}
