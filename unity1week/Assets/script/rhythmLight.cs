using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Rendering.Universal;


public class rhythmLight : MonoBehaviour
{
     
    [SerializeField] private int num = 0;
    [SerializeField] private InputActionAsset inputActions;  
    private InputAction hitAction;  
    private SpriteRenderer sprend;
    private float alfa = 0.2f;
    private float timer = 0;
    public float soundinterval = 0f;
    void Awake()
    {
        // 1. アクションを探して参照を取得する
        // "Player"はAction Map名、"hit"はAction名に合わせて変えてください
        hitAction = inputActions.FindActionMap("Player").FindAction("hit");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {    
        sprend = GetComponent<SpriteRenderer>();
        SetAlpha(0f);
       
    }
    void OnEnable()
    {
        hitAction.Enable();
    }

    void OnDisable()
    {
        hitAction.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        bool isAnyKeyPressed = false;
       
        float screenHalf = Screen.width / 2f;
        if (Keyboard.current != null)
        {
            if (num == 1 && Keyboard.current.dKey.isPressed) isAnyKeyPressed = true;
            if (num == 2 && Keyboard.current.fKey.isPressed) isAnyKeyPressed = true;
        }
        // Dキーの判定
        var touch = Touchscreen.current?.primaryTouch;
        if (touch != null && touch.press.isPressed)
        {
            float touchX = Touchscreen.current.primaryTouch.position.ReadValue().x;
            if (touchX < screenHalf && num == 1)
            {
                isAnyKeyPressed = true;
            }

            // Fキーの判定 (QからFに変更しました)
            if (touchX >= screenHalf && num == 2)
            {
                isAnyKeyPressed = true;
            }

            // 判定結果に基づいて色を変える

        }
        if (isAnyKeyPressed)
        {
            timer += Time.deltaTime;
            

            ChangeColor();
        }
        else
        {
            SetAlpha(0f);
        }
    }
    void ChangeColor()
    {
        Color color = sprend.color;
        color.a = alfa; // 0.0 (透明) ～ 1.0 (不透明)
        sprend.color = color;

    }
    void SetAlpha(float targetAlpha)
    {
        Color color = sprend.color;
        color.a = targetAlpha;
        sprend.color = color;
    }
}
