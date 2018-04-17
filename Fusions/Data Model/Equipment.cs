namespace Fusions.DataModel
{
    public class Equipment : Card
    {
        private string _name;
        private CardType _cardType;

        public override string Name { get => _name; set => _name = value; }
        public override CardType Type { get => _cardType; set => _cardType = CardType.Equipment; }

        public Equipment(string name)
        {
            Name = name;
        }
    }
}
