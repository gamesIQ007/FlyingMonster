using UnityEngine;


public enum EnemyType
{
    Melee,
    Shooter
}


public class Enemy : Destructible
{
    [SerializeField] private EnemyType type;
    public EnemyType Type => type;

    [SerializeField] protected float movementSpeed;

    [SerializeField] private float meleeAttackDistance;
    public float MeleeAttackDistance => meleeAttackDistance;

    [SerializeField] private float shootAttackDistance;
    public float ShootAttackDistance => shootAttackDistance;

    [SerializeField] private float detectDistance;
    public float DetectDistance => detectDistance;

    [SerializeField] private float bestDistanceMin;
    public float BestDistanceMin => bestDistanceMin;
    
    [SerializeField] private float bestDistanceMax;
    public float BestDistanceMax => bestDistanceMax;

    [SerializeField] private WeaponLaser weapon;
    public WeaponLaser Weapon => weapon;

    protected bool isDead;
    public bool IsDead => isDead;

    protected Vector2 movementDirection = Vector2.zero;

    private Rigidbody2D rb;

    private PerkCreateIllusion perk;


    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();

        perk = GetComponent<PerkCreateIllusion>();
    }


    private Vector2 MovementDirectionTo(GameObject go)
    {
        return (go.transform.position - transform.position).normalized;
    }


    public void MoveTo(GameObject go)
    {
        rb.velocity += MovementDirectionTo(go) * movementSpeed;
    }

    public void MoveAway(GameObject go)
    {
        rb.velocity -= MovementDirectionTo(go) * movementSpeed;
    }

    public virtual void AttackDistanceWeapon(Vector3 attackPosition)
    {
        weapon.Fire(attackPosition);
    }

    public void ActivatePerk()
    {
        if (perk.enabled && perk.PerkCanBeActivated)
        {
            perk.ActivatePerk();
        }
    }

    private void OnMouseDown()
    {
        ApplyDamage(1000);
    }
}
