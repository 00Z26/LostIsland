using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IventoryUI : MonoBehaviour
{
  //�����������½ǵĲ���
  //�һ�д����ȡ�����֣�ͼƬ��manager�л����������Ȼ�󵥶�дһ���ࡣ

    public Button leftButton, rightButton;
    public int currentIndex; //��ǰ��ʾ����Ʒ���
    public SlotUI slotUI;
    private void OnEnable()
    {
        EventHandler.updateUIEvent += OnUpdateUIEvent;
        
    }
    private void OnDisable()
    {
        EventHandler.updateUIEvent -= OnUpdateUIEvent;

    }

    private void OnUpdateUIEvent(ItemDetails itemDetails, int index)
    {
        if(itemDetails == null)
        {
            slotUI.SetEmpty();
            currentIndex = -1;
            leftButton.interactable = false;
            rightButton.interactable = false;
        }
        else
        {
            currentIndex = index;
            slotUI.SetItem(itemDetails);

            if(index > 0)
            {
                leftButton.interactable = true; //��ӵ������µģ�����ֻ������ఴť�Ϳ���
            }
            if(index == -1)
            {
                leftButton.interactable = false;
                rightButton.interactable = false;
            }
        }



    }

    public void SwitchItem(int amount) //amountΪ������
    {
        var index = currentIndex + amount; //currentIndex������OnUpdateUIEvent����ֵ����ǰ��ʾ����Ʒһ�������µ���Ʒ��������current

        if (index < currentIndex) //����ָ�ƶ���Ľ����Ҳ�������list����βֵ���бȽϣ�
        {
            leftButton.interactable = false;
            rightButton.interactable = true;
        }
        else if(index > currentIndex) //�ƶ�����������壬�ұ�û��
        {
            leftButton.interactable = true;
            rightButton.interactable = false;
        }
        else //����������������
        {
            leftButton.interactable = true;
            rightButton.interactable = true;
        }
        //�����¼�����manager��sprite
        EventHandler.CallChangeItemEvent(index);
    }

}
