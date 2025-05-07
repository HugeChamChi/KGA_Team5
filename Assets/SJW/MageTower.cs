using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    [Header("���� Ÿ�� ���� ����")]
    [SerializeField] private GameObject magicPrefab;
    [SerializeField] private Transform firePoint;

    private float nextAttackTime = 0f; // ���� ���� ������ �ð�

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void Update()
    {
        base.Update(); // �θ� Ŭ�������� FindEnemy() ȣ���
    }

    protected override void Attack()
    {
        if (Time.time < nextAttackTime) return;

        if (currentTarget == null) return;

        GameObject energyBall = EnergyBallPool.Instance.GetFromPool();
        if (energyBall == null) return;

        energyBall.transform.position = firePoint.position;
        energyBall.transform.rotation = Quaternion.identity;

        // ���� ���
        Vector3 direction = (currentTarget.position - firePoint.position).normalized;

        Rigidbody rb = energyBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float projectileSpeed = EnergyBallPool.Instance.ProjectileSpeed;
            rb.velocity = direction * projectileSpeed;
        }

        nextAttackTime = Time.time + (1f / attackSpeed);
    }


    protected override void Upgrade()
    {
        attackPower += 5f;
        range += 1f;
        attackSpeed += 0.2f;
        Debug.Log("����");
    }
}

