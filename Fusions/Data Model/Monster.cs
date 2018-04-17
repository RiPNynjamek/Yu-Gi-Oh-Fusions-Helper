using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fusions.DataModel
{
    public class Monster : Card
    {
        public Monster(string name, CardType @base)
        {
            Name = name;
            Type = @base;
        }
    }
}
