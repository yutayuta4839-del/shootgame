using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class Checknotes : MonoBehaviour
{
    [SerializeField] GameObject[] textsobj;
    [SerializeField] MusicNotesManager musiccontroller;
    [SerializeField] Transform line;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        if (musiccontroller.NotesTime.Count == 0)
        {
            return;
        }
     

      
        int currentLane = musiccontroller.Lanenum[0];
        // Dキーの判定
        if (Keyboard.current.dKey.wasPressedThisFrame && currentLane == 0)
        {
           
            Judgement(GetAbs(musiccontroller.noteTime - musiccontroller.elapsedTime), currentLane);


        }

          // Fキーの判定 (QからFに変更しました)
        if (Keyboard.current.fKey.wasPressedThisFrame && currentLane == 1)
        {
                                                          
            Judgement(GetAbs(musiccontroller.noteTime - musiccontroller.elapsedTime), currentLane);

        }





    }


    void Judgement(float timelag, int lane)
    {       
         if (timelag <= 0.15f)
        {
            message(0, lane); // Good
           
            Debug.Log(musiccontroller.elapsedTime);
            Debug.Log(timelag);
        }
        else
        {
            message(1, lane); // Bad/Miss
           
            Debug.Log(musiccontroller.elapsedTime);
            Debug.Log(timelag);
        }

        deleteData();
    }
    void message(int judge,int lane)//判定を表示する
    {
        if (lane == 0)
        {
            Vector3 textspawnPos1 = new Vector3(-1.5f, line.position.y, 0.15f);
            GameObject text = Instantiate(textsobj[judge], textspawnPos1, Quaternion.Euler(45, 0, 0));
            Destroy(text, 0.5f);
        }
        else if (lane == 1)
        {
            Vector3 textspawnPos2 = new Vector3(1.5f, line.position.y, 0.15f);
            GameObject text = Instantiate(textsobj[judge], textspawnPos2, Quaternion.Euler(45, 0, 0));
            Destroy(text, 0.5f);
        }

    }

    void deleteData()
    {
        musiccontroller.NotesTime.RemoveAt(0);
        musiccontroller.Lanenum.RemoveAt(0);
        musiccontroller.NoteType.RemoveAt(0);
    }

        float GetAbs(float num)
    {
        if(num > 0)
        {
            return num;
        }else
        {
            return -num;
        }
    }
}
