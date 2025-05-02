using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildPoint : MonoBehaviour
{
    public GameObject buildUI;
    public GameObject manageUI;
    private Transform spawnPoint;

    private GameObject currentTower;
    private int a;//���� �������� ui�� ��������� buildUI���� manageUI���� �����ϱ����� 0, 1, 2

    //Ÿ�����迭
    public GameObject[] tower1;
    public GameObject[] tower2;
    public GameObject[] tower3;

    private int towerType = 0;
    private int towerLevel = 0;

    private void Awake()
    {
        spawnPoint = transform;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log($"����a��{a}�Դϴ�");
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (!Physics.Raycast(ray, out RaycastHit hit) && a != 0)
            {
                CloseAllUI();
                a = 0;
            }
        }
    }

    private void OnMouseDown()
    {
        if (Time.timeScale == 0f)//�Ͻ��������� �۵� ���ϰ�
            return;

        if (currentTower == null)
        {
            if (a == 1 || a == 0) manageUI.SetActive(false);
            CloseAllUI();
            buildUI.SetActive(true);
            buildUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            StartCoroutine(ScaleUpCoroutine(buildUI));
            BuildManager.Instance.SetCurrentSpot(this);
            a = 1;
        }
        else
        {
            if (a == 2 || a == 0) buildUI.SetActive(false);
            CloseAllUI();
            manageUI.SetActive(true);
            manageUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            StartCoroutine(ScaleUpCoroutine(manageUI));
            BuildManager.Instance.SetCurrentSpot(this);
            a = 2;
        }
        //������ ���� �����ؾ��� Ŭ�� ������ �������� �����°� �������Ű����� ��ġ�� ���� ui�� �߷����̰ų� �� �� ����
        //���� �����ų� ui ��ġ�� �����ϰ� Ŭ�� ��ġ�� ���̶���Ʈ�ϰų�
    }

    IEnumerator ScaleUpCoroutine(GameObject UI)
    {
        float duration = 0.2f;
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            UI.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            yield return null;
        }

        UI.transform.localScale = Vector3.one;
    }

    IEnumerator ScaleDownCoroutine(GameObject UI)
    {
        float duration = 0.2f;
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            UI.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);
            yield return null;
        }

        UI.transform.localScale = Vector3.zero;
    }

    public void CloseAllUI()
    {
        StartCoroutine(ScaleDownCoroutine(buildUI));
        StartCoroutine(ScaleDownCoroutine(manageUI));
    }
    public void CloseAllUI(int n)
    {
        switch (n)
        {
            case 1:
                StartCoroutine(ScaleDownCoroutine(buildUI));
                break;
            case 2:
                StartCoroutine(ScaleDownCoroutine(manageUI));
                break;
            default:
                break;
        }
    }
    public void BuildTower(int n)//n�� ���� Ÿ�� �Ǽ��ϱ�
    {
        towerType = n;
        towerLevel = 0;

        switch (n)
        {
            case 1:
                currentTower = Instantiate(tower1[0], spawnPoint.position + Vector3.up, Quaternion.identity);
                break;
            case 2:
                currentTower = Instantiate(tower2[0], spawnPoint.position + Vector3.up, Quaternion.identity);
                break;
            case 3:
                currentTower = Instantiate(tower3[0], spawnPoint.position + Vector3.up, Quaternion.identity);
                break;
            default:
                break;
        }
        //�ϴ� ���������ǿ� 1 ���ϴ°ɷ� �ߴµ� ÷���� ��������Ʈ�� �����ϴ°� �����Ű�����
        buildUI.SetActive(false);
    }

    public void UpgradeTower()
    {
        towerLevel++;

        if (towerType == 1 && towerLevel < tower1.Length)
        {
            Destroy(currentTower);
            currentTower = Instantiate(tower1[towerLevel], spawnPoint.position + Vector3.up, Quaternion.identity);
        }
        else if (towerType == 2 && towerLevel < tower2.Length)
        {
            Destroy(currentTower);
            currentTower = Instantiate(tower2[towerLevel], spawnPoint.position + Vector3.up, Quaternion.identity);
        }
        else if (towerType == 3 && towerLevel < tower3.Length)
        {
            Destroy(currentTower);
            currentTower = Instantiate(tower3[towerLevel], spawnPoint.position + Vector3.up, Quaternion.identity);
        }
        else
        {
            towerLevel--; // �ǵ�����
            Debug.Log("�ִ� ���׷��̵� �ܰ��Դϴ�");
        }

        manageUI.SetActive(false);
    }

    public void RemoveTower()
    {
        Destroy(currentTower);
        currentTower = null;
        manageUI.SetActive(false);
    }
}
