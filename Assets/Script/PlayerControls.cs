using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    [Header("General Setup Settings")]
    [Tooltip("How fast ships moves up and down based on input")]
        [SerializeField] private float controlSpeed = 20f;
    [Tooltip("How far the ship can move left and right on the screen")]
        [SerializeField] private float xRange = 7f;
    [Tooltip("How far the ship can move up and down on the screen")]
        [SerializeField] private float yRange = 5f;
    
    [Header("Plane Rotation")]
    [Tooltip("How the pitch changes based on the planes current location")]
        [SerializeField] private float positionPitchFactor = -2f;
    [Tooltip("How the pitch changes based on the players movement")]
        [SerializeField] private float controlPitchFactor = -10f;
    [Tooltip("How much the yaw changes based on the planes position")]
        [SerializeField] private float positionYawFactor = 2f;
    [Tooltip("How muh the plane rolls based on the players movement")]
        [SerializeField] private float controlRollFactor = -20f;

    [Header("Weapons")]
    [SerializeField] private GameObject[] lasers;

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
        ProcessFiring();
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

    private void ProcessRotation()
    {
        //Affects Pitch of Player's Plane
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition  + pitchDueToControlThrow;

        //Affects Yaw of the Player's Plane
        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yaw = yawDueToPosition;

        //Affects Roll of the Player's Plane
        float rollDueToControlThrow = xThrow * controlRollFactor;
        float roll = rollDueToControlThrow;



        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring()
    {

        if (Input.GetButton("Fire1"))
        {
            SetLaserActive(true);
        }
        else
        {
            SetLaserActive(false);
        }
    }

    private void SetLaserActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
