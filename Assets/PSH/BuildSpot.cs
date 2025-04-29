using UnityEngine;

public class BuildPoint : MonoBehaviour
{
    public GameObject buildUI;
    public GameObject manageUI;
    public Transform spawnPoint; // Ÿ�� ��ġ

    private GameObject currentTower;

    private void OnMouseDown()
    {
        if (currentTower == null)
        {
            buildUI.SetActive(true);
            buildUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            BuildManager.Instance.SetCurrentSpot(this);
        }
        else
        {
            manageUI.SetActive(true);
            manageUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
            BuildManager.Instance.SetCurrentSpot(this);
        }
    }

    public void BuildTower(GameObject prefab)
    {
        currentTower = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        buildUI.SetActive(false);
    }

    public void UpgradeTower()
    {
        // ���׷��̵� ����
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
