using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeControl : MonoBehaviour
{
    private bool canSwitch;
    private Volume myVolume;
    private Vignette myVignette;
    private float currentVelocity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        myVolume = GetComponent<Volume>();
        myVolume.profile.TryGet(out myVignette);
        myVignette.intensity.value = 0.7f;
    }

    private void FixedUpdate()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        if (canSwitch)
        {
            myVignette.intensity.value = Mathf.SmoothDamp(myVignette.intensity.value, 0.4f, ref currentVelocity, 0.5f);
            Invoke("RoundedSwitch", 0.8f);
        }
    }
    private void RoundedSwitch()
    {
        myVignette.rounded.value = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canSwitch = true;
        }
    }
}

