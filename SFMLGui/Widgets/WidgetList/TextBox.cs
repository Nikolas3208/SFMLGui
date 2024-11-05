using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLGui.Widgets.WidgetList
{
    public class TextBox : Widget
    {
        private StringBuilder stringBuilder = new StringBuilder();
        public TextBox(string strId) : base(strId)
        {
            Size = new Vector2f(150, 40);
        }

        public override void SubscribeEvent(RenderWindow window)
        {
            base.SubscribeEvent(window);

            window.TextEntered += Window_TextEntered;
        }

        private void Window_TextEntered(object? sender, SFML.Window.TextEventArgs e)
        {
            if(IsSelected)
            {
                string keyKode = e.Unicode;
                if (keyKode != "\b" && keyKode != "\r")
                    stringBuilder.Append(keyKode);
                else if (keyKode == "\b" && stringBuilder.Length > 0 && keyKode != "\r")
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                else if (keyKode == "\r")
                {
                    IsSelected = false;
                }

                Text = stringBuilder.ToString();
            }
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

        protected override void Window_MouseButtonReleased(object? sender, MouseButtonEventArgs e)
        {
            if(IsHovered)
            {
                IsSelected = true;
            }
            else
                IsSelected = false;
        }

        protected override void UpdateText()
        {
            if (Text.Length > 0 && Text[Text.Length - 1] != '|')
            {
                text.Origin = new Vector2f(
                    -1,
                    text.GetGlobalBounds().Height / 2f);

                text.Position = new Vector2f(
                    5,
                    rect.GetGlobalBounds().Height / 2.5f);
            }
        }

        protected override void UpdateView()
        {
            
        }

        float second = 0;
        bool Isadd = true;
        public override void Update(float deltaTime)
        {
            if (IsSelected)
            {
                second += deltaTime * 1.8f;

                if (second >= 1 && Isadd)
                {
                    Text += "|";
                    second = 0;
                    Isadd = !Isadd;
                }
                else if (text.DisplayedString.Length > 0 && second >= 1 && !Isadd)
                {
                    StringBuilder stringBuilder = new StringBuilder(Text);
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                    Text = stringBuilder.ToString();

                    second = 0;
                    Isadd = !Isadd;
                }
            }
            else
            {
                if (Text.Length > 0 && Text[Text.Length - 1] == '|')
                {
                    StringBuilder stringBuilder = new StringBuilder(Text);
                    stringBuilder.Remove(stringBuilder.Length - 1, 1);
                    Text = stringBuilder.ToString();
                }

                second = 0;
                Isadd = true;
            }

            //Text = second.ToString();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
        }
    }
}
