using System;
using UniRx;

public class GameModel
{
    public readonly ReactiveProperty<int> mineral;
    public readonly ReactiveProperty<int> organism;


    public GameModel()
    {
        mineral = new ReactiveProperty<int>(0);
        organism = new ReactiveProperty<int>(0);
    }

    internal void AddMineral(int v)
    {
        mineral.Value += v;
    }
}
