using System;
using UnityEngine;

public class BobbingHead : MonoBehaviour
{
    
    //카메라 흔들림 크기 //달릴때
    public float bobbingAmount = 0f;
    public float bobbingAmountNormal = 0.4f;
    public float bobbingAmountRun = 3f;
    
    //카메라 흔들림 속도)) 사인계산을 위해 1초에 설정각도씩 올라가게 하는 각도 담는 그릇
    public float presentBobbingSpeed = 0f;
    public float goalBobbingSpeed = 0f;
    
    //카메라 원 위치 저장 그릇
    private float defaultYCam = 0;
    
    //사인계산을 위한 현 몇도인지 담기위한 그릇
    public float bobbingSpeedSum = 0;
    
    //현재 bobbingspeed에서 다른 속도로 바뀔때의 속도 변수
    public float bobbingSpeedSumRun = 0;
    
    
    
    //라디안각으로 변환한값을 담을 그릇
    public float radian = 0;
    //라디안각을 사인파로 변환한 값을 담는 그릇
    public float sin = 0;

    
    //스크립트가 실행되었을때 카메라의 처음 위치 저장
    private void OnEnable()
    {
         defaultYCam = transform.localRotation.eulerAngles.x;
    }
    
    //스크립트가 꺼졌을때 카메라를 원위치로 복구시킴
    private void OnDisable()
    {
        //부드럽게 돌아가도록 식 추가 필요
        
        transform.localRotation = Quaternion.Euler(defaultYCam, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
    }
    
    //무브모드가 바뀌었을때 스피드값을 바꾸는 매소드
    public void ChangeMode(MoveMode mode)
    {
        goalBobbingSpeed = BobbingSpeed.ChangeBobbingSpeed(mode);
    }
    
    
    private void Update()
    {
        if (presentBobbingSpeed - goalBobbingSpeed < 0f)
        {
            presentBobbingSpeed = goalBobbingSpeed;
        }
        else if (presentBobbingSpeed - goalBobbingSpeed > 0f)
        {
            presentBobbingSpeed = goalBobbingSpeed;
        }
        
        
        ///// presentBobbingSpeed -> goalBobbingSpeed
        // 10 -> 20
        //
        bobbingAmount = bobbingAmountNormal;
        bobbingHead();
    }
    
    //카메라 흔들리는 기능
    private void bobbingHead()
    {
        //사인계산식
        bobbingSpeedSum += presentBobbingSpeed * Time.deltaTime;
        //360도 넘어가지 않게 제한을 두면서 360도가 되면 다시 0도로 돌아가도록 설정
        if (bobbingSpeedSum > 360f)
        {
            bobbingSpeedSum -= 360f;
        }

        //각도를 라디안각으로 변환하는 식
        radian = Mathf.Deg2Rad * bobbingSpeedSum;
        //라디안각을 사인파로 변환하는 식
        sin = Mathf.Sin(radian);

        //흔들리게 해주는 식
        float movingYCam = defaultYCam + sin * bobbingAmount;
        transform.localRotation = Quaternion.Euler(movingYCam, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
    }
}
