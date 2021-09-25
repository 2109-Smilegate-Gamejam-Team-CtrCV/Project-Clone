using DG.Tweening;
using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class TitleOptionView : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Button cancleButton;

    [SerializeField]
    private Slider masterVolume;

    [SerializeField]
    private Slider bgmVolume;

    [SerializeField]
    private Slider vfxVolume;


    public IObservable<Unit> CancelClick
    {
        get => cancleButton.OnClickAsObservable();
    }

    public IObservable<float> MasterVolumeValueChanged
    {
        get => masterVolume.OnValueChangedAsObservable();
    }
    public IObservable<float> BGMVolumeValueChanged
    {
        get => bgmVolume.OnValueChangedAsObservable();
    }
    public IObservable<float> VFXVolumeValueChanged
    {
        get => vfxVolume.OnValueChangedAsObservable();
    }


    public void Show()
    {
        var showSequence = DOTween.Sequence();
        showSequence.Append(canvasGroup.DOFade(1, 0.5f).From(0));
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        var showSequence = DOTween.Sequence();
        showSequence.Append(canvasGroup.DOFade(0, 0.5f).From(1));
        showSequence.OnComplete(() => gameObject.SetActive(false)); 
    }

}