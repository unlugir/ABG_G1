using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Stats : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public float energy;
    [SerializeField] public bool isAlive = true;
    [SerializeField] public UnityEvent onDeath;
    [SerializeField] Slider healthSlider;
    private void Start() 
    {
        healthSlider.value = health;
    }
    public void TakeDamage(float damage)
    {
        if (!isAlive) return;
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            isAlive = false;
            healthSlider.gameObject.SetActive(false);
            onDeath.Invoke();
        }
        StartCoroutine(SmoothSlide(health, 0.2f));
    }
    
    public void DestroyAfter(float time)
    {
        Destroy(this.gameObject, time);
    }


    private IEnumerator SmoothSlide(float value, float time)
    {
        float currentTime = 0;
        float startvalue = healthSlider.value;
        while (currentTime <= time)
        {
            healthSlider.value = Mathf.Lerp(startvalue, value, currentTime/time);
            currentTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        healthSlider.value = value;
    }
}
