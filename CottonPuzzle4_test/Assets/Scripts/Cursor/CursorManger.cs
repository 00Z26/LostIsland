using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManger : MonoBehaviour
{
    private Vector3 mouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    private bool canClick;

    private ItemName currentItem;
    public RectTransform hand;//�������
    private bool holdItem;

    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
        EventHandler.ItemUsedEvent += OnItemUsedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.ItemUsedEvent -= OnItemUsedEvent;
    }



    private void Update()
    {

        canClick = ObjectAtMousePosition();

        if(hand.gameObject.activeInHierarchy)
        {
            hand.position = Input.mousePosition;
        }
        if (InteractiveWithUI()) return;
        if (canClick && Input.GetMouseButtonDown(0))
        {
            //���ڼ�⵽�������л�ҳ�����ײ��Ŀ��ܣ���Ҫ����ײ����б�ǩ�ж�
            ClickAction(ObjectAtMousePosition().gameObject);

        }

    }

    private void OnItemUsedEvent(ItemName obj)
    {
        currentItem = ItemName.None;
        holdItem = false;
        hand.gameObject.SetActive(false);
    }



    private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        holdItem = isSelected;  //�Ƿ����
        if(isSelected) //���л�ȡ��Ʒ��
        {
            currentItem = itemDetails.itemName;
            
        }
        hand.gameObject.SetActive(holdItem);
    }


    private void ClickAction(GameObject clickObject)
    {
        switch (clickObject.tag)
        {
            case "Teleport":
                var teleport = clickObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();
                break;
            case "Item":
                var item = clickObject.GetComponent<Item>();
                item?.ItemClicked();
                break;
            case "Interactive":
                var interactive = clickObject.GetComponent<Interactive>();
                if(holdItem) //��������ʱ�ŵ��ã����bool�ڶ��Ĵ���ʱ�����ı�
                {
                    interactive?.CheckItem(currentItem);
                }
                else
                {
                    interactive?.EmptyCliked(); //δ��������Ŀյ�
                }
                
                break;

        }
        //colliderName = ObjectAtMousePosition();
        //tag = colliderName.tag;
        //if(tag == "Teleport")
        //{
        //    return 1;
        //}
    }
    private Collider2D ObjectAtMousePosition()
    {
        return Physics2D.OverlapPoint(mouseWorldPos); //��ȡ��õ��غϵ���ײ��
    }

    private bool InteractiveWithUI()
    {
        if(EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }
}
