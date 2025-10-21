using UnityEngine;

public enum ITEM_TYPE
{
    INGREDIENT,
    FOOD,
    EQUIPMENT
}

[System.Serializable]
public class Item
{
    [SerializeField] int Item_Index;
    public int Get_Item_Index { get => Item_Index; }

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

    // ������ ������
    [SerializeField] Sprite Item_Icon;
    public Sprite Get_Item_Icon { get => Item_Icon; }

    // ������
    #region Constructor
    public Item(int _index, string _name, ITEM_TYPE _itemType, string _itemDesc, int _amount, string _iconPath, int _slotIndex = -1, bool _isEquip = false)
    {
        Item_Index = _index;
        Item_Name = _name;
        ItemType = _itemType;
        Item_Desc = _itemDesc;
        Item_Amount = _amount;
        Item_SlotIndex = _slotIndex;
        Item_Equip = _isEquip;

        Item_Icon = Resources.Load<Sprite>(_iconPath);
    }

    public Item(Item _item)
    {
        Item_Index = _item.Item_Index;
        Item_Name = _item.Item_Name;
        ItemType = _item.ItemType;
        Item_Desc = _item.Item_Desc;
        Item_Amount = _item.Item_Amount;
        Item_SlotIndex = _item.Item_SlotIndex;
        Item_Equip = _item.Item_Equip;

        Item_Icon = _item.Item_Icon;
    }
    #endregion
}
