using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLife : MonoBehaviour
{
    public void Hit(GameObject hitObject)
    {
        // TODO: 死亡动画


        GameManager.instance.Reload();
    }
}
