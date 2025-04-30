using System.Collections;
using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    public GameObject buildUI;
    public GameObject manageUI;
    public Transform spawnPoint; // Ÿ�� ��ġ �̰� ���־���ϴ°���

    private GameObject currentTower;

    public GameObject testprefab;

    private void OnMouseDown()
    {
        if (currentTower == null)
        {
            buildUI.SetActive(true);
            buildUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            StartCoroutine(ScaleUpCoroutine(buildUI));
            BuildManager.Instance.SetCurrentSpot(this);
        }
        else
        {
            manageUI.SetActive(true);
            manageUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            StartCoroutine(ScaleUpCoroutine(manageUI));
            BuildManager.Instance.SetCurrentSpot(this);
        }
        //������ ���� �����ؾ��� Ŭ�� ������ �������� �����°� �������Ű����� ��ġ�� ���� ui�� �߷����̰ų� �� �� ����
        //���� �����ų� ui ��ġ�� �����ϰ� Ŭ�� ��ġ�� ���̶���Ʈ�ϰų�
        //ui 2�� ���ÿ� �ȳ����� �ϴ� ���� �ʿ�
        //ui ���� ��� ���� �ʿ� 
    }

    IEnumerator ScaleUpCoroutine(GameObject UI)
    {
        float duration = 0.2f;
        float timer = 0;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer/ duration);
            UI.transform.localScale = Vector3.Lerp(Vector3.zero,Vector3.one, t);
            yield return null;
        }

        UI.transform.localScale = Vector3.one;
    }
    public void BuildTower(GameObject prefab)
    {
        currentTower = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        buildUI.SetActive(false);
    }

    public void UpgradeTower()
    {
        // ���׷��̵� ����
        // ������ Ÿ���ı� ������ Ÿ������
        Destroy(currentTower);
        currentTower = Instantiate(testprefab, spawnPoint.position, Quaternion.identity);
        //currentTower = Instantiate();
        //�������� Ÿ���� ��� ��������
        Debug.Log("���׷��̵��");
        manageUI.SetActive(false);
    }

    public void RemoveTower()
    {        
        Destroy(currentTower);
        currentTower = null;
        manageUI.SetActive(false);
    }
}
