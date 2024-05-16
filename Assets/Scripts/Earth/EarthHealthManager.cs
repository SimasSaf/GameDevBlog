using System.Collections.Generic;
using UnityEngine;

public class EarthHealthManager : MonoBehaviour, IEarthHealthManagerOR
{
    public int maxHealth = 10;
    private int currentHealth;
    private List<IHealthObserver> observers = new List<IHealthObserver>();

    void Start()
    {
        ResetHealth();
        NotifyOnDamageTaken(currentHealth, maxHealth);
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        currentHealth -= 1;
        NotifyOnDamageTaken(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            NotifyOnFatalDamageTaken();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Earth triggered with Enemy");
            TakeDamage();
        }
    }

    public void ApplyTemporaryHealthUpgrade(int upgradeAmount)
    {
        currentHealth += upgradeAmount;
    }

    public void RegisterObserver(IHealthObserver observer)
    {
        observers.Add(observer);
    }

    public void UnregisterObserver(IHealthObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyOnDamageTaken(int currentHealth, int maxHealth)
    {
        foreach (IHealthObserver observer in observers)
        {
            observer.OnDamageTaken(currentHealth, maxHealth);
        }
    }

    private void NotifyOnFatalDamageTaken()
    {
        foreach (IHealthObserver observer in observers)
        {
            observer.OnFatalDamageTaken();
        }
    }
}
