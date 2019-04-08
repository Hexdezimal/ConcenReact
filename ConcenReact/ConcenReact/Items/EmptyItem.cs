using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcenReact
{
    class EmptyItem : Item
    {
        public EmptyItem():base()
        {

        }
        public override string GetDataAsString()
        {
            return "";
        }
    }
}
