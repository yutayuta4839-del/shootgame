using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;

public class Tempo
{
    public float bpm;
    public int ticks;
}
public class Header
{
    public List<Tempo> tempos;
    public int ppq;
    public string name;
}
[Serializable]
public class MusicData
{  
    public Header header;  //
    public string name;
    public Note[] notedates;//音符データ
    public Track[] tracks;
}
[Serializable]
public class Track
{
    public Note[] notes;
}
[Serializable] public class Note
{
    public int ticks;
    public float time;     // 発音タイミング（秒）
    public int midi;       // 音の高さ
    public float duration; // 音の長さ
    public float velocity; // 強さ (0-1)
}

public class MusicNotesManager : MonoBehaviour
{
   
    private string songName;
    private float lastNoteSpawnedTime = -1f;
    [SerializeField] AudioSource audiosources;
    [SerializeField] AudioClip musicTrack;
    [SerializeField] TextAsset jsonfile;
    [SerializeField] Tempo tempoData;
    [SerializeField] Header headerData;
    [SerializeField] private MusicData musicData;
    [SerializeField] private int nextNoteIndex = 0;
    [SerializeField] private Note[] notes;
    [SerializeField] float musicintervals = 0.4f;
    [SerializeField] float timer = 0f;
    [SerializeField] GameObject musicnote;

    public List<int> Lanenum = new List<int>();
    public List<int> NoteType = new List<int>();
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NoteObj = new List<GameObject>();
    public Transform spawnPoint;
    private float lastSpawnTime = 0f;
    public GameObject textObject;
    [SerializeField] private float NotesSpeed;
    [SerializeField] private float startTime = 0f;
    bool isTimerRunning = false;
    public float elapsedTime = 0f;
    public float noteTime;
    public void Start()
    {
        headerData = JsonUtility.FromJson<Header>(jsonfile.text);
        musicData = JsonUtility.FromJson<MusicData>(jsonfile.text);
        tempoData = JsonUtility.FromJson<Tempo>(jsonfile.text);
        notes = musicData.tracks[0].notes;
     
    }

    void OnEnable()
    {
     
        songName = "テスト";
       
    }





    void Update()
    {

        if (Keyboard.current.spaceKey.isPressed)
        {
            if (!audiosources.isPlaying)
            {
                double delayTime = 1.0; // 1秒待ってから再生開始              
                double startTime = AudioSettings.dspTime + delayTime;
                textObject.SetActive(false);
                audiosources.clip = musicTrack;
                audiosources.PlayScheduled(startTime);
              
            }
        }

        if (notes == null || nextNoteIndex >= notes.Length) return;



         elapsedTime = audiosources.time;
     


        
        Load(songName);
    }


    public void Load(string SongName)
    {

        float currenttime = audiosources.time;
        while (nextNoteIndex < notes.Length && currenttime > notes[nextNoteIndex].time)
        {
          
            float tickToSec = 60f / (120 * 480);  // 1tickあたりの秒数
            // 各ノーツのtime
            noteTime = notes[nextNoteIndex].ticks * tickToSec;
          
            Vector3 notex;
           

            float laneX = 0f;
            if (notes[nextNoteIndex].midi < 50 || notes[nextNoteIndex].midi > 70)
            {
                laneX = -1.5f; // 左レーンのX座標
            }
            else
            {
                laneX = 1.5f;  // 右レーンのX座標
            }

            // 生成位置を決定
            float z = notes[nextNoteIndex].time * NotesSpeed;
            Vector3 spawnPos = new Vector3(laneX, spawnPoint.position.y, z);


            if (notes[nextNoteIndex].time - lastNoteSpawnedTime > 0.3f)
            {

                GameObject newNote = Instantiate(musicnote, spawnPos, Quaternion.identity);
                NoteObj.Add(newNote);
                NotesTime.Add(noteTime);
                lastNoteSpawnedTime = notes[nextNoteIndex].time;
                notex = newNote.transform.position;
                if (notex.x == 1.5f)
                {
                    Lanenum.Add(1);
                }
                else if (notex.x == -1.5f)
                {
                    Lanenum.Add(0);
                }
                if (musicnote)
                {
                    NoteType.Add(1);
                }
            }

         

            nextNoteIndex++;
        }



    }
  

}
