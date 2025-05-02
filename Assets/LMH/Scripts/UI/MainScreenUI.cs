using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenUI : MonoBehaviour
{
    public void GameStart()
    {
        SceneChangeManager.Instance.ChangeScene("TestingStageSelectScreen");
    }

    public void HowToPlay()
    {

    }

    public void GameQuit()
    {
        Application.Quit();
        Debug.Log("����� ������ ����Ǿ����� ������ �󿡼��� ������� ���� ������ ó���˴ϴ�.");
    }
}
