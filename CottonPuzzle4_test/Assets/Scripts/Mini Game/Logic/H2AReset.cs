using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using DG.Tweening 

public class H2AReset : Interactive
{
    private Transform gearSprite;
    private void Awake()
    {
        gearSprite = transform.GetChild(0); 
    }

    public override void EmptyCliked()
    {
        //÷ÿ÷√”Œœ∑
        //gearSprite.DOPunchRotation(Vector3.forward * 180, 1, 1, 0);
        GameController.Instance.ResetGame();
    }
}
