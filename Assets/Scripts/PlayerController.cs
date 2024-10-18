using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float controlSpeed = 30f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 5f;

    [SerializeField] float posPitchFactor = -5f;
    [SerializeField] float ctrlPitchFactor = -10f;

    [SerializeField] float posYawFactor = -2f;
    [SerializeField] float ctrlRollFactor = 5;


    float xThrow;
    float yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    void ProcessRotation()
    {
        float picthDeuToPos = transform.localPosition.y * posPitchFactor;
        float pitchDeuToCtrlThrow = yThrow * ctrlPitchFactor;

        float pitch = picthDeuToPos + pitchDeuToCtrlThrow;
        float yaw = transform.localPosition.x * posYawFactor;   

        float roll = xThrow * ctrlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);


        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
