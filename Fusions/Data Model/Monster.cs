namespace Fusions.DataModel
{
    public class Monster : Card
    {
        private string _name;
        private CardCategory _cardCategory;
        private int _attack;
        private int _defense;
        private Type _type;
        private MonsterType _monsterType;

        public override string Name { get => _name;  set => _name = value; }
        public override CardCategory Category { get => _cardCategory;  set => _cardCategory = value; }
        public int Attack { get => _attack; set => _attack = value; }
        public int Defense { get => _defense; set => _defense = value; }
        public Type Type { get => _type; set => _type = value; }
        public MonsterType MonsterType { get => _monsterType; set => _monsterType = value; }

        public Monster(string name, int attack, int defense, CardCategory type)
        {
            Name = name;
            Category = type;
            Attack = attack;
            Defense = defense;
        }

        public Monster(string name, CardCategory cardType)
        {
            Name = name;
            Category = cardType;
        }
    }

    public enum Type
    {

    }
}
