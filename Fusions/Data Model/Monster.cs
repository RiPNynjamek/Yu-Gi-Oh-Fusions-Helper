namespace Fusions.DataModel
{
    public class Monster : Card
    {
        private string _name;
        private CardType _cardType;

        public override string Name { get => _name;  set => _name = value; }
        public override CardType Type { get => _cardType;  set => _cardType = value; }

        public Monster(string name, CardType type)
        {
            Name = name;
            Type = type;
        }
    }
}
