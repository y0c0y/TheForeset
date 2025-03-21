using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{

    //속도를 바꿀 그릇
    public float moveSpeed = 2.0f;

    //달릴때 속도
    public const float runningspeed = 4.0f;

    //느려졌을때 속도
    public const float slowspeed = 0.5f;

    //기본속도
    public const float normalspeed = 1.5f;
    
    //회전방향 담는 그릇
    private float rotateX;
    private float rotateY;

    //마우스 감도 조절
    public float mouseSensitivity = 150f;

    // 카메라 할댱칸
    public Transform cam;

    //리지드바디 대신 캐릭터컨트롤러로 움직일 예정
    private CharacterController controller;

    //열거형으로 이동속도모드 변경
    public enum MoveMode
    {
        Move,
        Running,
        Slow,
    }
    
    //이동속도 변경 매소드 (안에 값은 바꿔야함)
    public void speed(MoveMode mode)
    {
        if (mode == MoveMode.Running)
        {
            moveSpeed = runningspeed;
        }
        else if (mode == MoveMode.Slow)
        {
            moveSpeed = slowspeed;
        }
        else if (mode == MoveMode.Move)
        {
            moveSpeed = normalspeed;
        }
    }
    
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    
    void Start()
    {
        //게임 시작시 마우스 커서를 가운데 고정 후 안보이게 설정
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    void Update()
    {

        //키보드 방향키 입력정보를 가져옴
        var v = Input.GetAxisRaw("Vertical");
        var h = Input.GetAxisRaw("Horizontal");
        
        // 마우스 입력정보를 가져옴 (감도 조절 가능 (아래 함수 참고 ))
        var mousex = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        var mousey = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        //유니티는 회전축을 기준으로 회전하기 때문에 좌우로 움직이려면 y축 회전
        //위아래는 x축 기준 회전이기 때문에 이렇게 매치해줘야하고
        // -/+를 넣는 이유는 오른손 좌표계 때문이다.
        rotateX -= mousey;
        rotateY += mousex;
        
        //플레이어 회전 y축 기준
        transform.rotation = Quaternion.Euler(0f, rotateY, 0);
        
        //카메라 회전 x축 기준
        cam.localRotation = Quaternion.Euler(rotateX, 0f, 0f);
        
        //카메라 회전 제한 (90)
        rotateX = Mathf.Clamp(rotateX, -90f, 90f);

        // 아무 방향키도 입력받지 않으면 이동하지 않고 종료
        //Mathf.Approximately가 근사치를 뽑아 비교해주는? 메소드이고 =! 를 쓰면 값이 뒤집혀 들어간다
        bool isidle = (Mathf.Approximately(h, 0f) && Mathf.Approximately(v, 0f));
        if (isidle)
            return;

        //플레이어가 로컬축 기준으로 이동
        var movedir = new Vector3(h, 0, v).normalized;
        controller.Move(transform.rotation * (moveSpeed * Time.deltaTime * movedir));

        //시프트 키가 입력될때 트루를 반환하는 코드 작성중
        bool isRunning = (Input.GetKey(KeyCode.LeftShift));
        
        //달릴때 필요한 키 입력받고 출력하는 이프문 (안에 값 변경 필요)
        if (isRunning)
        {
            speed(MoveMode.Running);
        }
        else
        {
            speed(MoveMode.Move);
        }
    }
}

