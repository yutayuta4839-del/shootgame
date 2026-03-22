using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;


public class Playersystem : MonoBehaviour
{

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
    [SerializeField] private string reflectActionName = "Attack";
    private InputAction reflectAction;
    private enemyattack currentEnemyAttack;
    [SerializeField] private InputActionAsset inputActions;
    private Animator anim;
    
    void Start()
    {
        reflectAction = inputActions.FindAction(reflectActionName);
        if (energyslider != null)
        {
            energyslider.maxValue = maxenergy;
            energyslider.value = currentenergy;
        }

        anim = GetComponent<Animator>();
    }

   
     

       
    
    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            attackreflect();
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {           
            Move();
        }
    }

    public void OnMoveButtonClick()
    {
        Move();
    }

    public void OnReflectAction()
    {
        attackreflect();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("enemyattack"))
        {
            // 相手（Player）が持っている Health スクリプトを取得
            Health playerHealth = this.GetComponent<Health>();
            if (bodyCollider.IsTouching(other))
            {
                anim.SetTrigger("takedamage");
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
                 currentEnemyAttack = other.GetComponent<enemyattack>();
             }
                
                                                                                                      
        }
    }

    public void Addenergy(int giveenergy)
    {
        currentenergy += giveenergy;
        energyslider.value = currentenergy;
    }
    public void Move()
    {
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

    public void attackreflect()
    {
        anim.SetTrigger("swing");
        if (currentenergy >= 100)
        {
            if (currentEnemyAttack != null)
            {
                currentEnemyAttack.isReflected = true;
                currentenergy = 0;
                currentEnemyAttack = null;
            }
            
        }

     }
}


