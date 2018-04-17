using System.Collections.Generic;

namespace Fusions.DataModel
{
    public class Association
    {
        public Monster BaseMonster { get; set; }
        public Dictionary<string, string> Combination { get; set; }

        public Association(Monster baseMonster)
        {
            BaseMonster = baseMonster;
            Combination = new Dictionary<string, string>();
        }
    }
}
