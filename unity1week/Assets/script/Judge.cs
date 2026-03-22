using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Judge : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    private InputAction hitAction;
    [SerializeField] MusicNotesManager musicnoteManager;
    [SerializeField] GameObject[] MessageObj;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] Playersystem player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created  
    // Update is called once per frame

    // 1. アクションを探して参照を取得する
    // "Player"はAction Map名、"hit"はAction名に合わせて変えてください
    [SerializeField] bool dkeyispressed = false;
    [SerializeField] bool fkeyispressed = false;
    void Awake()
    {
        // 1. アクションを探して参照を取得する
        // "Player"はAction Map名、"hit"はAction名に合わせて変えてください
        hitAction = inputActions.FindActionMap("Player").FindAction("hit");
    }
    void OnEnable()
    {
        hitAction.Enable();
    }

    void OnDisable()
    {
        hitAction.Disable();
    }
    void Update()
    {
       

        if (musicnoteManager.LaneNum.Count == 0) return;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.wasPressedThisFrame) dkeyispressed = true;
            if (Keyboard.current.fKey.wasPressedThisFrame) fkeyispressed = true;
        }
        var touch = Touchscreen.current?.primaryTouch;
        float screenHalf = Screen.width / 2f;

        if (touch != null && touch.press.wasPressedThisFrame)
        {
            float touchX = Touchscreen.current.primaryTouch.position.ReadValue().x;
            if (touchX < screenHalf && musicnoteManager.LaneNum[0] == 1)
            {
                Judgement(Mathf.Abs(musicnoteManager.elapsedTime - musicnoteManager.NotesTime[0]));
            }
            else
            {
                if (touchX < screenHalf && musicnoteManager.LaneNum[1] == 1)
                {
                    Judgement(Mathf.Abs(musicnoteManager.elapsedTime - musicnoteManager.NotesTime[1]));
                }
            }

            if (touchX >= screenHalf && musicnoteManager.LaneNum[0] == 0)
            {
                Judgement(Mathf.Abs(musicnoteManager.elapsedTime - musicnoteManager.NotesTime[0]));
            }
            else
            {
                if (touchX >= screenHalf && musicnoteManager.LaneNum[1] == 0)
                {

                    Judgement(Mathf.Abs(musicnoteManager.elapsedTime - musicnoteManager.NotesTime[1]));
                }
            }
        }

        if (dkeyispressed)
        {
            if (musicnoteManager.LaneNum.Count > 0 && musicnoteManager.LaneNum[0] == 1)
            {
                Judgement(Mathf.Abs(musicnoteManager.elapsedTime - musicnoteManager.NotesTime[0]));
                dkeyispressed = false;
            }
            else
            {
                if((musicnoteManager.LaneNum.Count > 1 && musicnoteManager.LaneNum[1] == 1))
                {
                    Judgement(Mathf.Abs(musicnoteManager.elapsedTime - musicnoteManager.NotesTime[1]));
                    dkeyispressed = false;
                }


            }


        }

        if (fkeyispressed)
        {
            if (musicnoteManager.LaneNum[0] == 0)
            {

                Judgement(Mathf.Abs(musicnoteManager.elapsedTime - musicnoteManager.NotesTime[0]));
                fkeyispressed = false;
            }
            else
            {
                if(musicnoteManager.LaneNum[1] == 0)
                {
                    Judgement(Mathf.Abs(musicnoteManager.elapsedTime - musicnoteManager.NotesTime[1]));
                    fkeyispressed = false;
                }

            }

        }
        saymiss();

    }


    void saymiss()
    {
        while (musicnoteManager.LaneNum.Count > 0 &&
     musicnoteManager.elapsedTime > musicnoteManager.NotesTime[0] + 0.40f)
        {

            message(1);       
            deleteData();
            Debug.Log("ミス");


        }
    }
    void deleteData()//すでにたたいたノーツを削除する関数
    {

        if (musicnoteManager.NotesTime.Count == 0) return;

        musicnoteManager.NotesTime.RemoveAt(0);
        musicnoteManager.LaneNum.RemoveAt(0);
        musicnoteManager.NoteType.RemoveAt(0);
    }
    void Judgement(float timelag)
    {
        if (timelag <= 0.25)
        {
            Debug.Log("nice" + timelag);
            message(0);           
            player.Addenergy(10);
            deleteData();
        }
        
    }
    void message(int judge)//判定を表示する
    {
        GameObject text = Instantiate(MessageObj[judge], new Vector3(0, -0.7f, 0.15f), Quaternion.Euler(45, 0, 0));
        Destroy(text, 0.5f);

    }
}


