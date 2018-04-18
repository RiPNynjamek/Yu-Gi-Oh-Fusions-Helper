using System.Collections.Generic;

namespace Fusions.DataModel
{
    public class Association
    {
        public Card BaseMonster { get; set; }
        public Dictionary<Card, Card> Combination { get; set; }

        public Association(Card baseMonster)
        {
            BaseMonster = baseMonster;
            Combination = new Dictionary<Card, Card>();
        }
    }
}
