using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePresenter : MonoBehaviour
{
    [SerializeField]
    private TitleMainView mainView;
    [SerializeField]
    private TitleOptionView optionView;


    private void Awake()
    {

        mainView.StartClick.Subscribe(_ => SceneManager.LoadScene(1));
        mainView.OptionClick.Subscribe(_ => optionView.Show());
        mainView.ExitClick.Subscribe(_ => Application.Quit());


        optionView.CancleClick.Subscribe(p => optionView.Hide());
        optionView.MasterVolumeValueChanged.Subscribe(p => OptionManager.Instance.Master = p);
        optionView.BGMVolumeValueChanged.Subscribe(p => OptionManager.Instance.BGM = p);
        optionView.VFXVolumeValueChanged.Subscribe(p => OptionManager.Instance.VFX = p);

    }
}
