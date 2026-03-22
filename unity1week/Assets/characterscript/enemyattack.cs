using System.Runtime.CompilerServices;
using UnityEngine;

public class enemyattack : MonoBehaviour
{
    public bool isReflected = false;
    [SerializeField] float enemyattackspeed;
    [SerializeField] float targettime;
    [SerializeField] Rigidbody2D rg2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        rg2 = GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    void Update()
    {
        if (!isReflected)
        {
            bossattackmove(enemyattackspeed, 0);
        }
        else
        {
            bossattackmove(-enemyattackspeed, 1);
            transform.localScale = new Vector3(0.58f, -0.5f, 1f);
        }



        if (transform.position.y < -5)
        {
            Destroy(this.gameObject);
        }
    }


    public void bossattackmove(float enemyattack, int z)
    {
        rg2.linearVelocity = new Vector3(0, -enemyattack, z);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyattack") || collision.CompareTag("Notes"))
        {
            if (isReflected)
            {
                Destroy(collision.gameObject);
            }
        }

    }
}
