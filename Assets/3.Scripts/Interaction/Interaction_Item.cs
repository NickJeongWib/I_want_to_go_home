using UnityEngine;

public class Interaction_Item : Interaction
{
    void Start()
    {
        // �׽�Ʈ(������ ������ ����)
        int Rand = Random.Range(0, ItemList.Inst.Item_List.Count);
        ItemData = ItemList.Inst.Item_List[Rand];
    }

    public override void OnInteraction(int _amount)
    {
        // Debug.Log(this.ItemData.Get_Item_Name);

        // �κ��丮�� ���� ������ ȹ�� �ڵ�
        // �̹� �����Ѵٸ�
        if (GlobalValue.User_Inventory.ContainsKey(ItemData.Get_Item_Index) == true)
        {
            // ���������� �ƴ϶��
            if(ItemData.Get_ItemType != ITEM_TYPE.EQUIPMENT)
            {
                GlobalValue.User_Inventory[ItemData.Get_Item_Index].Get_Item_Amount += _amount;
            }
            else
            {
                // ���� �������� �����ϱ� 1
                Add_Inventory(1);
            }
        }
        else // �������� ������
        {
            Add_Inventory(_amount);
        }

        // �κ��丮 �ʱ�ȭ
        Mgr_Inventory.Inst.Refresh_Inventory();
    }

    void Add_Inventory(int _amount)
    {
        Item item = new Item(ItemData);

        GlobalValue.User_Inventory.Add(item.Get_Item_Index, item);
        GlobalValue.User_Inventory[ItemData.Get_Item_Index].Get_Item_Amount = _amount;
    }
}
