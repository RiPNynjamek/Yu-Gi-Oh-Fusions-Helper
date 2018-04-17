using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusions.DataModel
{
    public abstract class Card
    {
        public abstract string Name { get; set; }
        public abstract CardType Type { get; set; }
    }
}
