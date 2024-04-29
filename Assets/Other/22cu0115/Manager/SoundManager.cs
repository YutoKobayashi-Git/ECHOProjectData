using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    // ������(string)��name�ƁA���ۂ̉���(AudioClip)��audioClip�̃y�A���Ǘ�����N���X
    [System.Serializable]
    public class SoundData
    {
        public string name;
        public AudioClip audioClip;
        public float audioVolume;
        public float playedTime;    //�O��Đ���������
    }

    [SerializeField]
    private SoundData[] soundDatas;

    //AudioSource�i�X�s�[�J�[�j�𓯎��ɖ炵�������̐������p��
    private AudioSource[] audioSourceList = new AudioSource[20];

    //�ʖ�(name)���L�[�Ƃ����Ǘ��pDictionary
    private Dictionary<string, SoundData> soundDictionary = new Dictionary<string, SoundData>();

    //��x�Đ����Ă���A���Đ��o����܂ł̊Ԋu(�b)
    [SerializeField]
    private float playableDistance = 0.2f;

    protected override void Awake()
    {
        base.Awake();

        //auidioSourceList�z��̐�����AudioSource���������g�ɐ������Ĕz��Ɋi�[
        for (var i = 0; i < audioSourceList.Length; ++i)
        {
            audioSourceList[i] = gameObject.AddComponent<AudioSource>();
        }

        //soundDictionary�ɃZ�b�g
        foreach (var soundData in soundDatas)
        {
            soundDictionary.Add(soundData.name, soundData);
        }
    }

    //���g�p��AudioSource�̎擾 �S�Ďg�p���̏ꍇ��null��ԋp
    private AudioSource GetUnusedAudioSource()
    {
        for (var i = 0; i < audioSourceList.Length; ++i)
        {
            if (audioSourceList[i].isPlaying == false) return audioSourceList[i];
        }

        return null; //���g�p��AudioSource�͌�����܂���ł���
    }

    //�w�肳�ꂽAudioClip�𖢎g�p��AudioSource�ōĐ�
    public void Play(AudioClip clip , float volume)
    {
        var audioSource = GetUnusedAudioSource();
        if (audioSource == null) return; //�Đ��ł��܂���ł���
        audioSource.clip = clip;
        audioSource.volume = Mathf.Clamp01(volume);
        audioSource.Play();
    }

    //�w�肳�ꂽ�ʖ��œo�^���ꂽAudioClip���Đ�
    public void Play(string name)
    {
        if (soundDictionary.TryGetValue(name, out var soundData)) //�Ǘ��pDictionary ����A�ʖ��ŒT��
        {
            if (Time.realtimeSinceStartup - soundData.playedTime < playableDistance) return;    //�܂��Đ�����ɂ͑���
            soundData.playedTime = Time.realtimeSinceStartup;//����p�ɍ���̍Đ����Ԃ̕ێ�
            Play(soundData.audioClip,soundData.audioVolume); //����������A�Đ�
        }
        else
        {
            Debug.LogWarning($"���̕ʖ��͓o�^����Ă��܂���:{name}");
        }
    }
}