using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private int maxHealth = 2;
    private int currentHealth;
    //property denen şeyi kullandık ne olduğunu araştır! Property'ler inspector'da görünmez
    public int GetHealth
    {
        get {return currentHealth;}

        set 
        {
            currentHealth = value;
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }
    void Start()
    {
        currentHealth = maxHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.gameObject.GetComponent<Bullet>();
        
        if (bullet) // bullet null değilse yani true ise / bullet ==true da diyebiliriz
        {
            //bullet null değilse ve owner'ı player değilse
            if (bullet && bullet.owner != gameObject)
            {
                currentHealth--;

                if (currentHealth <= 0)
                {
                    Die();
                }
                Destroy(other.gameObject);
            }
        }
    }
    private void Die()
    {       
        Destroy(gameObject);
    }
}
