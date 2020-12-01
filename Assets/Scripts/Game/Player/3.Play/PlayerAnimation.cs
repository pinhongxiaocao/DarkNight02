using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMove move;
    private Animation anim;
    private void Start()
    {
        move = this.GetComponent<PlayerMove>();
        anim = GetComponent<Animation>();
    }
    /// <summary>
    /// 这里来确保每次都能跟到PlayerMove的状态
    /// </summary>
    private void LateUpdate()
    {
        if (move.state == E_PLAYER_STATE.Moving) 
        {
            PlayAnim("Walk");
        }
        else if(move.state == E_PLAYER_STATE.Idle)
        {
            PlayAnim("Idle");
        }
    }

    void PlayAnim(string animName) 
    {
        anim.CrossFade(animName);
    }
}
