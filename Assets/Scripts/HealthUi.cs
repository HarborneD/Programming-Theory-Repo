using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUi : MonoBehaviour
{
    [SerializeField] bool shouldFaceCamera = true;
    [SerializeField] Camera cameraToLookAt;
    [SerializeField] Image healthBar;
    [SerializeField] Image shieldBar;


    private void Start()
    {
        cameraToLookAt = Camera.main;
        if(shieldBar == null)
        {
            UpdateShields(0, 1);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shouldFaceCamera)
        {
            transform.LookAt(transform.position + cameraToLookAt.transform.forward);
        }
    }

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (maxHealth == 0)
        {
            healthBar.transform.localScale = new Vector3(0 / 1, 0.5f, 1);
        }
        else
        {
            healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, 0.5f, 1);
        }
    }

    public void UpdateShields(float currentShield, float maxShield)
    {
        if (maxShield == 0)
        {
            shieldBar.transform.localScale = new Vector3(0 / 1, 0.5f, 1);
        }
        else
        {
            shieldBar.transform.localScale = new Vector3(currentShield / maxShield, 0.5f, 1);
        }
        
    }
}
