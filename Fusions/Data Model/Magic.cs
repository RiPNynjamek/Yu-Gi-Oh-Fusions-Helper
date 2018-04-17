namespace Fusions.DataModel
{
    class Magic : Card
    {
        private string _name;
        private CardType _cardType;

        public Magic(string name)
        {
            Name = name;
        }

        public override string Name { get => _name; set => _name = value; }
        public override CardType Type { get => _cardType; set => _cardType = CardType.Magic; }
    }
}
