namespace Fusions.DataModel
{
    public class Equipment : Card
    {
        private string _name;
        private CardCategory _cardType;

        public override string Name { get => _name; set => _name = value; }
        public override CardCategory Type { get => _cardType; set => _cardType = CardCategory.Equipment; }

        public Equipment(string name)
        {
            Name = name;
        }
    }
}
