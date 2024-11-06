using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFMLGui.Widgets.WidgetList
{
    public class Slider : Widget
    {
        private int Min;
        private int Max;
        private float Step;
        public float Value;

        private bool pressed = false;

        private RectangleShape bar;

        public Slider(string strId, float value, string text = "", int min = 0, int max = 100, float step = 1) : base(strId)
        {
            Min = min;
            Max = max;
            Step = step;
            Value = value;

            Size = new Vector2f(300, 5);
            Texture = new Texture("Slider.png");
            OutlineColor = Color.White;

            bar = new RectangleShape(new Vector2f(20, 20));
            bar.Texture = new Texture("Circle.png");
            bar.Origin = new Vector2f(rect.Size.X / 2, 10);
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);

            float length = rect.GetGlobalBounds().Width - bar.GetGlobalBounds().Width;
            float range = Max - Min;

            if(pressed)
            {
                bar.Position = new Vector2f((mousePos.X + bar.Origin.X) - bar.GetGlobalBounds().Width / 2f, 0);

                if(bar.Position.X - Position.X < 0)
                {
                    bar.Position = new Vector2f(0, bar.Position.X) + Position;
                }
                else if((bar.Position.X - Position.X) + bar.Size.X > Size.X - bar.Size.X)
                {
                    bar.Position = new Vector2f(Size.X - bar.Size.X, bar.Position.X) + Position;
                }

                float offset = length / range;
                Value = ((int)(((bar.Position.X - Position.X) + offset / 2f) * range / length) + Min) * Step;
            }

            bar.Position = Position + new Vector2f((Value / Step - Min) * length / range, 0);

        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);

            target.Draw(bar);

            DebugRender.AddRectangle(GetBarRect(), Color.Green);
        }

        private Vector2f mousePos;
        protected override void Window_MouseMoved(object? sender, MouseMoveEventArgs e)
        {
            mousePos = new Vector2f(e.X, e.Y);

            if (GetBarRect().Contains(e.X, e.Y))
            {
                IsHovered = true;
            }
            else
                IsHovered = false;
        }

        protected override void Window_MouseButtonPressed(object? sender, MouseButtonEventArgs e)
        {
            if (IsHovered)
                pressed = true;
            else
                pressed = false;
        }

        protected override void Window_MouseButtonReleased(object? sender, MouseButtonEventArgs e)
        {
            pressed = false;
        }

        public FloatRect GetBarRect() => new FloatRect(bar.Position - bar.Origin, bar.Size);

        protected override void UpdateView()
        {
            
        }
    }
}
