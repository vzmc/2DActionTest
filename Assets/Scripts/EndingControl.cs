using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingControl : MonoBehaviour
{
    public Text titleText;
    private Animator animator;

    // Use this for initialization
    void Awake()
    {
        animator = GetComponent<Animator>();

        if(PlayerPrefs.GetInt("IsClear") == 1)
        {
            animator.SetBool("IsClear", true);
            titleText.text = "Game Clear";
            titleText.color = Color.green;
        }
        else
        {
            animator.SetBool("IsClear", false);
            titleText.text = "Game Over";
            titleText.color = Color.red;
        }
    }

}
