using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWallObj : StageObject
{
    private int floorNum;   //è∞ÇÃêî

    private void Awake()
    {
        floorNum = 0;
        isOnPlayer = false;
    }

    private void Start()
    {
        
    }

    public void SetFloorNum(int _val)
    {
        floorNum = _val;
    }
}
