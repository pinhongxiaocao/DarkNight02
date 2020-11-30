using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicMgr : BaseManager<MusicMgr>
{
    //背景音乐
    private AudioSource bkMusic=null;
    //背景音乐大小
    private float bkValue=1f;

    //音效依附的空物体
    private GameObject soundObj = null;
    //音效列表
    private List<AudioSource> soundList = new List<AudioSource>();
    //音效大小
    private float soundValue = 1f;

    public MusicMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(Update);
    }

    private void Update()
    {
        for (int i = soundList.Count - 1; i >= 0; i--)
        {
            if (!soundList[i].isPlaying)//没播我们就删除audiosource组件防止占用空间
            {
                GameObject.Destroy(soundList[i]);
                Debug.Log(1);
                soundList.RemoveAt(i);
            }
        }
    }

    #region 背景音乐
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name"></param>
    public void PlayBKMusic(string name)
    {
        if (bkMusic == null)
        {
            GameObject obj = new GameObject();
            obj.name = "BKMusic";
            bkMusic = obj.AddComponent<AudioSource>();
        }
        //异步加载音乐 加载之后播放
        ResMgr.GetInstance().LoadAsync<AudioClip>("Music/BK/" + name, (clip) =>
        {
            bkMusic.clip = clip;
            bkMusic.loop = true;
            bkMusic.volume = bkValue;
            bkMusic.Play();
        });
    }
    /// <summary>
    /// 改变背景音乐音量大小
    /// </summary>
    /// <param name="v"></param>
    public void ChangeBKValue(float v)
    {
        bkValue = v;
        if (bkMusic == null)
        {
            return;
        }
        bkMusic.volume = bkValue;
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBKMusic()
    {
        if (bkMusic == null)
        {
            return;
        }
        bkMusic.Pause();
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBKMusic()
    {
        if (bkMusic == null)
        {
            return;
        }
        bkMusic.Stop();
    }
    #endregion
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name"></param>
    public void PlaySound(string name,bool isLoop=false,UnityAction<AudioSource>callBack=null)
    {
        if (soundObj == null)
        {
            soundObj = new GameObject();
            soundObj.name = "Sound";
        }
        //异步加载音效 加载之后播放
        ResMgr.GetInstance().LoadAsync<AudioClip>("Music/Sound/" + name, (clip) =>
        {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            source.volume = soundValue;
            source.Play();
            soundList.Add(source);
            if (callBack != null)
            {
                callBack(source);
            }
        });
    }

    public void ChangeSoundVolume(float value)
    {
        soundValue = value;
        for (int i = 0; i < soundList.Count; i++)
        {
            soundList[i].volume = value;
        }
    }

    /// <summary>
    /// 停止音效
    /// </summary>
    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }
}

