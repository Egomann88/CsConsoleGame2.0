namespace CsConsoleGame
{
    public interface IWorkerServices
    {
        string Name { get; set; }
        byte Lvl { get; set; }
        ushort UpgradeCost { get; set; }
    }
}
