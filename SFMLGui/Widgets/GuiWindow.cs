using SFML.Graphics;
using SFML.System;
using SFML.Window;
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
        private Vector2f lastPos;

        private WindowStyle style;

        private RectangleShape titleBarRect;
        private RectangleShape windowRect;

        private Layer windowLayer;
        private Layer titleBarLayer;

        private RenderWindow window;

        private bool windowRectVisible = true;
        private bool isHovered = false;
        private bool isPressed = false;
        private bool isFirstMove = true;

        private bool isClosed = false;

        public new Vector2f Position { get => position; set { position = value; UpdatePosition(value); } }
        public Vector2f Size { get => windowSize; set { windowSize = value; titleBarSize.X = value.X; } }

        public string Title;

        public bool IsClouse = false;

        public GuiWindow(RenderWindow window, string title, WindowStyle style = WindowStyle.Default)
        {
            this.window = window;
            this.style = style;
            titleBarLayer = new Layer(window, "titleBarLayer");
            windowLayer = new Layer(window, "windowLayer");

            Title = title;

            titleBarRect = new RectangleShape(titleBarSize);
            titleBarRect.FillColor = Color.White;
            titleBarRect.OutlineColor = Color.Black;
            titleBarRect.OutlineThickness = -2;

            windowRect = new RectangleShape(windowSize);
            windowRect.Position = new Vector2f(0, titleBarSize.Y);
            windowRect.FillColor = new Color(200, 200, 200);
            windowRect.OutlineThickness = -2;
            windowRect.OutlineColor = Color.Black;

            window.MouseMoved += Window_MouseMoved;
            window.MouseButtonPressed += Window_MouseButtonPressed;
            window.MouseButtonReleased += Window_MouseButtonReleased;
        }

        public void InitTitleBar()
        {
            switch (style)
            {
                case WindowStyle.Default:
                    break;
                case WindowStyle.Clouse:
                    isClosed = true;
                    break;
            }

            Lable title = new Lable("title", Title);
            title.TextSize = 20;
            title.Origin = new Vector2f(0,12);
            title.Position = new Vector2f(5, titleBarRect.GetGlobalBounds().Height / 2);

            Button hide = new Button("hide");
            hide.OutlineColor = Color.White;
            hide.Texture = new Texture("Hide.png");
            hide.Size = new Vector2f(20, 20);
            hide.Origin = hide.Size / 2;
            hide.Position = new Vector2f(titleBarRect.GetGlobalBounds().Width - 15, titleBarRect.GetGlobalBounds().Height / 2);
            hide.Click += Hide_Click;

            if(isClosed)
            {
                hide.Position = new Vector2f(titleBarRect.GetGlobalBounds().Width - 36, hide.Position.Y);

                Button close = new Button("close");
                close.OutlineColor = Color.White;
                close.Texture = new Texture("Close.png");
                close.Size = new Vector2f(20, 20);
                close.Origin = hide.Size / 2;
                close.Position = new Vector2f(titleBarRect.GetGlobalBounds().Width - 12, titleBarRect.GetGlobalBounds().Height / 2);
                close.Click += Close_Click;

                titleBarLayer.AddWidget(close);
            }

            titleBarLayer.AddWidget(title);
            titleBarLayer.AddWidget(hide);
        }

        public void Update(float deltaTime)
        {
            titleBarLayer.Update(deltaTime);
            windowLayer.Update(deltaTime);

            //if(isPressed)
            {
                if(isFirstMove)
                {
                    lastPos = (Vector2f)Mouse.GetPosition();
                    isFirstMove = false;
                }
                else
                {
                    float deltaX = Mouse.GetPosition().X - lastPos.X;
                    float deltaY = Mouse.GetPosition().Y - lastPos.Y;
                    lastPos = (Vector2f)Mouse.GetPosition();

                    if (isPressed && !titleBarLayer.GetWidgetByStrId("hide").OnHovered())
                        Position = new Vector2f(deltaX, deltaY);
                }
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(titleBarRect, states);
            titleBarLayer.Draw(target, states);


            if (windowRectVisible)
            {
                target.Draw(windowRect, states);
                windowLayer.Draw(target, states);
            }
        }

        public void AddWidget(Widget widget)
        {
            windowLayer.AddWidget(widget);
        }

        public Widget GetWidget(string strId) => windowLayer.GetWidgetByStrId(strId);

        public bool RemoveWidgetByStrId(string strId) => windowLayer.RemoveWidgetByStrId(strId);

        public void SetLayer(Layer layer) => windowLayer = layer;
        public Layer GetLayer() => windowLayer;
        public FloatRect GetWindowRect() => new FloatRect(windowRect.Position, windowSize);
        public FloatRect GetTitleBarRect() => new FloatRect(titleBarRect.Position, titleBarSize);

        public void SetFont(Font font) => titleBarLayer.SetFont(font);

        private void UpdatePosition(Vector2f pos)
        {
            titleBarRect.Position += pos;
            windowRect.Position += pos;

            titleBarLayer.Position += pos;
            titleBarLayer.UpdatePosition(pos);
        }
        private void Close_Click(Button button)
        {
            IsClouse = true;
        }

        private void Hide_Click(Button button)
        {
            windowRectVisible = !windowRectVisible;
        }

        private void Window_MouseButtonReleased(object? sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
                isPressed = false;
        }

        private void Window_MouseButtonPressed(object? sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                if (isHovered)
                    isPressed = true;
            }
        }

        private void Window_MouseMoved(object? sender, MouseMoveEventArgs e)
        {
            if (GetTitleBarRect().Contains(e.X, e.Y))
            {
                isHovered = true;
            }
            else
                isHovered = false;
        }
    }
}
