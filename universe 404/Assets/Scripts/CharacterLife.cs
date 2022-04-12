using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CharacterLife : MonoBehaviour
{
    public void Hit(GameObject hitObject)
    {

        // TODO: 死亡动画
        GameManager.instance.Reload();
        PlayerController2D.isDead = false;
    }
  
}
