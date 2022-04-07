using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基础障碍；玩家碰到即死。
/// </summary>
public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var characterLife = collision.gameObject.GetComponent<CharacterLife>();
        if (characterLife != null)
        {
            characterLife.Hit(gameObject);
        }
    }
}
