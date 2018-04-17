using System;
using System.Collections.Generic;

namespace Fusions.DataModel
{
    public class Association
    {
        public Monster BaseMonster;
        //public Card AssociatedCard;
        //public Monster FusionResult;
        public List<Tuple<Card, Monster>> Combination;

        public Association(Monster baseMonster)/*, Card associatedCard, Monster fusionResult)*/
        {
            BaseMonster = baseMonster;
            //AssociatedCard = associatedCard;
            //FusionResult = fusionResult;
        }
    }
}
