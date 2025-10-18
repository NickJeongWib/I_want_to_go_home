using UnityEngine;


public class Player_Ctrl : MonoBehaviour
{
    Vector2 Move = Vector2.zero; // �Է��� ���� ����
    public float Speed;                  // �̵� ����
    public float MoveSpeed = 4.0f;       // �ȱ� �ӵ�

    float TargetRotation = 0.0f;  // ȸ�� Ÿ�� ����
    float RotationVelocity;       // ȸ�� �ӵ�

    [Range(0.0f, 0.3f)]
    float RotationSmoothTime = 0.12f;    // ȸ���� õõ�� ���� ���
    float SpeedChangeRate = 10.0f;   // �ӵ� ��ȭ��


    // �ִϸ��̼�
    [Header("Animator")]
    Animator m_Animator;
    float AnimationBlend;      // �̵��� �ִϸ��̼� ����


    // ī�޶�
    GameObject CameraTargetRoot;        // ī�޶� �ٶ� Ÿ��

    float CinemachineTargetYaw = 0;
    float CinemachineTargetPitch = 0;

    float TopClamp = 70.0f;
    float BottomClamp = -20.0f;



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
        // �׽�Ʈ
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CharMove();
    }

    void LateUpdate()
    {
        CameraRotation();
    }

    // ������
    void CharMove()
    {
        // ��ǲ�ý��ۿ��� Vector2�� ��������
        Move = Mgr_Input.Inst.InputMove;


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
            AnimationBlend = Mathf.Lerp(AnimationBlend, Move != Vector2.zero ? 1 : 0, Time.deltaTime * SpeedChangeRate);
            if (AnimationBlend < 0.01f) AnimationBlend = 0f;

            m_Animator.SetFloat("Move", AnimationBlend);
        }
    }

    // ī�޶� ȸ��
    void CameraRotation()
    {
        Vector2 look = Mgr_Input.Inst.InputLook;

        if (look.sqrMagnitude >= 0.01f)
        {
            CinemachineTargetYaw += look.x;
            CinemachineTargetPitch += look.y;
        }

        CinemachineTargetYaw = ClampAngle(CinemachineTargetYaw, float.MinValue, float.MaxValue);
        CinemachineTargetPitch = ClampAngle(CinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CameraTargetRoot.transform.rotation = Quaternion.Euler(CinemachineTargetPitch + 0.0f,
            CinemachineTargetYaw, 0.0f);
    }

    public static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }


}
