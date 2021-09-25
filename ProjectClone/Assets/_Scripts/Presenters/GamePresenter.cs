using UniRx;
using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    [SerializeField]
    private GameMainView gameMainView;

    public GameModel gameModel;
    public void Init()
    {
        gameModel = new GameModel();

        gameModel.mineral.Subscribe(value => gameMainView.MineralText = value.ToString());
        gameModel.organism.Subscribe(value => gameMainView.OrganismText = value.ToString());
    }
}
