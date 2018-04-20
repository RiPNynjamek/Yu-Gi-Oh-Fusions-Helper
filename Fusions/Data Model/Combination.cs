using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusions.DataModel
{
    public class Combination
    {
        public Combination(Monster firstCard)
        {
            FirstCard = firstCard;
        }

        public Combination(Card firstCard, Card secondCard, Card fusionResult)
        {
            FirstCard = firstCard;
            SecondCard = secondCard;
            FusionResult = fusionResult;
        }

        public Card FirstCard { get; set; }
        public Card SecondCard { get; set; }
        public Card FusionResult { get; set; }
    }
}
