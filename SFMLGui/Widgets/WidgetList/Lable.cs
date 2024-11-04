using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLGui.Widgets.WidgetList
{
    public class Lable : Widget
    {
        public Lable(string strId, string mess) : base(strId)
        {
            text = new Text();
            text.DisplayedString = mess;
        }
    }
}
