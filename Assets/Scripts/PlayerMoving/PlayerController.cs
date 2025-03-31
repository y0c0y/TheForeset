using System.Collections;
using Unity.VisualScripting;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    public PlayerController Instance { get; private set; }
    
    public MoveMode moveMode;
    public bool isStunning;
    private bool isChangingMoveMode = false; // 코루틴 실행 여부 체크
    
    //원래 속도 값
    public float originSpeed = 2.0f;

    //속도를 바꿀 그릇
    private float moveSpeed;

    //회전방향 담는 그릇
    private float _rotateX;
    private float _rotateY;

    //마우스 감도 조절
    public float mouseSensitivity = 150f;

    // 카메라 할댱칸
    public Transform cam;

    //리지드바디 대신 캐릭터컨트롤러로 움직일 예정
    private CharacterController _controller;

    public BobbingHead bobbingHead;
    
    private void Awake()
    {

        Instance = this;
        _controller = GetComponent<CharacterController>();
    }
    
    void Start()
    {
        //게임 시작시 마우스 커서를 가운데 고정 후 안보이게 설정
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //이동속도 초기화
        moveSpeed = originSpeed;
    }

    private IEnumerator ChangeMoveMode()
    {   
        if (isChangingMoveMode) yield break; // 이미 실행 중이면 중복 실행 방지

        isChangingMoveMode = true; // 실행 시작 표시
   
        
        if (isStunning)
        {
            moveMode = MoveMode.Slow;

            // for (int i = 0; i < 3; i++)
            // {
            //     // Debug.Log(i);
            //     yield return new WaitForSeconds(0.5f);
            // }
            
            yield return new WaitForSeconds(1.5f);
            
        }
        else
        {
            //시프트 키가 입력될때 트루를 반환하는 코드 작성
            //달릴때 필요한 키 입력받고 출력하는 이프문 (안에 값 변경 필요)
            bool isRunning = (Input.GetKey(KeyCode.LeftShift));
            moveMode = (isRunning ? MoveMode.Running : MoveMode.Move);
        }
        
        isChangingMoveMode = false;
        isStunning = false;
        Debug.Log(moveMode);
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
        _rotateX -= mousey;
        _rotateY += mousex;
        
        //플레이어 회전 y축 기준
        transform.rotation = Quaternion.Euler(0f, _rotateY, 0);
        
        //카메라 회전 x축 기준
        cam.localRotation = Quaternion.Euler(_rotateX, 0f, 0f);
        
        //카메라 회전 제한
        _rotateX = Mathf.Clamp(_rotateX, -40f, 20f);

        // 아무 방향키도 입력받지 않으면 이동하지 않고 종료
        //Mathf.Approximately가 근사치를 뽑아 비교해주는? 메소드이고 =! 를 쓰면 값이 뒤집혀 들어간다
        bool isidle = (Mathf.Approximately(h, 0f) && Mathf.Approximately(v, 0f));
        if (isidle)
        {
            bobbingHead.ChangeMode(MoveMode.Idle);
            return;
        }
        
        if (!isChangingMoveMode) // 코루틴이 실행 중이 아닐 때만 실행
        {
            StartCoroutine(ChangeMoveMode());
        }
        
        moveSpeed = originSpeed * SpeedMode.ChangeSpeed(moveMode);
        bobbingHead.ChangeMode(moveMode);
        bobbingHead.bobbingAmount = bobbingHead.bobbingAmountRun;
        
        //플레이어가 로컬축 기준으로 이동
        var movedir = new Vector3(h, 0, v).normalized;
        _controller.Move(transform.rotation * (moveSpeed * Time.deltaTime * movedir));

    }
}

