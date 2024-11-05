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

        private bool IsPressed = false;
        private bool IsReleased = false;
        private bool triger = false;

        public Button(string strId) : base(strId)
        {
        }

        public Button(string strId, string text) : base(strId)
        {
            Text = text;
        }

        protected override void Window_MouseMoved(object? sender, MouseMoveEventArgs e)
        {
            base.Window_MouseMoved(sender, e);
        }

        protected override void Window_MouseButtonPressed(object? sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                if(IsHovered)
                    IsPressed = true;
                else
                    IsPressed = false;
            }
        }

        protected override void Window_MouseButtonReleased(object? sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                if (IsHovered && IsPressed)
                { 
                    IsPressed = false;
                    IsReleased = true;
                }
                else
                    IsReleased = false;
            }
        }

        protected override void UpdateView()
        {
            if (IsHovered)
            {
                Color = HoveredColor;
                if(IsClicked)
                {
                    Color = ClickColor;
                }
            }

            if (!IsHovered)
                Color = DefaultColorRect;
        }

        public override void Update(float deltaTime)
        {
            if(IsPressed && !triger)
            {
                IsClicked = true;
                triger = true;
            }
            else if(IsPressed && triger)
            {
                IsClicked = false;
            }

            if(IsReleased)
            {
                IsClicked = false;
                triger = false;
                IsReleased = false;
            }
        }
    }
}
