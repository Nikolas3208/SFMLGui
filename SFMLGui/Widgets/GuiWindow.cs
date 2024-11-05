using SFML.Graphics;
using SFML.System;

using SFMLGui.Widgets.WidgetList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLGui.Widgets
{
    public enum WindowStyle
    {
        Default,
        Clouse
    }
    public class GuiWindow : Transformable, Drawable
    {
        private Vector2f titleBarSize = new Vector2f(500, 24);
        private Vector2f windowSize = new Vector2f(500, 300);
        private Vector2f position;

        private RectangleShape titleBarRect;
        private RectangleShape windowRect;

        private LayerStack layerStack = new LayerStack();
        private Layer titleBarLayer;

        private bool windowRectVisible = true;

        public new Vector2f Position { get => position; set { position = value; UpdatePosition(value); } }

        public string Title;

        public Vector2f Size { get => windowSize; set { windowSize = value; titleBarSize.X = value.X; } }

        public GuiWindow(RenderWindow window, string title)
        {
            titleBarLayer = new Layer(window, "titleBarLayer");

            Title = title;

            titleBarRect = new RectangleShape(titleBarSize);
            titleBarRect.FillColor = new Color(170, 170, 170);
            titleBarRect.OutlineColor = Color.Black;
            titleBarRect.OutlineThickness = -2;

            windowRect = new RectangleShape(windowSize);
            windowRect.Position = new Vector2f(0, titleBarSize.Y);
            windowRect.FillColor = new Color(200, 200, 200);
            windowRect.OutlineThickness = -2;
            windowRect.OutlineColor = Color.Black;
        }

        public void InitTitleBar()
        {
            Lable title = new Lable("title", Title);
            title.TextSize = 20;
            title.Origin = new Vector2f(0,12);
            title.Position = new Vector2f(5, titleBarRect.GetGlobalBounds().Height / 2) + Position;

            Button hide = new Button("hide");
            hide.Size = new Vector2f(15, 15);
            hide.Origin = hide.Size / 2;
            hide.Position = new Vector2f(titleBarRect.GetGlobalBounds().Width - 10, titleBarRect.GetGlobalBounds().Height / 2) + Position;
            hide.Click += Hide_Click;

            titleBarLayer.AddWidget(title);
            titleBarLayer.AddWidget(hide);
        }

        private void Hide_Click(Button button)
        {
            windowRectVisible = !windowRectVisible;
        }

        public void Update(float deltaTime)
        {
            titleBarLayer.Update(deltaTime);
        }

        private void UpdatePosition(Vector2f pos)
        {
            titleBarRect.Position += pos;
            windowRect.Position += pos;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            //states.Transform *= Transform;

            target.Draw(titleBarRect, states);
            titleBarLayer.Draw(target, states);


            if (windowRectVisible)
            {
                target.Draw(windowRect, states);
                layerStack.Draw(target, states);
            }
        }

        public void SetFont(Font font) => titleBarLayer.SetFont(font);
    }
}
