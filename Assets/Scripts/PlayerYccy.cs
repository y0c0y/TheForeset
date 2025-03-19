using System;
using UnityEngine;

public class PlayerYccy : MonoBehaviour
{
    private int hashIsMoved = Animator.StringToHash("IsMoved");
    public float moveSpeed = 0.01f;

    public Animator animator;
    private Rigidbody rigidbody;

    public UnityEngine.Camera cam;

    private Vector3 _lastPoint;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        // cam = GetComponent<UnityEngine.Camera>();
        // animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        ///// 마우스 커서를 바라보게
        // 마우스 좌표 -> 카메라 기준 레이로 변경
        // var mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f,
                LayerMask.GetMask("Ground")))
        {
            // Debug.Log($"히트됨 {hit.transform.gameObject.name} - {hit.point}");
            _lastPoint = hit.point;
        }
        // else
        // {
        //     Debug.Log("히트안됨");
        // }

        // 플레이어가 히트 포인트를 바라보는 방향 벡터 연산
        var targetPoint = _lastPoint;
        targetPoint.y = transform.position.y;
        var dir = Vector3.Normalize(targetPoint - transform.position);
        // 플레이어가 커서를 바라보게 회전
        transform.forward = dir;
        
        // 키보드 방향키 입력정보를 가져옴
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        // 아무 방향키도 입력하지 않았으면 이동하지 않고 종료
        bool isMoved = !(Mathf.Approximately(h, 0f) && Mathf.Approximately(v, 0f));
        // animator.SetBool(hashIsMoved, isMoved);
        if (!isMoved)
            return;
        
        // 플레이어가 회전된 방향을 무시하고 월드축을 기준으로 이동
        var moveDir = new Vector3(h, 0, v).normalized;
        rigidbody.position += moveSpeed * Time.deltaTime * moveDir;
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.color = new Color(1f, 0.2f, 0.2f, 1f);
    //     {
    //         Gizmos.DrawSphere(_lastPoint, 0.1f);
    //     }
    //     Gizmos.color = Color.white;
    // }
}