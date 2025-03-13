using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 0.1f;
    public Rigidbody rb;
    private bool isGrounded;
    private bool isJumping;
    private bool isWalking;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() //물리 연산 프레임마다 호출되는 생명주기 함수 
    {
        // 키보드 입력을 받아 이동 벡터 계산
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // 정규화하는 이유: 모든 방향의 벡터 길이가 1이어야 방향에 따른 속도가 같아지기 때문 
        Vector3 move = new Vector3(horizontal, 0, vertical).normalized; 

        // 이동 및 회전 수행
        if (move.magnitude > 0) // 벡터의 크기가 0 초과 일때 : 0이라면 이동할 이유 X
        {
            rb.MovePosition(transform.position + move * (moveSpeed * Time.fixedDeltaTime));
            rb.MoveRotation(Quaternion.LookRotation(move));// 지정된 방향을 가리키는 쿼터니언을 생성
            //Quaternion : 3차원 공간에서의 회전 정보 <= 벡터의 세 축으로만 할 때 발생하는 짐벌락 문제를 해결하기 위해 사용
        }

        // 점프 입력 처리
        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * (jumpForce * Time.fixedDeltaTime), ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 땅에 닿았는지 여부를 판별
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // 땅을 벗어났는지 여부를 판별
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            SceneManager.LoadScene("SampleScene");//씬 재시작
        }
    }
}