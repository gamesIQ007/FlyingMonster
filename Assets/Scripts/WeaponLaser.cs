using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class WeaponLaser : MonoBehaviour
{
    [SerializeField] private int damage;
    public int Damage => damage;

    [SerializeField] private float duration;
    [SerializeField] private float refireTime;
    
    private float refireTimer;

    private LineRenderer line;

    private Enemy enemy;

    public bool CanFire => refireTimer <= 0;


    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (refireTimer > 0)
        {
            refireTimer -= Time.deltaTime;
        }

        if (line != null)
        {
            line.SetPosition(0, enemy.transform.position);
        }
    }


    public void Fire(Vector2 endPos)
    {
        if (CanFire == false) return;

        refireTimer = refireTime;

        line = new GameObject("Line").AddComponent<LineRenderer>();
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.positionCount = 2;
        line.useWorldSpace = true;
        line.SetPosition(0, enemy.transform.position);
        line.SetPosition(1, endPos);
        Destroy(line.gameObject, duration);
    }


    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
}
