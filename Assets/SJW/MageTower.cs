using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    [Header("���� Ÿ�� ���� ����")]
    [SerializeField] private GameObject magicPrefab;  // ���� ����ü ������
    [SerializeField] private Transform firePoint;     // �߻� ��ġ
    
    private Coroutine fireCoroutine;
    private YieldInstruction fireDelay;

    private float attackCooldown = 0f;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    private IEnumerator FireCoroutine()
    {
        yield return fireDelay;
    }



    void Update()
    {
        base.Update();

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    protected override void Attack()
    {
        GameObject energyBall = Instantiate(magicPrefab, firePoint.position, Quaternion.identity);
    }

    protected override void Upgrade()
    {
        attackPower += 5f;
        range += 1f;
        attackSpeed += 0.2f;
        Debug.Log("����");
    }

}
