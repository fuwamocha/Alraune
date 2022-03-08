
public class Config
{
    internal static int BPM = 170;

    // 一拍分の秒数
    internal static float SecondsPerBeat = 60 / 170f;
    internal static float StepSecondsPerBeat = SecondsPerBeat * 2;

    internal enum GameStatus
    {
        GameClear,
        GameOver,
    }
    //TODO:カウントダウンの秒も上のでやったらいい感じにできそう
}
