using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Playerの動作
public class CharacterAction : MonoBehaviour
{
    static private int hash_hSpeed = Animator.StringToHash("hSpeed");
    static private int hash_vSpeed = Animator.StringToHash("vSpeed");
    static private int hashGroundDistance = Animator.StringToHash("GroundDistance");
    static private int hashIsCrouch = Animator.StringToHash("IsCrouch");
    static private int hashIsAttacking = Animator.StringToHash("IsAttacking");
    static private int hashIsDamegeing = Animator.StringToHash("IsDamegeing");
    static private int hashShoot = Animator.StringToHash("Shoot");
    static private int hashPunch = Animator.StringToHash("Punch");
    static private int hashDamage = Animator.StringToHash("Damage");
    //static private int hashIsDead = Animator.StringToHash("IsDead");

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rig2d;
    private CapsuleCollider2D groundCheckCollider;
    private Vector2 groundCheckPosition;
    private Direction direction;
    private bool isDown;
    private bool isJumping;
    private float flashTimer;
    
    [SerializeField]
    private LayerMask groundMask;       //地面とするLayer
    [SerializeField]
    private int hp = 100;               //Hp
    [SerializeField]
    private float moveSpeed = 2f;       //移動速度
    [SerializeField]
    private float jumpSpeed = 5f;       //Jump力
    [SerializeField]
    private GameObject Bullet;          //撃つ弾のPrefab
    [SerializeField]
    private float bulletSpeed;          //弾の速度
    [SerializeField]
    private Transform ShootPosition;    //弾を出す位置
    [SerializeField]
    private Text HpText;

    //方向
    public enum Direction
    {
        LEFT = -1,
        RIGHT = 1,
    }

    //攻撃の種類
    public enum AttackType
    {
        NOATTACK,
        PUNCH,
        SHOOT,
    }

    void Awake()
    {
        isDown = false;
        isJumping = false;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rig2d = GetComponent<Rigidbody2D>();
        groundCheckCollider = GetComponent<CapsuleCollider2D>();
        direction = Direction.RIGHT;
        HpText.text = "HP : " + hp;
    }

    void Update()
    {
        //方向によって、Scaleを変えて、表示方向を変更する
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (int)direction, transform.localScale.y, transform.localScale.z);

        //地面と接触判定
        groundCheckPosition = new Vector2(transform.position.x + groundCheckCollider.offset.x, transform.position.y + groundCheckCollider.offset.y - groundCheckCollider.size.y / 2);
        var GroundCheckHit = Physics2D.Raycast(groundCheckPosition, Vector3.down, 2, groundMask);

