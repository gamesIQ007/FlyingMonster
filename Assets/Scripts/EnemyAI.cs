using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Enemy))]

public class EnemyAI : MonoBehaviour
{
    public enum AIBehaviour
    {
        Null,
        Idle,
        PursuitTarget
    }


    [SerializeField] protected AIBehaviour aIBehaviour;
    public AIBehaviour Behaviour { get { return aIBehaviour; } set { aIBehaviour = value; } }

    [SerializeField] protected GameObject target;

    protected Enemy enemy;


    #region Unity Events

    protected virtual void Start()
    {
        enemy = GetComponent<Enemy>();

        StartBehaviour(aIBehaviour);

        enemy.ChangeHitPoints.AddListener(OnChangeHitPoints);
    }

    protected virtual void OnDestroy()
    {
        enemy.ChangeHitPoints.RemoveListener(OnChangeHitPoints);
    }

    // Исправить на Update? Враги перемещаются в билде слишком медленно, если перемещение в Update.
    protected void FixedUpdate()
    {
        if (aIBehaviour == AIBehaviour.Null)
        {
            return;
        }

        UpdateAI();
    }

    #endregion


    protected virtual void UpdateAI()
    {
        if (aIBehaviour == AIBehaviour.Idle)
        {
            return;
        }

        if (aIBehaviour == AIBehaviour.PursuitTarget)
        {
            if (target == null) return;

            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget < enemy.BestDistanceMin)
            {
                enemy.MoveAway(target);
            }
            if (distanceToTarget > enemy.BestDistanceMax)
            {
                enemy.MoveTo(target);
            }

            if (enemy.Type == EnemyType.Shooter && distanceToTarget > enemy.MeleeAttackDistance && distanceToTarget <= enemy.ShootAttackDistance)
            {
                enemy.AttackDistanceWeapon(target.transform.position);
            }

            enemy.ActivatePerk();
        }
    }


    #region Поведение

    protected void StartBehaviour(AIBehaviour state)
    {
        if (enemy.IsDead) return;

        aIBehaviour = state;
    }

    protected IEnumerator SetBehaviourOnTime(AIBehaviour state, float second)
    {
        AIBehaviour previous = aIBehaviour;
        aIBehaviour = state;
        StartBehaviour(aIBehaviour);

        yield return new WaitForSeconds(second);

        StartBehaviour(previous);
    }

    #endregion


    private void OnChangeHitPoints(int damage, Vector2 position)
    {
        if (enemy.MaxHitPoints == enemy.HitPoints) return;
        
        StartBehaviour(AIBehaviour.PursuitTarget);
    }
}
