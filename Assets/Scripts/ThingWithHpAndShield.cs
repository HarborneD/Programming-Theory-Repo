using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class ThingWithHpAndShield : ThingWithHp
{
    [SerializeField]
    private int _maxShield;

    // ENCAPSULATION
    public int maxShield { get; protected set; }

    // ENCAPSULATION
    public int currentShield { get; protected set; }

    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        maxShield = _maxShield;
        currentShield = maxShield;
        healthUi.UpdateShields(currentShield, maxShield);
    }


    public override void TakeDamage(int damage)
    {
        int shieldDamage = Mathf.Min(damage, currentShield);
        int hpDamage = damage - shieldDamage;

        currentShield -= shieldDamage;
        healthUi.UpdateShields(currentShield, maxShield);

        if (hpDamage > 0)
        {
            base.TakeDamage(hpDamage);
        }
    }
}
