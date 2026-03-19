using UnityEngine;

using UnityEngine.Audio;

using UnityEngine.InputSystem;

using System;

using System.Collections.Generic;



[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Note[] notes;

}
[Serializable]
public class Note
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

public class MusicNotesManager : MonoBehaviour
{

    

    private string songName;
    public int noteNum;


    private float lastNoteSpawnedTime = -1f;

    [SerializeField] AudioClip audioclip;
    [SerializeField] AudioSource audiosource;
    [SerializeField] private Data Data;
    [SerializeField] public Note[] notes;
    [SerializeField] private int nextNoteIndex = 0;
    [SerializeField] GameObject textObject;
    [SerializeField] private float NotesSpeed;
    [SerializeField] GameObject noteObj;
    [SerializeField] public float elapsedTime = 0f;

    public List<int> LaneNum = new List<int>();
    public List<int> NoteType = new List<int>();
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NoteObj = new List<GameObject>();
    public Transform spawnPoint;
    [SerializeField] float startTime = 0f;
  

    
  

    void OnEnable()
    {
        noteNum = 0;
        songName = "テスト";
       
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {


            if (!audiosource.isPlaying)
            {
                textObject.SetActive(false);
                audiosource.clip = audioclip;
                audiosource.Play();
                Load(songName);

            }
        }
            elapsedTime = audiosource.time;
    }


    public void Load(string SongName)
    {
        string inputString = Resources.Load<TextAsset>(SongName).ToString();
        Data inputJson = JsonUtility.FromJson<Data>(inputString);
     
        noteNum = inputJson.notes.Length;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB)+ 2f ;
            NotesTime.Add(time);

            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);

            // 生成位置を決定
            float y = NotesTime[i] * NotesSpeed;
            if (inputJson.notes[i].block == 0)
            {
                NoteObj.Add(Instantiate(noteObj, new Vector3(1.5f, y, 0), Quaternion.identity));
            }else if (inputJson.notes[i].block == 1)
            {
                NoteObj.Add(Instantiate(noteObj, new Vector3(-1.5f, y, 0), Quaternion.identity));
            }

          
                            
           
            

        }

    }
}
