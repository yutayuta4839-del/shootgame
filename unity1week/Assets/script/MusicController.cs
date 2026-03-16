using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class MusicData
{
    public Header header;
    public Track[] tracks;
}

[Serializable]
public class Header
{
    public Tempo[] tempos;
}

[Serializable]
public class Tempo
{
    public float bpm;
}

[Serializable]
public class Track
{
    public Note[] notes;
}

[Serializable]
public class Note
{
    public float time;     // 発音タイミング（秒）
    public int midi;       // 音の高さ
    public float duration; // 音の長さ
    public float velocity; // 強さ (0-1)
}

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioSource audiosources;
    [SerializeField] AudioClip musicTrack;
    [SerializeField] TextAsset jsonfile;
    [SerializeField] private MusicData musicData;
    [SerializeField] private int nextNoteIndex = 0;
    [SerializeField] private Note[] notes;
    [SerializeField] float musicintervals = 0.4f;

    public List<int> Lanenum = new List<int>();
    public List<int> NoteType = new List<int>();
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NoteObj = new List<GameObject>();
    public Transform spawnPoint;
    private float lastSpawnTime = 0f;
    public GameObject textObject;
    private void Start()
    {
        musicData = JsonUtility.FromJson<MusicData>(jsonfile.text);
        notes = musicData.tracks[0].notes;
    }







    void Update()
    {
        
        if (Keyboard.current.spaceKey.isPressed)
        {
            if (!audiosources.isPlaying)
            {
                textObject.SetActive(false);
                audiosources.clip = musicTrack;
                audiosources.Play();
            }
        }

        if (notes == null || nextNoteIndex >= notes.Length) return;

        float currenttime = audiosources.time;
        while(nextNoteIndex < notes.Length && currenttime > notes[nextNoteIndex].time)
        {
            if (Time.time - lastSpawnTime >= musicintervals)
            {
                OnNoteTriggered(notes[nextNoteIndex]);
                lastSpawnTime = Time.time;
            }
                nextNoteIndex++;
           
        }

        void OnNoteTriggered(Note note)
        {
            Debug.Log(" タイミング: {note.time}");
            float laneX = 0;

           
            if (note.midi < 50 || note.midi > 65)
            {
                laneX = -1.5f; // 左レーンのX座標
            }
            else
            {
                laneX = 1.5f;  // 右レーンのX座標
            }

            // 生成位置を決定
            Vector3 spawnPos = new Vector3(laneX, spawnPoint.position.y, spawnPoint.position.z);

            
            Instantiate(NoteObj[0], spawnPos, Quaternion.identity);
        }







    }
}
