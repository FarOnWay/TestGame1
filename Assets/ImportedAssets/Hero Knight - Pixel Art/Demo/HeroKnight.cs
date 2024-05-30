
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class HeroKnight : Entity
{

    [SerializeField] float m_speed = 4.0f;

    public ItemNameDisplay itemNameDisplay;

    public Item equippedItem;

    public InventoryController inventory;
    [SerializeField] float m_jumpForce = 7.5f;
    [SerializeField] float m_rollForce = 6.0f;
    [SerializeField] bool m_noBlood = false;
    [SerializeField] GameObject m_slideDust;

    RespawnController respawn;

    private Animator m_animator;

    public GameObject hand;
    private Animator handAnimator;



    // public GameObject hand;


    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    private bool m_isWallSliding = false;
    private bool m_grounded = false;
    private bool m_rolling = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;
    private float m_rollDuration = 8.0f / 14.0f;
    private float m_rollCurrentTime;
    bool isShildUpNow = false;


    // public lifeBar lifeBar;

    // public CoinManager cm;
    // public Text lifeUI;
    int velocity;
    // public int maxHealth = 100;
    // public float knockbackForce = 2f;

    LifeManager lifeManager;

    int fallDamage = 0;

    public int damage;

    Vector2 startPos;

    // public int coinAmoint;

    // public enemy enemy;

    // Use this for initialization
    void Start()
    {
        if (handAnimator == null) Debug.Log("handAnimator is null");
        else Debug.Log("handAnimator is not null");

        // health = maxHealth;
        lifeManager = GetComponent<LifeManager>();
        inventory = GetComponent<InventoryController>();
        respawn = GetComponent<RespawnController>();
        startPos = transform.position;
        handAnimator = hand.GetComponent<Animator>();
        // handAnimator.ResetTrigger("Attack");
        // m_animator.ResetTrigger("Attack1");
        // m_animator.ResetTrigger("Attack2");
        // m_animator.ResetTrigger("Attack3");


        // lifeBar.SetMaxHealth(maxHealth);

        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();
    }

    public void EquipItem(Item item)
    {
        equippedItem = item;
        Debug.Log("you equipped " + item.name);

        // Update the player's sprite or model to show the equipped item
        // This will depend on how your sprites or models are set up
        // For example, if you have a separate sprite for each item, you could do something like this:
        //  spriteRenderer.sprite = item.sprite;
    }

    bool ShieldUp(bool isShildUpNow)
    {
        if (isShildUpNow)
        {
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }
        return isShildUpNow;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            // cm.coinCount++;
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {

        if (isShildUpNow) return;

        if (damage >= 1)
        {
            m_animator.SetTrigger("Hurt");
            lifeManager.lifeCount -= damage;

            if (lifeManager.lifeCount <= 0)
            {
                lifeManager.lifeCount = 0;
                m_animator.SetTrigger("Death");
                Respawn();
                //  Destroy(gameObject, 1f);
            }
        }
    }

    void Respawn()
    {
        transform.position = startPos;
    }

    // void DealDamage(int damage)
    // {
    //     Debug.Log("dando dano");
    //     Vector2 hitboxSize = new Vector2(1f, 1f);
    //     Vector2 hitboxCenter = transform.position + new Vector3(1f * m_facingDirection, 0, 0);

    //     Collider2D[] colliders = Physics2D.OverlapBoxAll(hitboxCenter, hitboxSize, 0);

    //     foreach (Collider2D collider in colliders)
    //     {
    //         if (collider.CompareTag("Enemy"))
    //         {
    //             EnemyController enemy = collider.gameObject.GetComponent<EnemyController>();
    //             if (enemy != null)
    //             {
    //                 Debug.Log("sexo");
    //                 enemy.TakeDamage(damage);

    //                 Rigidbody2D enemyRb = collider.gameObject.GetComponent<Rigidbody2D>();
    //                 if (enemyRb != null)
    //                 {
    //                     Debug.Log("sexo 2");

    //                     Vector2 knockbackDirection = (enemyRb.transform.position - transform.position).normalized;
    //                     knockbackDirection = (knockbackDirection + new Vector2(0, 1f)).normalized;
    //                     enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

    //                 }
    //             }
    //         }

    //     }
    // }


    int fallDamageCalc()
    {
        int minimumFallSpeed = -8; //using nevative value because when falling in the Y axxis, the unity uses negative values
        int damageMultiplier = 25;

        if (m_body2d.velocity.y < minimumFallSpeed)
        {
            //Debug.Log("velocidade no y é menor que minimumFallSpeed");
            fallDamage = ((int)-m_body2d.velocity.y - minimumFallSpeed) * damageMultiplier;
            // Debug.Log("Dano de queda " + fallDamage);

            return 0;
        }
        return fallDamage;
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            TakeDamage(fallDamageCalc());
            fallDamage = 0;
        }
        if (other.gameObject.CompareTag("Item"))
        {
            // Get the ItemInstance component of the GameObject
            ItemInstance itemInstance = other.gameObject.GetComponent<ItemInstance>();

            if (itemInstance != null && itemInstance.item != null)
            {
                inventory.CollectItem(itemInstance.item);
                itemNameDisplay.DisplayItemName(itemInstance.item);

            }
            else
            {
                Debug.LogError("The item does not have an ItemInstance component or the item is null.");
            }
            Destroy(other.gameObject);
        }

    }

    // write a method to open the inventoru by clicking tab



    // Update is called once per frame
    void Update()
    {
        fallDamageCalc();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventory.SeeInventory();
        }

        m_timeSinceAttack += Time.deltaTime;

        if (m_rolling)
            m_rollCurrentTime += Time.deltaTime;

        if (m_rollCurrentTime > m_rollDuration)
            m_rolling = false;

        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        float inputX = Input.GetAxis("Horizontal");

        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            m_facingDirection = 1;
        }
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            //   transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            m_facingDirection = -1;
        }

        if (!m_rolling)
            m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

        m_isWallSliding = (m_wallSensorR1.State() && m_wallSensorR2.State()) || (m_wallSensorL1.State() && m_wallSensorL2.State());
        m_animator.SetBool("WallSlide", m_isWallSliding);

        if (Input.GetKeyDown("e") && !m_rolling)
        {
            m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
        }
        else if (Input.GetKeyDown("q") && !m_rolling && ShieldUp(isShildUpNow) == false)
        {
            m_animator.SetTrigger("Hurt");
        }
        else if (Input.GetMouseButtonDown(0) && m_timeSinceAttack > 0.25f && !m_rolling)
        {
            m_animator.SetTrigger("Attack" + m_currentAttack);
            handAnimator.SetTrigger("Attack");

            if (m_facingDirection > 0) base.DealDamage(damage, true, false);

            else base.DealDamage(damage, true, true);

            m_currentAttack++;
            if (m_currentAttack > 3)
                m_currentAttack = 1;

            if (m_timeSinceAttack > 1.0f)
                m_currentAttack = 1;

            m_timeSinceAttack = 0.0f;

            // Ensure to reset triggers to avoid continuous triggering
            // if (handAnimator.GetCurrentAnimatorStateInfo(0).IsName("Atk_HandAnim"))
            // {
            //     handAnimator.ResetTrigger("Attack");
            // }
        }
        else if (Input.GetMouseButtonDown(1) && !m_rolling)
        {
            isShildUpNow = true;
            ShieldUp(isShildUpNow);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            m_animator.SetBool("IdleBlock", false);
            isShildUpNow = false;
        }
        else if (Input.GetKeyDown("left shift") && !m_rolling && !m_isWallSliding && m_grounded)
        {
            m_rolling = true;
            m_animator.SetTrigger("Roll");
            m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
        }
        else if (Input.GetKeyDown("space") && m_grounded && !m_rolling)
        {
            m_animator.SetTrigger("Jump");
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
            m_groundSensor.Disable(0.2f);
        }
        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
        {
            m_delayToIdle = 0.05f;
            m_animator.SetInteger("AnimState", 1);
        }
        else
        {
            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_animator.SetInteger("AnimState", 0);
        }
    }

    // Animation Events
    // Called in slide animation.
    void AE_SlideDust()
    {
        Vector3 spawnPosition;

        if (m_facingDirection == 1)
            spawnPosition = m_wallSensorR2.transform.position;
        else
            spawnPosition = m_wallSensorL2.transform.position;

        if (m_slideDust != null)
        {
            // Set correct arrow spawn position
            GameObject dust = Instantiate(m_slideDust, spawnPosition, gameObject.transform.localRotation) as GameObject;
            // Turn arrow in correct direction
            dust.transform.localScale = new Vector3(m_facingDirection, 1, 1);
        }
    }
}
