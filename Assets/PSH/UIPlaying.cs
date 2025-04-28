using TMPro;
using UnityEngine;
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
    private bool isGameStarted = false;
    private bool isWave = false;

    private int health = 10;
    private int gold = 100;

    private float timer = 0f;
    private int gamePause = 1;
    private int currentWave = 0;

    private void OnEnable()
    {
        CameraController.OnCameraMoveDone += StartGame;
    }
    private void StartGame()
    {
        isGameStarted = true;
        PopupStageInfo.SetActive(false);
    }
    private void Start()
    {
        PopupPause.SetActive(false);
        PopupLose.SetActive(false);
        PopupWin.SetActive(false);

        UpdateUI();

        //���̺� ���� ���� ��������

    }

    private void Update()
    {
        if (isOver || !isGameStarted)
        {
            return;
        }

        timer += Time.deltaTime * gamePause;
        timerText.text = timer.ToString("F1") + Time.time.ToString("F1");//�ð��帣�°�Ȯ�ο�

        if (!isWave)
            WaveInfo();//���̺� ���� �� 

        GameCleared();
        //���⿡ ü�°� ��� ��ȭ ���� ������ ��
        //���Ͱ� ������ ��带�ش�
        //���Ͱ� ������ �����ϸ� ü���� ���δ�

        //���� �� �׾ ���̺� ������ isWave = false;
    }

    private void UpdateUI()
    {
        healthText.text = health.ToString();
        goldText.text = gold.ToString();
    }

    private void WaveInfo()//���̺����
    {
        WaveBtn.gameObject.SetActive(true);
        //���̺갡 �̹������� �ɴϴ� ǥ��
        waveLine.DrawPath(1);
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
            gamePause = 0;
            PopupWin.SetActive(true);
            isOver = true;
        }

        if (health <= 0)
        {
            //���Ӹ��߰� ���ӿ��� �̹����� ����
            gamePause = 0;
            PopupLose.SetActive(true);
            isOver = true;
        }

    }

    public void PauseButtonClick()
    {
        if (isPaused)
        {
            gamePause = 1;
            Time.timeScale = 1;
            isPaused = false;
            PopupPause.SetActive(false);
        }
        else
        {
            gamePause = 0;
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
        //����۹�ư�������¼��
    }

    public void QuitClick()
    {
        //����ư�������¼��
    }

    public void NextClick()
    {
        //������������ ��¼��
    }


}
