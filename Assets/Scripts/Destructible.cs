using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Destructible : MonoBehaviour
{
    [SerializeField] protected bool indestructible;
    public bool IsIndestructible => indestructible;

    [SerializeField] protected int maxHitPoints;
    public int MaxHitPoints => maxHitPoints;

    protected int currentHitPoints;
    public int HitPoints => currentHitPoints;

    [SerializeField] protected UnityEvent eventOnDeath;
    public UnityEvent EventOnDeath => eventOnDeath;

    public UnityEvent<int, Vector2> ChangeHitPoints;


    protected virtual void Start()
    {
        currentHitPoints = maxHitPoints;
        ChangeHitPoints?.Invoke(0, Vector2.zero);
    }


    public virtual void ApplyDamage(int damage)
    {
        if (indestructible) return;

        currentHitPoints -= damage;

        ChangeHitPoints?.Invoke(damage, transform.position);

        if (currentHitPoints <= 0)
        {
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {
        Destroy(gameObject);
        eventOnDeath?.Invoke();
    }

    public void AddHitPoints(int addHitPoints)
    {
        currentHitPoints += addHitPoints;
        if (currentHitPoints > maxHitPoints)
        {
            currentHitPoints = maxHitPoints;
        }
        ChangeHitPoints?.Invoke(0, Vector2.zero);
    }

    public void IndestructibleOn()
    {
        indestructible = true;
    }
    public void IndestructibleOff()
    {
        indestructible = false;
    }

    private static HashSet<Destructible> allDestructibles;
    public static IReadOnlyCollection<Destructible> AllDestructibles => allDestructibles;

    protected virtual void OnEnable()
    {
        if (allDestructibles == null)
        {
            allDestructibles = new HashSet<Destructible>();
        }

        allDestructibles.Add(this);
    }

    protected virtual void OnDestroy()
    {
        allDestructibles.Remove(this);
    }
}
