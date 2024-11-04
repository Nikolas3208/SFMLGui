using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLGui.Widgets.WidgetList
{
    public class Button : Widget
    {
        public Button(string strId, string mess) : base(strId)
        {
            rect = new RectangleShape();
            rect.FillColor = BaseColorRect;
            Size = new Vector2f(100, 50);

            text = new Text();
            text.FillColor = BaseColorText;
            text.DisplayedString = mess;
        }

        public Button(string strId, string mess, Vector2f size) : base(strId)
        {
            rect = new RectangleShape(size);
            rect.FillColor = BaseColorRect;
            Size = size;

            text = new Text();
            text.FillColor = BaseColorText;
            text.DisplayedString = mess;
        }

        public Button(string strId, string mess, Vector2f size, Font font) : base(strId)
        {
            rect = new RectangleShape(size);
            rect.FillColor = BaseColorRect;
            Size = size;

            text = new Text(mess, font);
            text.FillColor = BaseColorText;
        }
    }
}
