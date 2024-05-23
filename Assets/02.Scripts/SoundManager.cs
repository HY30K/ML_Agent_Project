using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public enum SoundType
{
    MASTER,
    BGM,
    SFX
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    private AudioMixerGroup masterMixer;
    private AudioMixerGroup bgmMixer;
    private AudioMixerGroup sfxMixer;
    private AudioMixer mainMixer;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject("SoundManager");
            instance = obj.AddComponent<SoundManager>();
            instance.InitInstance();
        }
    }

    private void InitInstance()
    {
        mainMixer = Resources.Load<AudioMixer>("Audio/MainMixer");
        sfxMixer = mainMixer.FindMatchingGroups("SFX")[0];
        bgmMixer = mainMixer.FindMatchingGroups("BGM")[0];
        masterMixer = mainMixer.FindMatchingGroups("Master")[0];

        Debug.Log($"sfxMixer: {sfxMixer}");
        Debug.Log($"bgmMixer: {bgmMixer}");
        Debug.Log($"masterMixer: {masterMixer}");

        DontDestroyOnLoad(gameObject);
    }


    public static void PlaySound(string clipName, SoundType type = SoundType.SFX)
    {
        if (instance == null) return;

        instance.Play(clipName, type);
    }

    private void Play(string clipName, SoundType type)
    {
        GameObject obj = new GameObject("AudioSource_" + clipName);
        var source = obj.AddComponent<AudioSource>();

        AudioClip clip = Resources.Load<AudioClip>($"Sound/{clipName}");
        if (clip == null)
        {
            Debug.LogError($"AudioClip {clipName} not found in Resources/Sound");
            Destroy(obj);
            return;
        }

        source.clip = clip;
        source.spatialBlend = 0;
        source.outputAudioMixerGroup = type switch
        {
            SoundType.SFX => sfxMixer,
            SoundType.BGM => bgmMixer,
            _ => masterMixer
        };

        if (type == SoundType.BGM)
        {
            source.loop = true;
        }
        else
        {
            StartCoroutine(SFXDestroyCo(clip.length, obj));
        }

        source.Play();
    }

    public static void SettingMaster(float value)
    {
        instance.mainMixer.SetFloat("Master", Mathf.Log10(value) * 20);
    }

    public static void SettingBgm(float value)
    {
        instance.mainMixer.SetFloat("BGM", Mathf.Log10(value) * 20);
    }

    public static void SettingSfx(float value)
    {
        instance.mainMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
    }

    private static IEnumerator SFXDestroyCo(float length, GameObject obj)
    {
        yield return new WaitForSeconds(length + 0.1f);

        if (obj != null)
        {
            Destroy(obj);
        }
    }
}
