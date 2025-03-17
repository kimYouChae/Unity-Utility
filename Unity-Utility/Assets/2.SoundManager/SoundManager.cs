using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum BGM 
{ 
    Lobby,
    DungeonRoom,
    BossRoom
}


public enum UISound
{
    Click
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance { get => instance; }

    private void Awake()
    {
        // �̹� �ν��Ͻ��� �����ϰ� ���� ������Ʈ�� �ƴ϶��
        if (instance != null && instance != this)
        {
            // �ߺ��� �ν��Ͻ� ����
            Destroy(gameObject);
            return;
        }

        // �ν��Ͻ��� ������ ���� �ν��Ͻ� ����
        instance = this;
    }

    [Header("===Player Sound===")]
    [SerializeField] AudioClip[] playerClip;

    [Header("===Background Sound===")]
    [SerializeField] AudioClip[] backGroundClip;

    [Header("===Dungeon Sound===")]
    [SerializeField] AudioClip[] dungeonSound;

    [Header("===Ui Sound===")]
    [SerializeField] AudioClip[] uiClickSound;

    /// <summary>
    /// �����
    /// 1. �����տ� AudioSource������Ʈ�� �Ҵ��Ѵ�
    /// 2. AudioSource ������ ���� ��, Awake/Start���� Audio GetComponent 
    /// 3. SoundManager.Instance.PlayPlayerSound(������ҽ�, enum);
    /// 
    /// play : source�� �̸� clip�� �Ҵ�Ǿ��־�� ��, ������� �Ҹ� ���� ��Ų �� ���
    /// PlayOnShot : �����ų clip �Ű�������, ������� ���� �ʰ� ���� ���
    /// </summary>

    // ��� ����
    public void PlaySounds(AudioSource audioSource, BGM sound) 
    {
        try
        {
            //audioSource.PlayOneShot(backGroundClip[(int)sound]);

            // ���� ��� ���� ���尡 ������ ����
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // �� Ŭ�� ���� �� ���
            audioSource.clip = backGroundClip[(int)sound];
            audioSource.Play();
        }
        catch (Exception ex) { Debug.Log($"SoundManager ���� : {ex}"); }
    }

    // Ui ����
    public void PlaySounds(AudioSource audioSource, UISound sound)
    {
        try
        {
            audioSource.PlayOneShot(uiClickSound[(int)sound]);
        }
        catch (Exception ex) { Debug.Log($"SoundManager ���� : {ex}"); }
    }


    // ���尡 ���� Manager���� ����ϰ� �ִٸ�
    public void PlaySounds(AudioSource audioSource, AudioClip sound) 
    {
        try
        {
            audioSource.PlayOneShot(sound);
        }
        catch (Exception ex) { Debug.Log($"SoundManager ���� : {ex}"); }
    }

}
