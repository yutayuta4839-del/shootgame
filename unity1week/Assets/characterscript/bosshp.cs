
using UnityEngine;
using UnityEngine.UI;

public class bosshp : MonoBehaviour
{
    public int bossmaxHp = 100;
    private int bosscurrentHp;
    [SerializeField] int bossdamage;
    // HPバーを表示するためのSlider（任意）
    public Slider bosshpSlider;
    private bool isdeath = false;
    private Animator anim;
    public GameObject clearObject;
    public GameObject cleartitle;
    void Start()
    {
        anim = GetComponent<Animator>();
        bosscurrentHp = bossmaxHp;

        // UIの初期設定
        if (bosshpSlider != null)
        {
            bosshpSlider.maxValue = bossmaxHp;
            bosshpSlider.value = bosscurrentHp;
        }
    }

    // ダメージを受ける関数
    public void BossTakeDamage(int bossdamageamount)
    {
        bosscurrentHp -= bossdamageamount;
        bosscurrentHp = Mathf.Clamp(bosscurrentHp, 0, bossmaxHp); // 0〜最大値の間に収める
        Debug.Log("現在のHP: " + bosscurrentHp);
        // UIの更新
        if (bosshpSlider != null)
        {
            bosshpSlider.value = bosscurrentHp;
        }

        if (bosscurrentHp <= 0)
        {
            clearObject.SetActive(true);
            cleartitle.SetActive(true);
            BossDie();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemyattack"))
        {
          
            enemyattack eaa = other.GetComponent<enemyattack>();
            if (eaa.isReflected)
            {
                BossTakeDamage(bossdamage);
                Destroy(other.gameObject);
            }
        }
    }



    void BossDie()
    {
        Debug.Log("死亡しました");
        // ここに破壊演出などを書く
        anim.SetBool("bossdeath", true);
       
        
    }
}
