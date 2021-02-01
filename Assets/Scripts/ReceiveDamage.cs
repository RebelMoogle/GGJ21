using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum HealthState
{
    High,
    Low,
    Knockout
}

public class ReceiveDamage : MonoBehaviour
{


    [SerializeField]
    private int startHealth = 100;
    
    [Range(0.0f, 1.0f)]
    public const float LowHealth = 0.25f;

    public class DamageEvent : UnityEvent<HealthState> {}
    public DamageEvent damageEvent;

    private int currentHealth = 100;

    public int CurrentHealth { get => currentHealth; }
    public int StartHealth {get => startHealth; }

    private void Awake() {
        if (damageEvent == null) {
            damageEvent = new DamageEvent();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int receiveDamage(int amount){
        
       currentHealth -= amount;

       if(currentHealth <= 0) {
           // health 0 event event
           currentHealth = 0;
       }
       damageEvent.Invoke(this.GetHealthState());

       return currentHealth;
    }

    public HealthState GetHealthState() {
        if (currentHealth <= 0){
            return HealthState.Knockout;
        }

        return (float)currentHealth / (float)startHealth < LowHealth ? HealthState.Low : HealthState.High;
    }

}
