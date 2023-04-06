using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public ItemName requirItem;

    public bool isDone;

    public void CheckItem(ItemName itemName)
    {
        if(!isDone && itemName == requirItem)
        {
            isDone = true;
            //��⵽ʹ������ȷ����Ʒ��ʹ�ò�����Ʒ���Ƴ�
            OnClickedAction();
            EventHandler.CallItemUsedEvent(itemName); //ȷ����ʹ�ø���Ʒ
        }
    }
    //Ĭ������ȷ����Ʒִ��
    protected virtual void OnClickedAction()
    {

    }
    public virtual void EmptyCliked()  //Ҫ����cursor��ִ��
    {
        Debug.Log("�յ�");
    }

}
