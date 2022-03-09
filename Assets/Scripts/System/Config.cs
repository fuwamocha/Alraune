
public class Config
{
    internal static int BPM = 170;

    // 一拍分の秒数
    internal static float SecondsPerBeat = 60f / BPM;
    internal static float StepSecondsPerBeat = SecondsPerBeat * 2;

    internal enum GameStatus
    {
        GameClear,
        GameOver,
    }
}
