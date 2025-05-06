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

        GameObject energyBall = EnergyBallPool.Instance.GetFromPool();
        if (energyBall == null) return;

        energyBall.transform.position = firePoint.position;
        energyBall.transform.rotation = Quaternion.identity;

        // ���� ���� ���� �ð� ��� (1�� / �ʴ� ���� Ƚ��)
        if (attackSpeed > 0f)
            nextAttackTime = Time.time + (1f / attackSpeed);
        else
            nextAttackTime = Time.time + 1f; // Ȥ�� 0�� ��� ���
    }

    protected override void Upgrade()
    {
        attackPower += 5f;
        range += 1f;
        attackSpeed += 0.2f;
        Debug.Log("����");
    }
}

