using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusions.DataModel
{
    public class Card
    {
        private string _name;
        private CardType _type;

        public string Name { get => _name; set => _name = value; }
        public CardType Type { get => _type; set => _type = value; }
    }
}
