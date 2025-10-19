using UnityEngine;

public enum ITEM_TYPE
{

}

[System.Serializable]
public class Item
{
    // ������ �̸�
    [SerializeField] string Item_Name;
    public string Get_Item_Name { get => Item_Name; }

    // ������ Ÿ��
    [SerializeField] ITEM_TYPE ItemType;
    public ITEM_TYPE Get_ItemType { get => ItemType; }

    // ������ ���� ����
    [SerializeField] int Item_Amount;
    public int Get_Item_Amount { get => Item_Amount; set => Item_Amount = value; }

    // ������ ���� ����
    [SerializeField] int Item_SlotIndex;
    public int Get_Item_SlotIndex { get => Item_SlotIndex; set => Item_SlotIndex = value; }

    // ������ ���� ����
    [SerializeField] bool Item_Equip;
    public bool Get_Item_Equip { get => Item_Equip; set => Item_Equip = value; }

    // ������ ����
    [SerializeField] string Item_Desc;
    public string Get_Item_Desc { get => Item_Desc; }

    // ������
    #region Constructor
    public Item(string _name, ITEM_TYPE _itemType, string _itemDesc, int _amount = 0, int _slotIndex = -1, bool _isEquip = false)
    {
        Item_Name = _name;
        ItemType = _itemType;
        Item_Desc = _itemDesc;
        Item_Amount = _amount;
        Item_SlotIndex = _slotIndex;
        Item_Equip = _isEquip;
    }
    #endregion
}
