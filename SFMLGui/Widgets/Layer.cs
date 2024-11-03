using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLGui.Widgets
{
    public class Layer
    {
        private List<Widget> widgets;

        public string Name;

        public Layer(string name)
        {
            Name = name;

            widgets = new List<Widget>();
        }
    }
}
