using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Character : Destructible
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        currentHitPoints = maxHitPoints;
        ChangeHitPoints?.Invoke(0, Vector2.zero);
    }

    private void FixedUpdate()
    {
        UpdateRigitBody();
    }


    public override void ApplyDamage(int damage)
    {
        if (indestructible) return;
    }


    public Vector2 MovementControl { get; set; }

    private void UpdateRigitBody()
    {
        rb.velocity = new Vector2(MovementControl.x * speed, MovementControl.y * speed);
    }
}
