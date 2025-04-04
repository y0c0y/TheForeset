public static class SpeedMode
{
    //달리기속도배율
    private const float RunningSpeed = 1.5f;
    //느려진속도배율
    private const float SlowSpeed = 0.5f;
    //기본속도배율
    private const float NormalSpeed = 1.0f;
    //말할때속도배율
    private const float TalkSpeed = 0.9f;
    
    //속도를 바꿔주는 메소드
    public static float ChangeSpeed(MoveMode mode)
    {
        if (mode == MoveMode.Running)
        {
            return RunningSpeed;
        }
        else if (mode == MoveMode.Slow)
        {
            return SlowSpeed;
        }
        else if (mode == MoveMode.Talk)
        {
            return TalkSpeed;
        }
        else 
        {
            return NormalSpeed;
        }
    }
}