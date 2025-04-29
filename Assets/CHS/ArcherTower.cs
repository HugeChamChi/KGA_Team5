using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public enum WeaponState { SearchTarget, AttackToTarget }        // ���� �߰��ϰ� ���
public class ArcherTower : MonoBehaviour, ITower
{

    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform muzzlePos;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private float attackRate;

    [SerializeField] private Animator animator;

    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private int poolSize;
    public Stack<GameObject> arrowPool;

    private Coroutine arrowCoroutine;
    private YieldInstruction arrowDelay;

    [SerializeField] private Transform targetPos;

    private string arrowType;       // ���߿� �߰��� �Ӽ�? �ɷ�?

    [SerializeField] private Transform currentTarget;     // ���� Ÿ�� ����

    [SerializeField] private float attackPower;
    [SerializeField] private float range;       // ��Ÿ�
    [SerializeField] private float attackSpeed;

    
    public float Damage => attackPower;
    public float Range => range;
    public float AttackSpeed => attackSpeed;

    private void Awake()
    {
        arrowDelay = new WaitForSeconds(attackRate);
    }


    private void Start()
    {
        arrowPool = new Stack<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(arrowPrefab);
            obj.GetComponent<Arrow>().returnPool = arrowPool;
            obj.SetActive(false);
            arrowPool.Push(obj);
        }
    }


    private void Update()
    {
        findEnemy();
    }

    private void findEnemy()
    {
        if (currentTarget != null && currentTarget.gameObject.activeInHierarchy)
        {
            // ���� Ÿ���� ������ ��Ÿ� �ȿ� �ִ��� Ȯ��
            float distance = Vector3.Distance(transform.position, currentTarget.position);
            if (distance <= range)
            {
                Vector3 lookPos = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
                transform.LookAt(lookPos);

                if (arrowCoroutine == null)
                {
                    Attack();
                }

                return; // ���� Ÿ�� ���� ���̹Ƿ� �� Ÿ�� ã�� ����
            }
            else
            {
                // Ÿ�� ��Ÿ� ���
                currentTarget = null;
            }
        }

        // �� Ÿ�� ã�� (���̾� 6����)
        Collider[] hits = Physics.OverlapSphere(transform.position, range, enemyLayer);

        foreach (var hit in hits)
        {
            if (hit.gameObject.layer == 6 && hit.gameObject.activeInHierarchy)      // �±� ��� �� other.CompareTag("Enemy")
            {
                currentTarget = hit.transform;
                break;
            }
        }

        if (currentTarget != null)
        {
            Vector3 lookPos = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
            transform.LookAt(lookPos);

            if (arrowCoroutine == null)
            {
                Attack();
            }
        }
        else
        {
            // Ÿ�� ����
            if (arrowCoroutine != null)
            {
                StopCoroutine(arrowCoroutine);
                arrowCoroutine = null;
            }
        }
    }
    public void Attack()
    {
        if (arrowCoroutine == null)
        {
            arrowCoroutine = StartCoroutine(AttackCoroutine());
        }
    }
    public void AttackEnemy(Enemy enemy)
    {
        float damage = GameManager.Instance.CalculatePhysicalDamage(attackPower, enemy.defense);

        enemy.TakePhysicalDamage(damage);
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {

            GameObject arrow = arrowPool.Pop();

            arrow.transform.position = muzzlePos.position;
            arrow.transform.forward = transform.forward;

            arrow.SetActive(true);

            yield return arrowDelay;
        }
    }

    public void CriticalShot()
    {

    }
    
    public void Upgrade()
    {
        attackPower += 5f;
        attackSpeed *= 0.9f;
        arrowDelay = new WaitForSeconds(attackRate / attackSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
