namespace CsConsoleGame
{
    internal class Worker
    {
        //const
        public Worker(ushort price, string name) {
            Price = price;
            Name = name;
        }

        //meth
        public string Name { get; set; }
        public ushort Price { get; }
    }
}