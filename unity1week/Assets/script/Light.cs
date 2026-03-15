using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;


public class Light : MonoBehaviour
{
    [SerializeField] private float speed = 3;
    [SerializeField] private int num = 0;
    [SerializeField] InputActionAsset inputActions;
    
    private SpriteRenderer sprend;
    private float alfa = 0.2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprend = GetComponent<SpriteRenderer>();
        SetAlpha(0f);
    }

    // Update is called once per frame
    void Update()
    {

        bool isAnyKeyPressed = false;

        // Dキーの判定
        if (Keyboard.current.dKey.isPressed && num == 1)
        {
            isAnyKeyPressed = true;
        }

        // Fキーの判定 (QからFに変更しました)
        if (Keyboard.current.fKey.isPressed && num == 2)
        {
            isAnyKeyPressed = true;
        }

        // 判定結果に基づいて色を変える
        if (isAnyKeyPressed)
        {
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
