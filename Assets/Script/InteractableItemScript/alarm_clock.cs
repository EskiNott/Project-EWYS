using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alarm_clock : InteractableItem
{
    [SerializeField] private Transform SecondHandTrans;
    [SerializeField] private AudioSource AlarmClockAudioSource;
    private float RotateTiming;

    private void Start()
    {
        RotateTiming = -99999;
        StartCoroutine(WaitForBackgroundMusic(0.5f));
    }

    protected override void Update()
    {
        base.Update();
        RotateTiming += Time.deltaTime;
        SecondHandRotate();
    }

    private void SecondHandRotate()
    {
        if(RotateTiming >= 1)
        {
            RotateTiming = 0;
            SecondHandTrans.Rotate(new Vector3(0, 0, 6));
        }
    }

    IEnumerator WaitForBackgroundMusic(float time)
    {
        yield return new WaitForSeconds(time);
        AlarmClockAudioSource.Play();
        RotateTiming = 1;
    }

    public override void OnInteract()
    {
        base.OnInteract();
        FlowManager.Instance.ChangeDay();
        IsInteractable = false;
    }
}
