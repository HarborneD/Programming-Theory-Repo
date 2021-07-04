using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingWithHp : MonoBehaviour
{
    [SerializeField]
    protected int _maxHp;
    public int maxHp { get; protected set; }

    public int currentHp { get; protected set; }
    
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        maxHp = _maxHp;
        currentHp = maxHp;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHp = Mathf.Max(0, currentHp - damage);
        if(currentHp <= 0)
        {
            Handle0Hp();
        }
    }

    public virtual void Handle0Hp()
    {
        Destroy(gameObject);
    }
}
