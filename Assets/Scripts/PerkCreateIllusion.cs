using UnityEngine;

public class PerkCreateIllusion : MonoBehaviour
{
    [SerializeField] private Enemy prefab;
    [SerializeField] private int count;
    [SerializeField] private float recharge;

    private float rechargeTimer = 0;
    private int activeIllusionsCount = 0;

    public bool PerkCanBeActivated => rechargeTimer <= 0;


    private void Update()
    {
        if (activeIllusionsCount == 0)
        {
            rechargeTimer -= Time.deltaTime;
        }
    }

    public void ActivatePerk()
    {
        if (rechargeTimer <= 0)
        {
            for (int i = 0; i < count; i++)
            {
                Enemy illusion = Instantiate(prefab, transform.position, Quaternion.identity);
                illusion.GetComponent<WeaponLaser>().SetDamage(prefab.GetComponent<WeaponLaser>().Damage / 2);
                illusion.GetComponentInChildren<SpriteRenderer>().color = Color.grey;
                illusion.GetComponent<PerkCreateIllusion>().enabled = false;

                illusion.EventOnDeath.AddListener(OnIllusionDeath);

                activeIllusionsCount++;
            }

            rechargeTimer = recharge;
        }
    }

    private void OnIllusionDeath()
    {
        activeIllusionsCount--;
    }
}
