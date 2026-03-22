using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] InputActionAsset inputactions;
    [SerializeField] Slider energyslider;
    [SerializeField] int maxenergy = 100;
    [SerializeField] int currentenergy;
    [SerializeField] int damageAmount = 10;
    [SerializeField] private BoxCollider2D bodyCollider;   // 四角をアサイン
    [SerializeField] private CircleCollider2D weaponCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool positionside = false;
    float targetpos;
    // Update is called once per frame
    private void Start()
    {
        if (energyslider != null)
        {
            energyslider.maxValue = maxenergy;
            energyslider.value = currentenergy;
        }
    }
    public void OnRightClick(InputValue value)
    {
        if (!value.isPressed) return;

        positionside = !positionside;

        if (positionside)
        {
            targetpos = 1.5f;
        }
        else
        {
            targetpos = -1.5f;
        }

        Vector3 currentPos = transform.position;
        transform.position = new Vector3(targetpos, currentPos.y, currentPos.z);
    }
   

   

    void OnTriggerEnter2D(Collider2D other)
    {      
        if (other.CompareTag("enemyattack"))
        {
            // 相手（Player）が持っている Health スクリプトを取得
            Health playerHealth = this.GetComponent<Health>();
            if (bodyCollider.IsTouching(other))
            {
                playerHealth.TakeDamage(damageAmount);
                Destroy(other.gameObject);
            }         
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("enemyattack"))
        {          
            if (weaponCollider.IsTouching(other))
            {
                if (Mouse.current.leftButton.isPressed)
                {
                    if (currentenergy >= 100)
                    {
                        enemyattack ea = other.GetComponent<enemyattack>();
                        ea.isReflected = true;                    
                        currentenergy = 0;

                    }


                }
            }
        }
    }
  
      
    


    public void Addenergy(int giveenergy)
    {
        currentenergy += giveenergy;
        energyslider.value = currentenergy;
    }


}


