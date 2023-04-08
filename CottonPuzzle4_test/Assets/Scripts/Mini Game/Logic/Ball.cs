using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public BallDetails ballDetails;

    public bool isMatch;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    public void SetupBall(BallDetails ball)
    {
        ballDetails = ball;
        //把球移动到另一个格子的setup时，判断是否是正确的
        if(isMatch)
        {
            SetRight();
        }
        else
        {
            SetWrong();
        }
    }

    public void SetRight()
    {
        spriteRenderer.sprite = ballDetails.rightSprite; 
    }

    public void SetWrong()
    {
        spriteRenderer.sprite = ballDetails.wrongSprite;
    }
}
