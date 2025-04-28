using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
// ���带 ����� ���ӽ����̽� ���� ����
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;
    public AudioSource bgm;
    public AudioClip[] bgmlist;
    public AudioMixer audioMixer;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //SceneManager.sceneLoaded += OnSceneLoaded;
            backGroundMusicPlay(bgmlist[0]);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        
    }
    // �⺻������ ����ϴ� �������
    public void backGroundMusicPlay(AudioClip cilp)
    {
        bgm.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];
        bgm.clip = cilp;
        bgm.loop = true;
        bgm.volume = 0.5f;
        bgm.Play();
    }
    // ���� ���� ����Ǵ� �������
    /*private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i = 0; i < bgmlist.Length; i++)
        {
            if (arg0.name == bgmlist[i].name);
        }
    }*/

    // UI�� ����Ǵ� ������� ��Ʈ�ѷ�
    public void BGMController(float value)
    {
        audioMixer.SetFloat("BGMParam", Mathf.Log10(value) * 20);
    }
}
