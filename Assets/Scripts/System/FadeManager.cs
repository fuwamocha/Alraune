using UnityEngine;
using System;
using DG.Tweening;
using UniRx;

internal static class FadeManager
{
    private static float minAlpha = 0.0f;
    private static float maxAlpha = 1.0f;

/// <summary>
/// 画面遷移用のフェードインクラス
/// </summary>
/// <param name="transitionTime">フェードインにかかる時間 </param>
/// <param name="canvasGroup">フェードインさせるCanvas</param>
/// <returns></returns>
    internal static IObservable<Unit> FadeIn(CanvasGroup canvasGroup, float transitionTime) {
		var responce = new Subject<Unit>();

        canvasGroup.alpha = minAlpha;
		canvasGroup.DOFade(maxAlpha, transitionTime)
                .OnComplete(() => {
					responce.OnNext(Unit.Default);
				});

        return responce.First();
	}

/// <summary>
/// 画面遷移用のフェードアウトクラス
/// </summary>
/// <param name="transitionTime">フェードアウトにかかる時間 </param>
/// <param name="canvasGroup">フェードアウトさせるCanvas</param>
/// <returns></returns>
    internal static IObservable<Unit> FadeOut(CanvasGroup canvasGroup, float transitionTime) {
        var responce = new Subject<Unit>();

		canvasGroup.alpha = maxAlpha;
		canvasGroup.DOFade(minAlpha, transitionTime)
                .OnComplete(() => {
					responce.OnNext(Unit.Default);
				});

        return responce.First();
	}
}