        if(GroundCheckHit.distance > 0.2f || GroundCheckHit.collider == null)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }

        //アニメションパラメーターを変更
        animator.SetBool(hashIsCrouch, isDown);
        animator.SetFloat(hashGroundDistance, GroundCheckHit.collider == null ? 99 : GroundCheckHit.distance);

        //ダメージ中のフラッシュ
        if (animator.GetBool(hashIsDamegeing))
        {
            FlashDraw(0.1f);
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }

    void FixedUpdate()
    {
        if (rig2d.velocity.y < -50)
        {
            rig2d.velocity = new Vector2(rig2d.velocity.x, -50);
        }

        //Rigidbodyに関係があるアニメション
        animator.SetFloat(hash_vSpeed, rig2d.velocity.y);
        animator.SetFloat(hash_hSpeed, Mathf.Abs(rig2d.velocity.x));
    }

    /// <summary>
    /// Playerの行動をコントロールする。Rigidbodyに関係あるので、FixUpdateで実行する
    /// </summary>
    /// <param name="hAxis">横方向操作</param>
    /// <param name="vAxis">縦方向操作</param>
    /// <param name="isJump">ジャンプ操作</param>
    /// <param name="attackType">攻撃操作</param>
    public void Control(float hAxis, float vAxis, bool isJump, AttackType attackType)
    {
        if (animator.GetBool(hashIsAttacking) || animator.GetBool(hashIsDamegeing))
        {
            return;
        }
        MoveControl(hAxis);
        CrouchControl(vAxis);
        JumpControl(isJump);
        AttackControl(attackType);
    }

    /// <summary>
    /// Flashする
    /// </summary>
    /// <param name="deltaTime">Flash間隔</param>
    private void FlashDraw(float deltaTime)
    {
        flashTimer += Time.deltaTime;
        if (flashTimer > deltaTime)
        {
            flashTimer = 0.0f;
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }
    }

    /// <summary>
    /// 移動コントロール
    /// </summary>
    /// <param name="hAxis">横操作</param>
    private void MoveControl(float hAxis)
    {
        rig2d.velocity = new Vector2(hAxis * moveSpeed, rig2d.velocity.y);
        
        if (hAxis > 0)
        {
            direction = Direction.RIGHT;
        }
        else if (hAxis < 0)
        {
            direction = Direction.LEFT;
        }
    }

    /// <summary>
    /// Jumpコントロール
    /// </summary>
    /// <param name="isJump">Jump操作</param>
    private void JumpControl(bool isJump)
    {
        if (isJump)
        {
            if (!isJumping)
            {
                rig2d.velocity = new Vector2(rig2d.velocity.x, jumpSpeed);
            }
        }
    }

    /// <summary>
    /// 攻撃操作
    /// </summary>
    /// <param name="attackType">攻撃種類</param>
    private void AttackControl(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.PUNCH:
                {
                    animator.SetTrigger(hashPunch);
                    rig2d.velocity = new Vector2(0, rig2d.velocity.y);
                    break;
                }
            case AttackType.SHOOT:
                {
                    animator.SetTrigger(hashShoot);
                    rig2d.velocity = new Vector2(0, rig2d.velocity.y);
                    break;
                }
        }
    }

    /// <summary>
    /// しゃがみ操作
    /// </summary>
    /// <param name="vAxis">縦操作</param>
    private void CrouchControl(float vAxis)
    {
        isDown = vAxis < 0;
    }

    /// <summary>
    /// ダメージ受ける
    /// </summary>
    /// <param name="damege">ダメージ量</param>
    /// <param name="damegeFrom">ダメージの方向</param>
    private void Damege(int damege, Vector3 damegeFrom)
    {
        hp -= damege;
        HpText.text = "HP : " + hp;

        if(hp <= 0)
        {
            PlayerPrefs.SetInt("IsClear", 0);
            SceneManager.LoadScene("Ending");
        }

        animator.SetTrigger(hashDamage);
        animator.SetBool(hashIsDamegeing, true);

        if(transform.position.x - damegeFrom.x >= 0)
        {
            direction = Direction.LEFT;
        }
        else
        {
            direction = Direction.RIGHT;
        }

        Vector2 speed = new Vector2(1, 2) * 4;

        speed.x *= -(int)direction;

        rig2d.velocity = speed;
    }

    public Direction GetDirection()
    {
        return direction;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //ダメージをける
            if (!animator.GetBool(hashIsAttacking) && !animator.GetBool(hashIsDamegeing))
            {
                Damege(10, collision.transform.position);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //ダメージをける
            if (!animator.GetBool(hashIsAttacking) && !animator.GetBool(hashIsDamegeing))
            {
                Damege(10, collision.transform.position);
            }
        }
        if (collision.tag == "Goal")
        {
            //ゲームクリア
            PlayerPrefs.SetInt("IsClear", 1);
            SceneManager.LoadScene("Ending");
        }
    }

    /// <summary>
    /// 撃つ時の処理
    /// </summary>
    public void OnShoot()
    {
        GameObject b = Instantiate(Bullet, ShootPosition.position, ShootPosition.rotation);
        b.GetComponent<Rigidbody2D>().velocity = new Vector2((int)direction * bulletSpeed, 0);
    }
}
