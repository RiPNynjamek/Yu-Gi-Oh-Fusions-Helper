namespace Fusions.DataModel
{
    class Magic : Card
    {
        private string _name;
        private CardCategory _cardType;

        public Magic(string name)
        {
            Name = name;
        }

        public override string Name { get => _name; set => _name = value; }
        public override CardCategory Category { get => _cardType; set => _cardType = CardCategory.Magic; }
    }
}
