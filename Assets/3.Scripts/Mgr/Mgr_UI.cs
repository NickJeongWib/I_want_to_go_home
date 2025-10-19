using UnityEngine;

public class Mgr_UI : MonoBehaviour
{
    public static Mgr_UI Inst;

    [SerializeField] Transform UI_Parent;

    [Header("Inventory")]
    [SerializeField] GameObject Inventory_Prefab;
    GameObject Inventory_UI;
    

    void Start()
    {
        #region Singleton
        if (Inst == null)
        {
            Inst = this;
        }
        else
        {
            Destroy(this);
        }
        #endregion  
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            // �κ��丮 ����
            if(Inventory_UI == null)
            {
                GameObject inven = Instantiate(Inventory_Prefab);
                Inventory_UI = inven;
                inven.transform.SetParent(UI_Parent, false);
            }
            // �κ��丮 ���� ���ְ� ��Ȼ��ȭ ���̸�
            else if (Inventory_UI != null && Inventory_UI.activeSelf == false) 
            {
                Inventory_UI.SetActive(true);
            }
            // �κ��丮 ���� ���ְ� Ȼ��ȭ ���̸�
            else if (Inventory_UI != null && Inventory_UI.activeSelf == true)
            {
                Inventory_UI.GetComponent<Animator>().Play("Close");
            }
        }
    }
}
