using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : Interactive
{
    public bool isEmpty;
    public BallName matchBall; //��ǰ�ĸ�����Ӧ�����ĸ���
    public Ball currentBall;//��ǰ��������Ϊ��������Ǹ���

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

    public override void EmptyCliked()  //Ҫ��ô��תִ�е�����
    {
        foreach (var holder in linkHolders) //�����ž���Ҫ�����������ԭ��hash�ı���Ҫ��һ��
            if(holder.isEmpty)
            {
                //�����ƹ�ȥ��ע����Ϊ�����壬��Ҫ�޸ĸ���Ȼ���ƶ�
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);

                //������
                holder.CheckBall(currentBall);
                this.currentBall = null;

                //�ı�״̬
                this.isEmpty = true;
                holder.isEmpty = false;

                EventHandler.CallCheckGameStateEvent(); 
            }
    }

}
