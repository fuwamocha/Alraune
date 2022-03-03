using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup = default;
    [SerializeField] private Button[] sceneTransitionButtons = default;
    [SerializeField] private string[] sceneName;

    [SerializeField] private float fadeInTime = 0.3f;
    [SerializeField] private float fadeOutTime = 0.3f;

    private async void Start()
    {
        await FadeManager.FadeIn(canvasGroup, fadeInTime);

        canvasGroup = canvasGroup.GetComponent<CanvasGroup>();

        for (int i = 0; i < sceneTransitionButtons.Length; i++)
        {
            var num = i;

            sceneTransitionButtons[i] = sceneTransitionButtons[i].GetComponent<Button>();
            sceneTransitionButtons[i].OnClickAsObservable()
                       .First()
                       .Subscribe(_ =>
                           OnClickSceneTransitionButton(num).Forget()
                       )
                       .AddTo(this);
        }
    }

    /// <summary>
    /// ボタンを押した時に画面遷移させるクラス
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid OnClickSceneTransitionButton(int i)
    {

        await FadeManager.FadeOut(canvasGroup, fadeOutTime);
        SceneManager.LoadScene(sceneName[i]);
    }
}
