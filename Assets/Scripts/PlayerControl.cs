using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Playerをコントロールする
public class PlayerControl : MonoBehaviour
{
    private CharacterAction actionScript;
    private Vector2 contorl;
    private CharacterAction.AttackType at;
    private bool isJump;

    void Start()
    {
        actionScript = GetComponent<CharacterAction>();
    }

    void Update()
    {
        if (CameraScript.isStart)
        {
            //Inputから入力
            contorl = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            at = CharacterAction.AttackType.NOATTACK;

            if (Input.GetButtonDown("Fire1"))
            {
                at = CharacterAction.AttackType.PUNCH;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                at = CharacterAction.AttackType.SHOOT;
            }

            if (Input.GetButtonDown("Jump"))
            {
                isJump = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (CameraScript.isStart)
        {
            //コントロールする
            actionScript.Control(contorl.x, contorl.y, isJump, at);
            isJump = false;
        }
    }
}
