using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player_Ctrl : NetworkBehaviour
{
    #region InPut
    Vector2 InputMove; // �Է��� ���� ����
    Vector2 InputLook; // �Է��� ���� ����
    #endregion

    #region Move
    Vector2 Move = Vector2.zero; 
    public float Speed;                  // �̵� ����
    public float MoveSpeed = 4.0f;       // �ȱ� �ӵ�

    float TargetRotation = 0.0f;  // ȸ�� Ÿ�� ����
    float RotationVelocity;       // ȸ�� �ӵ�

    [Range(0.0f, 0.3f)]
    float RotationSmoothTime = 0.12f;    // ȸ���� õõ�� ���� ���
    float SpeedChangeRate = 10.0f;   // �ӵ� ��ȭ��

    float AnimationMoveBlend;      // �̵��� �ִϸ��̼� ����
    #endregion



    // �ִϸ��̼�
    [Header("Animator")]
    Animator m_Animator;
    


    // ī�޶�
    GameObject CameraTargetRoot;        // ī�޶� �ٶ� Ÿ��

    float CinemachineTargetYaw = 0;
    float CinemachineTargetPitch = 0;

    float TopClamp = 70.0f;
    float BottomClamp = -20.0f;



    // ��ȣ�ۿ�
    [SerializeField] List<Interaction> InteractionList = new List<Interaction>();
    public List<Interaction> Get_InteractionList { get => InteractionList; }

    // �׽�Ʈ ������
    [SerializeField] GameObject testPrefab;

    protected CharacterController Controller;

    void Awake()
    {
        // ������Ʈ
        Controller = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();

        CameraTargetRoot = GameObject.Find("CameraTargetRoot");

    }

    void Start()
    {
        
    }

    void Update()
    {
        // ���� �Է�
        InputMove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        CharMove();

        if(Input.GetKeyDown(KeyCode.F))
        {
            if (InteractionList.Count <= 0) return;

            if (40 <= GlobalValue.User_Inventory.Count) return;

            // ȹ�� ����
            int get_Amount = 0;
            for (int i = 0; i < UI_ObjPool.Inst.Interact_UI_List.Count; i++)
            {
                if (InteractionList[0].ItemData.Get_Item_Index ==
                    UI_ObjPool.Inst.Interact_UI_List[i].Get_interaction.ItemData.Get_Item_Index)
                {
                    // ȹ�� ���� ����
                    get_Amount = UI_ObjPool.Inst.Interact_UI_List[i].Get_ItemAmount;
                    // ȹ�������� UI ��Ȱ��ȭ
                    UI_ObjPool.Inst.Interact_UI_List[i].gameObject.SetActive(false);
                    break;
                }
            }

            InteractionList[0].OnInteraction(get_Amount);

            Item GetItem = InteractionList[0].ItemData;
            // InteractionList.RemoveAt(0);

            int index = 0;
            while (0 < get_Amount)
            {
                // ��ȣ�ۿ�� ������Ʈ �߿��� ȹ���� �����۰� ������ ������Ʈ���
                if(InteractionList[index].ItemData.Get_Item_Index == GetItem.Get_Item_Index)
                {
                    get_Amount--;
                    GameObject removeItem = InteractionList[index].gameObject;
                    InteractionList.RemoveAt(index);
                    Destroy(removeItem);
                    continue;
                }

                index++;
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject obj = Instantiate(testPrefab);
        }
    }

    void LateUpdate()
    {
        // ���콺 �Է�
        InputLook = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        CameraRotation();
    }

    // ������
    void CharMove()
    {
        // ��ǲ�ý��ۿ��� Vector2�� ��������
        Move = InputMove;


        // �ӵ� ����
        float targetSpeed = 0.0f;

        // �Է��� ������� ��� �ӵ��� ����
        if (Move != Vector2.zero)
        {
            targetSpeed = MoveSpeed;
        }
        else
            targetSpeed = 0.0f;

        Speed = Mathf.Lerp(Speed, targetSpeed, Time.deltaTime * SpeedChangeRate);
        Speed = Mathf.Round(Speed * 1000f) / 1000f;


        // ��ֶ�����
        Vector3 inputDirection = new Vector3(Move.x, 0.0f, Move.y).normalized;

        //�̵� �Է��� �ִ� ��� �÷��̾ �̵��� �� ȸ��
        if (Move != Vector2.zero)
        {
            TargetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              Camera.main.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetRotation, ref RotationVelocity,
                RotationSmoothTime);


            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, TargetRotation, 0.0f) * Vector3.forward;

        // �̵�
        Controller.Move(targetDirection.normalized * (Speed * Time.deltaTime));

        // �ִϸ��̼�
        if (m_Animator)
        {
            AnimationMoveBlend = Mathf.Lerp(AnimationMoveBlend, Move != Vector2.zero ? 1 : 0, Time.deltaTime * SpeedChangeRate);
            if (AnimationMoveBlend < 0.01f) AnimationMoveBlend = 0f;

            m_Animator.SetFloat("Move", AnimationMoveBlend);
        }
    }

    // ī�޶� ȸ��
    void CameraRotation()
    {
        Vector2 look = InputLook;

        if (look.sqrMagnitude >= 0.01f)
        {
            CinemachineTargetYaw += look.x;
            CinemachineTargetPitch += look.y;
        }

        CinemachineTargetYaw = GlobalValue.ClampAngle(CinemachineTargetYaw, float.MinValue, float.MaxValue);
        CinemachineTargetPitch = GlobalValue.ClampAngle(CinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CameraTargetRoot.transform.rotation = Quaternion.Euler(CinemachineTargetPitch + 0.0f,
            CinemachineTargetYaw, 0.0f);
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Interaction") return;
        Interaction interaction = other.gameObject.GetComponent<Interaction>();
        if (interaction == null) return;

        foreach (Interaction list in InteractionList)
        {
            if (list == interaction) return;
        }

        bool isActive = false;
        // �̹� UI�� �� �ִٸ� ���� ����
        for(int i = 0; i < UI_ObjPool.Inst.Interact_UI_List.Count; i++)
        {
            // ��ȣ�ۿ� UI �̹� Ȱ��ȭ && ��ȣ�ۿ� �� �������� �̹� Ȱ��ȭ ���ִ� UI��
            if(UI_ObjPool.Inst.Interact_UI_List[i].gameObject.activeSelf == true
                && UI_ObjPool.Inst.Interact_UI_List[i].Get_interaction.ItemData.Get_Item_Index == interaction.ItemData.Get_Item_Index &&
                 UI_ObjPool.Inst.Interact_UI_List[i].Get_interaction.ItemData.Get_ItemType != ITEM_TYPE.EQUIPMENT)
            {
                UI_ObjPool.Inst.Interact_UI_List[i].Set_Amount(interaction.ItemData.Get_Item_Amount);
                isActive = true;
                break;
            }
        }
        //

        if(isActive == false)
        {
            // ������ UI ����
            UI_ObjPool.Inst.Get_Interact_UI(interaction.ItemData, interaction);
        }

        InteractionList.Add(interaction);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Interaction") return;
        Interaction interaction = other.gameObject.GetComponent<Interaction>();
        if (interaction == null) return;

        // �̹� UI�� �� �ִٸ� ���� ����
        for (int i = 0; i < UI_ObjPool.Inst.Interact_UI_List.Count; i++)
        {
            if (UI_ObjPool.Inst.Interact_UI_List[i].gameObject.activeSelf == true
                && UI_ObjPool.Inst.Interact_UI_List[i].Get_interaction.ItemData.Get_Item_Index == interaction.ItemData.Get_Item_Index)
            {
                UI_ObjPool.Inst.Interact_UI_List[i].Set_Amount(-interaction.ItemData.Get_Item_Amount);
                break;
            }
        }
        //

        for (int i = 0; i < UI_ObjPool.Inst.Interact_UI_List.Count; i++)
        {
            if(interaction == UI_ObjPool.Inst.Interact_UI_List[i].Get_interaction)
            {
                UI_ObjPool.Inst.Interact_UI_List[i].gameObject.SetActive(false);
                break;
            }
        }

        InteractionList.Remove(interaction);
    }


}
