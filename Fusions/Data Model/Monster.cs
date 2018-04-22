namespace Fusions.DataModel
{
    public class Monster : Card
    {
        private string _name;
        private CardCategory _cardType;
        private int _attack;
        private int _defense;

        public override string Name { get => _name;  set => _name = value; }
        public override CardCategory Type { get => _cardType;  set => _cardType = value; }
        public int Attack { get => _attack; set => _attack = value; }
        public int Defense { get => _defense; set => _defense = value; }

        public Monster(string name, int attack, int defense, CardCategory type)
        {
            Name = name;
            Type = type;
            Attack = attack;
            Defense = defense;
        }

        public Monster(string name, CardCategory cardType)
        {
            Name = name;
            Type = cardType;
        }
    }
}
