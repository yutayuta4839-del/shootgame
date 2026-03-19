using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Judge : MonoBehaviour
{
    [SerializeField] MusicNotesManager musicnoteManager;
    [SerializeField] GameObject[] MessageObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created  
    // Update is called once per frame
    void Update()
    {
        if (musicnoteManager.LaneNum.Count == 0) return;

       
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            Debug.Log($"{musicnoteManager.elapsedTime},—\’èŠÔ: {musicnoteManager.NotesTime[0]}");
            if (musicnoteManager.LaneNum[0] == 1)
            {
                Debug.Log(musicnoteManager.elapsedTime - musicnoteManager.NotesTime[0]);
                Judgement(Mathf.Abs(musicnoteManager.elapsedTime - musicnoteManager.NotesTime[0]));
            }
            return;

        }
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            Debug.Log($" {musicnoteManager.elapsedTime}, —\’èŠÔ: {musicnoteManager.NotesTime[0]}");

            if (musicnoteManager.LaneNum[0] == 0)
            {
                Debug.Log(musicnoteManager.elapsedTime - musicnoteManager.NotesTime[0]);
                Judgement(Mathf.Abs(musicnoteManager.elapsedTime - musicnoteManager.NotesTime[0]));

            }
        }
        saymiss();

        void Judgement(float timelag)
        {
            if (timelag <= 0.15)
            {

                Debug.Log("nice" + timelag);
                message(0);
                deleteData();

            }
            else if (timelag <= 0.25)
            {
                message(2);
                deleteData();
            }



        }

    }
    void saymiss()
    {
        while (musicnoteManager.LaneNum.Count > 0 &&
     musicnoteManager.elapsedTime > musicnoteManager.NotesTime[0] + 0.30f)
        {

            message(1);
            deleteData();
            Debug.Log("ƒ~ƒX");


        }
    }
    void deleteData()//‚·‚Å‚É‚½‚½‚¢‚½ƒm[ƒc‚ğíœ‚·‚éŠÖ”
    {

        if (musicnoteManager.NotesTime.Count == 0) return;

        musicnoteManager.NotesTime.RemoveAt(0);
        musicnoteManager.LaneNum.RemoveAt(0);
        musicnoteManager.NoteType.RemoveAt(0);



    }
    void message(int judge)//”»’è‚ğ•\¦‚·‚é
    {
        GameObject text = Instantiate(MessageObj[judge], new Vector3(0, -0.7f, 0.15f), Quaternion.Euler(45, 0, 0));
        Destroy(text, 0.5f);

    }
}


