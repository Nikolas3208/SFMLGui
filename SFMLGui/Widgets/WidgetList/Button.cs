using SFML.Graphics;
using SFML.System;
using SFML.Window;
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
        public delegate void ClickHandler(Button button);
        public event ClickHandler Click;

        public Button(string strId) : base(strId)
        {
        }

        public Button(string strId, string text) : base(strId)
        {
            Text = text;
        }

        protected override void Window_MouseMoved(object? sender, MouseMoveEventArgs e)
        {
            if (new FloatRect(e.X, e.Y, 10, 10).Intersects(GetFloatRect()))
            {
                IsHovered = true;
            }
            else
                IsHovered = false;
        }

        protected override void UpdateView()
        {
            if (IsHovered)
                Color = HoveredColor;

            if (!IsHovered)
                Color = DefaultColorRect;
        }
    }
}
