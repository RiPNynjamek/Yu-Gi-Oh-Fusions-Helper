namespace Fusions.DataModel
{
    public class Trap : Card
    {
        private string _name;
        private CardType _cardType = CardType.Trap;

        public Trap(string name)
        {
            Name = name;
        }

        public override string Name { get => _name; set => _name = value; }
        public override CardType Type { get => _cardType; set => _cardType = CardType.Trap; }
    }
}
