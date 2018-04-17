using System;
using System.Collections.Generic;

namespace Fusions.DataModel
{
    public class Association
    {
        public Monster BaseMonster;
        //public Card AssociatedCard;
        //public Monster FusionResult;
        public List<Tuple<Card, Card>> Combination;

        public Association(Monster baseMonster)/*, Card associatedCard, Monster fusionResult)*/
        {
            BaseMonster = baseMonster;
            Combination = new List<Tuple<Card, Card>>();
        }
    }
}
