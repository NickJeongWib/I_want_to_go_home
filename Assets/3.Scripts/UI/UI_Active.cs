using UnityEngine;

public class UI_Active : MonoBehaviour
{
    // �ִϸ��̼� �̺�Ʈ �Լ�
    #region UI_Active/Deactive
    public void Active_UI()
    {
        this.gameObject.SetActive(true);
    }

    public void Deactive_UI()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
}
