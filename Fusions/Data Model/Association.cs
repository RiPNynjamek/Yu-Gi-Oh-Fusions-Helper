using System;

namespace Fusions.DataModel
{
    public struct Association
    {
        public Monster BaseMonster;
        public Monster AssociatedMonster;
        public Monster FusionResult;
        public Tuple<Monster, Card> Combination;
    }
}
