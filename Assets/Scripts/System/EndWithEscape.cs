using UnityEngine;

public class EndWithEscape : MonoBehaviour
{
    private static EndWithEscape instance;
 
    // シングルトンにする
    public static EndWithEscape Instance
    {
        get {
            if (instance == null) {
                instance = (EndWithEscape)FindObjectOfType(typeof(EndWithEscape));
 
                if (instance == null) {
                    Debug.LogError(typeof(EndWithEscape) + "をアタッチしているGameObjectはありません");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        OnClickEscapeKey();
    }

    /// <summary>
    /// Escapeキーを押すといつでもゲーム終了する
    /// </summary>
    private void OnClickEscapeKey()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            Debug.Log("アプリが終了しました");
            Application.Quit();
        }
    }
}
