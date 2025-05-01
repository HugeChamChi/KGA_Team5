using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTower : Tower
{
    [Header("���� Ÿ�� ���� ����")]
    [SerializeField] private GameObject magicPrefab;  // ���� ����ü ������
    [SerializeField] private Transform firePoint;     // �߻� ��ġ
    [SerializeField] private SphereCollider rangeCollider;

    private float attackCooldown = 0f;

    void Start()
    {
        if (rangeCollider != null)
        {
            rangeCollider.radius = range;
            rangeCollider.isTrigger = true;
            rangeCollider.center = Vector3.zero; // Ÿ�� �߽ɿ� ����
        }
    }

    protected override void Update()
    {
        base.Update();

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    protected override void Attack()
    {
        if (attackCooldown > 0f || currentTarget == null) return;
        Debug.Log("�����߻�!");
        attackCooldown = 1f / attackSpeed;
    }

    protected override void Upgrade()
    {
        attackPower += 5f;
        range += 1f;
        attackSpeed += 0.2f;
        Debug.Log("����");
    }

    void OnValidate()
    {
        if (rangeCollider != null)
            rangeCollider.radius = range;
    }
}
