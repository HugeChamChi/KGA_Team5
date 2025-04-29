using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject stageSelect;

    private void Awake()
    {
        stageSelect.SetActive(false);
    }
    public void StartClick()
    {
        menu.SetActive(false);
        stageSelect.SetActive(true);
    }

    public void QuitClick()
    {
        //���� ������ �ڵ�
    }

    public void UndoClick()
    {
        menu.SetActive(true);
        stageSelect.SetActive(false);
    }

    public void StageClick(int n)
    {
        //n�� ���� �������� �� ��ȯ �ٸ��� �ڵ�
    }
}
