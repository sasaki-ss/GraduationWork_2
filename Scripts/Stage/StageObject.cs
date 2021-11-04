using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObject : MonoBehaviour
{
    public bool isOnPlayer { get; set; }    //プレイヤーいるフラグ
    public float destroyPoint { get; set; }   //破壊ポイント
}
