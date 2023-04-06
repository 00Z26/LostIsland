using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemImage;
    private ItemDetails currentItem;
    private bool isSelected; //��Ʒ����ĸ���Ʒ��ѡ�е�״̬
    public ItemTooltip tooltip;

    public void SetItem(ItemDetails itemDetails) //������ʾ��Ʒ�ķ���������λ�ô��β�����
    {
        currentItem = itemDetails;
        this.gameObject.SetActive(true);
        itemImage.sprite = itemDetails.itemSprite;
        //����Ϊԭ�гߴ�
        //itemImage.SetNativeSize();  

    }

    public void SetEmpty()
    {
        this.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) //���
    {
        isSelected = !isSelected; //�ı䱻ѡ��״̬ 
        Debug.Log(isSelected);
        EventHandler.CallItemSelectedEvent(currentItem, isSelected);
    }

    public void OnPointerEnter(PointerEventData eventData) //��껬��
    {
        if(this.gameObject.activeInHierarchy) //slotUI������ʾ��Ʒ
        {
            tooltip.gameObject.SetActive(true);
            tooltip.UpdateItemName(currentItem.itemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData) //��껬��
    {
        tooltip?.gameObject.SetActive(false);  
    }
}
