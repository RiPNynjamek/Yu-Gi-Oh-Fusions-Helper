namespace Fusions.DataModel
{
    public class Trap : Card
    {
        private string _name;
        private CardCategory _cardType = CardCategory.Trap;

        public Trap(string name)
        {
            Name = name;
        }

        public override string Name { get => _name; set => _name = value; }
        public override CardCategory Type { get => _cardType; set => _cardType = CardCategory.Trap; }
    }
}
