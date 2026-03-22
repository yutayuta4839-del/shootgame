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




    [SerializeField] AudioClip audioclip;
    [SerializeField] AudioSource audiosource;
    [SerializeField] private Data Data;
    [SerializeField] public Note[] notes;
    [SerializeField] GameObject textObject;
    [SerializeField] private float NotesSpeed;
    [SerializeField] GameObject[] noteObjects;
    [SerializeField] public float elapsedTime = 0f;

    public List<int> LaneNum = new List<int>();
    public List<int> NoteType = new List<int>();
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NoteObj = new List<GameObject>();
    public Transform spawnPoint;
    [SerializeField] GameObject resultText; // 終了時に出したいTextオブジェクトをアタッチ
    private bool isGameStarted = false;
    [SerializeField] GameObject finishbutton;




    void OnEnable()
    {
        noteNum = 0;
        songName = Data.name;    
    }

    void Update()
    {


        if (Keyboard.current.spaceKey.isPressed || Pointer.current.press.wasPressedThisFrame)
        {


            if (!audiosource.isPlaying)
            {
                textObject.SetActive(false);
                audiosource.clip = audioclip;
                audiosource.Play();
                Load(songName);
                isGameStarted = true;
            }
        }
        if (audiosource.isPlaying)
        {
            elapsedTime = audiosource.time;
        }
        else if (isGameStarted && !audiosource.isPlaying)
        {
            OnSongEnd();
        }

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
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + 2f;
            NotesTime.Add(time);

            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);

            // 生成位置を決定
            float y = NotesTime[i] * NotesSpeed;
            if (inputJson.notes[i].block == 0)
            {
                if (inputJson.notes[i].type == 1)
                {
                    NoteObj.Add(Instantiate(noteObjects[0], new Vector3(1.5f, y, 0), Quaternion.identity));
                }
                else if (inputJson.notes[i].type == 2)
                {
                    NoteObj.Add(Instantiate(noteObjects[1], new Vector3(1.5f, y, 0), Quaternion.identity));
                }

            }


            if (inputJson.notes[i].block == 1)
            {
                if (inputJson.notes[i].type == 1)
                {
                    NoteObj.Add(Instantiate(noteObjects[0], new Vector3(-1.5f, y, 0), Quaternion.identity));
                }
                else if (inputJson.notes[i].type == 2)
                {
                    NoteObj.Add(Instantiate(noteObjects[1], new Vector3(-1.5f, y, 0), Quaternion.identity));
                }

            }








        }

    }
    void OnSongEnd()
    {
        isGameStarted = false; // 二重実行防止
        if (resultText != null)
        {
            resultText.SetActive(true); // リザルトテキストを表示
            finishbutton.SetActive(true);
        }
        Debug.Log("曲が終了しました！");
    }
}
