using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPlaying : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI goldText;

    public Button DoubleSpeedBtn;
    public Button NormalSpeedBtn;
    public Button PauseBtn;
    public Button WaveBtn;
    public GameObject PopupPause;
    public GameObject PopupLose;
    public GameObject PopupWin;
    public GameObject PopupStageInfo;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI timerText;

    public WaveLine waveLine;

    private bool isPaused = false;
    private bool isDoubled = false;
    private bool isOver = false;
    private bool isWave = false;

    private int currentWave = 0;

    private void OnEnable()
    {
        CameraController.OnCameraMoveDone += StartGame;
        StartCoroutine(DelayStartCamera());
    }
    private void StartGame()
    {
        GameManager.Instance.isStageStarted = true;
        if (PopupStageInfo != null)
            PopupStageInfo.SetActive(false);
    }
    private IEnumerator DelayStartCamera()
    {
        yield return new WaitForSeconds(.1f);
        FindObjectOfType<CameraController>().StartCameraMove();
    }

    private void Start()
    {
        PopupPause.SetActive(false);
        PopupLose.SetActive(false);
        PopupWin.SetActive(false);

        Time.timeScale = 1;

        UpdateUI();


        //���̺� ���� ���� ��������

    }

    private void Update()//��� ������Ʈ�� ���⼭ ������ �ƴѰŰ��� �̱��濡�� �ؾ���
    {
        if (isOver || !GameManager.Instance.isStageStarted)
        {
            return;
        }

        // GameManager.Instance.timer += Time.deltaTime * GameManager.Instance.isGamePause;
        //timerText.text = timer.ToString("F1") + Time.time.ToString("F1");//�ð��帣�°�Ȯ�ο�

        if (!isWave)
            WaveInfo();//���̺� ���� �� 

        GameCleared();
        //���⿡ ü�°� ��� ��ȭ ���� ������ ��
        //���Ͱ� ������ ��带�ش�
        //���Ͱ� ������ �����ϸ� ü���� ���δ�

        //���� �� �׾ ���̺� ������ isWave = false;
    }




    public void UpdateUI()//���� ü���� ���Ҷ����� �����ؾ��� �Լ�
    {
        healthText.text = GameManager.Instance.playerLife.ToString();
        goldText.text = GameManager.Instance.gold.ToString();
    }

    private void WaveInfo()//���̺����. ���̺� ���� ���� ǥ���� �Լ�
    {
        WaveBtn.gameObject.SetActive(true);
        //���̺갡 �̹������� �ɴϴ� ǥ��
        //waveLine.DrawPath(1, 1);//������� ���°���̺��¶�
                                //ǥ���Ұ͵��� currentWave�� ���õǰ� �迭���� �ɵ�

    }

    public void WaveStartClick()
    {
        WaveBtn.gameObject.SetActive(false);
        waveLine.HidePath();
        isWave = true;
        currentWave = currentWave + 1;
        waveText.text = "WAVE : " + currentWave.ToString() + "/ 5";//5�� �� ���̺� ������ �ٲܰ�
    }
    void GameCleared()
    {
        if (currentWave > 5)// ���� Ŭ����������
        {
            //���Ӹ��߰� ����Ŭ���� �̹����� ����
            GameManager.Instance.isGamePause = 0;
            Time.timeScale = 0;
            PopupWin.SetActive(true);
            GameManager.Instance.playerLife = 20;
            GameManager.Instance.gold = 100;
        }

        if (GameManager.Instance.playerLife <= 0)
        {
            //���Ӹ��߰� ���ӿ��� �̹����� ����
            GameManager.Instance.isGamePause = 0;
            Time.timeScale = 0;
            PopupLose.SetActive(true);
            GameManager.Instance.isGameOver = true;
            GameManager.Instance.playerLife = 20;
            GameManager.Instance.gold = 100;
        }

    }

    public void PauseButtonClick()
    {
        if (isPaused)
        {
            GameManager.Instance.isGamePause = 1;
            Time.timeScale = 1;
            isPaused = false;
            PopupPause.SetActive(false);
        }
        else
        {
            GameManager.Instance.isGamePause = 0;
            Time.timeScale = 0;
            isPaused = true;
            PopupPause.SetActive(true);
        }
    }

    public void DoubleSpeedClick()
    {
        if (isPaused)
            return;
        if (isDoubled)
        {
            Time.timeScale = 1;
            isDoubled = false;
            DoubleSpeedBtn.gameObject.SetActive(false);
            NormalSpeedBtn.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 2;
            isDoubled = true;
            DoubleSpeedBtn.gameObject.SetActive(true);
            NormalSpeedBtn.gameObject.SetActive(false);
        }
    }

    public void RestartClick()
    {
        GameManager.Instance.isStageStarted = false;
        CameraController.OnCameraMoveDone -= StartGame;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NextClick()
    {
        //������������ ��¼��
    }


}
