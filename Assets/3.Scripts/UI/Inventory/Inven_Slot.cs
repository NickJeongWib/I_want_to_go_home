using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inven_Slot : MonoBehaviour, ISlot
{
    [SerializeField] int SlotNum;
    [SerializeField] Image Item_Icon;
    [SerializeField] Item ItemData;
    [SerializeField] TextMeshProUGUI Item_Amount;

    // ���� �ε��� �ο�
    void ISlot.Set_SlotNum(int num)
    {
        SlotNum = num;
    }

    // ���� ������ ���� ����
    public void Set_SlotInfo(Item _item, int _amount)
    {
        ItemData = _item;
        Item_Icon.sprite = _item.Get_Item_Icon;

        if(_item.Get_ItemType != ITEM_TYPE.EQUIPMENT)
        {
            Item_Amount.text = $"x{_amount}";
        }
    }
}
