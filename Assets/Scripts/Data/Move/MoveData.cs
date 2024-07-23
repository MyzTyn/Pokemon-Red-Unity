[System.Serializable]
public class MoveData
{
    public MoveData(string name, int power, Types type, int accuracy, int maxpp, MoveEffect effect)
    {
        this.name = name;
        this.power = power;
        this.type = type;
        this.accuracy = accuracy;
        this.maxpp = maxpp;
        this.effect = effect;
    }

    public string name;
    public int power;
    public Types type;
    public int accuracy;
    public int maxpp;
    public MoveEffect effect;
}