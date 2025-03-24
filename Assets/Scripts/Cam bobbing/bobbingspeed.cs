public static class BobbingSpeed
{
    //달리기속도배율
    private const float BobbingSpeedRun = 1000f;
    //느려진속도배율
    private const float BobbingSpeedSlow = 200f;
    //기본속도배율
    private const float BobbingSpeedNomal = 700f;
    
    //움직이지 않을때 배율
    private const float BobbingSpeedIdle = 0f;
    
    //속도를 바꿔주는 메소드
    public static float ChangeBobbingSpeed(MoveMode mode)
    {
        if (mode == MoveMode.Running)
        {
            return BobbingSpeedRun;
        }
        else if (mode == MoveMode.Slow)
        {
            return BobbingSpeedSlow;
        }
        else if (mode == MoveMode.Idle)
        {
            return BobbingSpeedIdle;
        }
        else
        {
            return BobbingSpeedNomal;
        }
    }
}
