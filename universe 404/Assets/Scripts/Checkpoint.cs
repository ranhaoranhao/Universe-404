using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 检查点类。玩家死亡后会在这个物体的位置出生，场景重置。
/// </summary>
[RequireComponent(typeof(UniqueId))]
public class Checkpoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var characterLife = collision.gameObject.GetComponent<CharacterLife>();
        if (characterLife != null)
        {
            if (GameManager.instance.LastCheckpoint != "")
            {
                // TODO: 上一个存档点播放停用动画
            }

            GameManager.instance.LastCheckpoint = GetComponent<UniqueId>().uniqueId;

            // TODO: 播放启用动画
        }


    }
}
