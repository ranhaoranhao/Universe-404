using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 宇宙碎片类。玩家可以收集的道具，收集后死亡也保存进度。
/// </summary>

[RequireComponent(typeof(UniqueId))]
public class Shard : MonoBehaviour
{
    private bool Collected = false;

    /// <summary>
    /// 将碎片设置为已经收集的状态。在这个状态下，碎片不可见。
    /// </summary>
    /// <param name="noAnim">若为真，不播放收集动画。</param>
    public void SetCollected(bool noAnim)
    {
        if (!noAnim)
        {
            // TODO: 收集动画
        }
        Collected = true;

        // TODO: 动画结束后碎片消失
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Collected)
            return;

        var characterLife = collision.gameObject.GetComponent<CharacterLife>();
        if (characterLife != null)
        {
            GameManager.instance.CollectShard(this);
            SetCollected(false);
        }
    }
    
}