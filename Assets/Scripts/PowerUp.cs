using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Health Settings")]
    public bool healthPowerUp = false;
    public int healthAmount = 1;
    [Header("Ammo Settings")]
    public bool ammoPowerUp = false;
    public int ammoAmount = 5;
    [Header("Transform Settings")]
    [SerializeField] private float turnSpeed = -1f;
    [Header("Scale Settings")]
    [SerializeField] private float period = 2f;  // iki dalga boyu arasındaki mesafe
    [SerializeField] Vector3 scaleVector;
    [SerializeField] private float scaleFactor;
    private Vector3 startScale;
 
    void Start()
    {
        //büyüklüğümüzü tutar
        startScale = transform.localScale;
        if (healthPowerUp && ammoPowerUp)
        {
            healthPowerUp = false;
            ammoPowerUp= false;
        }
        else if (healthPowerUp)
        {
            ammoPowerUp= false;
        }
        else if (ammoPowerUp)
        {
            healthPowerUp= false;
        }
    }

    void Update()
    {
        transform.Rotate(0f, turnSpeed, 0f);
        SinusWave();
    }

    private void SinusWave()
    {
        if (period< 0f)
        {
            period = 0.1f;
        }
        float cycles = Time.timeSinceLevelLoad / period;
        const float piX2 = Mathf.PI * 2; //bu değer oyun boyunca değişmez çünkü const
        float sinusWave = Mathf.Sin(cycles * piX2);
        scaleFactor = sinusWave / 2 + 0.5f;
        Vector3 offset = scaleFactor * scaleVector;
        transform.localScale = startScale + offset;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;

       if (healthPowerUp)
        {
            other.gameObject.GetComponent<Target>().GetHealth += healthAmount;
        }
       else if (ammoPowerUp)
        {
            other.gameObject.GetComponent<Attack>().GetAmmo+= ammoAmount;
        }
        Destroy(gameObject);
    }
}