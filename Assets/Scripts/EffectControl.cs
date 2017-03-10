using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectControl : MonoBehaviour
{
    /// <summary>
    /// 爆発アニメション終了時の処理
    /// </summary>
    void OnAnimationOver()
    {
        Destroy(gameObject);
    }
}
