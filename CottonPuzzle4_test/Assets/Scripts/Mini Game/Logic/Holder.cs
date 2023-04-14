using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : Interactive
{
    public bool isEmpty;
    public BallName matchBall; //当前的格子内应该是哪个球
    public Ball currentBall;//当前格子内作为子物体的那个球

    public HashSet<Holder> linkHolders = new HashSet<Holder>();

    public void CheckBall(Ball ball)
    {
        currentBall = ball;
        if (ball.ballDetails.ballName == matchBall)
        {
            currentBall.isMatch = true;
            currentBall.SetRight();
        }
        else
        {
            currentBall.isMatch = false;
            currentBall.SetWrong();
        }
    }

    public override void EmptyCliked()  //要怎么跳转执行到这里
    {
        foreach (var holder in linkHolders) //这个大概就是要正反存两遍的原因，hash的遍历要查一下
            if(holder.isEmpty)
            {
                //把球移过去，注意球为子物体，需要修改父级然后移动
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);

                //交换球
                holder.CheckBall(currentBall);
                this.currentBall = null;

                //改变状态
                this.isEmpty = true;
                holder.isEmpty = false;

                EventHandler.CallCheckGameStateEvent(); 
            }
    }

}
