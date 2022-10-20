using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alarm_clock : InteractableItem
{
    [SerializeField] private Transform SecondHandTrans;
    private float RotateTiming;

    private void Start()
    {
        RotateTiming = -99999;
        StartCoroutine(WaitForBackgroundMusic(0.5f)); ;
    }

    private void Update()
    {
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
        RotateTiming = 0;
    }


}
