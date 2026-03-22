using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHp = 100;
    private int currentHp;
    private Animator anim;
    // HPバーを表示するためのSlider（任意）
    public Slider hpSlider;
    public GameObject targetObject;
    public GameObject backtitle;
    void Start()
    {
        currentHp = maxHp;
        anim = GetComponent<Animator>();

        // UIの初期設定
        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHp;
            hpSlider.value = currentHp;
        }
    }

    // ダメージを受ける関数
    public void TakeDamage(int damage)
    {
       
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp); // 0〜最大値の間に収める
       
        // UIの更新
        if (hpSlider != null)
        {
            hpSlider.value = currentHp;
        }

        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("死亡しました");
        anim.SetBool("playerdeath", true);
        targetObject.SetActive(true);
        backtitle.SetActive(true);
        // ここに破壊演出などを書く

    }
}
