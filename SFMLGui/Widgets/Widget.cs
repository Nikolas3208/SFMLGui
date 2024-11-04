using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLGui.Widgets
{
    public abstract class Widget : Transformable, Drawable
    {
        protected RectangleShape? rect;
        protected Text? text;
        protected RenderWindow? window;
        protected Font? Font { get => font; set { font = value; if (text != null) text.Font = value; } }

        protected bool IsRectVisible = true;
        protected bool IsTextVisible = true;
        protected bool IsClicked = false;
        protected bool IsSelected = false;
        protected bool IsHovered = false;

        private Vector2f size;
        private Font? font;

        public string strId { get; protected set; }
        public bool LeaveРighlightСolor = false;

        public Color BaseColorRect = Color.White;
        public Color ClickedColorRect = new Color(64, 64, 64);
        public Color SelectedColorRect = Color.Yellow;
        public Color HoveredColorRect = new Color(128, 128, 128);
        public Color BaseColorText = Color.Black;

        public Vector2f Size { get => size; set { size = value; if(rect != null) rect!.Size = value; } }

        public Widget(string strId)
        {
            this.strId = strId;
        }

        public void SetWindow(RenderWindow window)
        {
            this.window = window;

            this.window.MouseMoved += Window_MouseMoved;
            this.window.MouseButtonPressed += Window_MouseButtonPressed;
            this.window.MouseButtonReleased += Window_MouseButtonReleased;
        }

        private void Window_MouseButtonReleased(object? sender, MouseButtonEventArgs e)
        {
            if(e.Button == Mouse.Button.Left)
            {
                if(IsClicked)
                {
                    IsSelected = true;
                    IsClicked = false;
                }
            }
        }

        protected virtual void Window_MouseButtonPressed(object? sender, MouseButtonEventArgs e)
        {
            if(e.Button == Mouse.Button.Left)
            {
                if(IsHovered)
                {
                    IsClicked = true;
                    IsHovered = false;
                }
                else
                {
                    IsClicked = false;
                    IsSelected = false;
                    IsHovered = false;
                }
            }
        }

        private void Window_MouseMoved(object? sender, MouseMoveEventArgs e)
        {
            if (new FloatRect(e.X, e.Y, 10, 10).Intersects(GetFloatRect()))
            {
                IsHovered = true;
            }
            else
                IsHovered = false;
        }

        public RenderWindow GetWindow() => window!;

        public void SetFont(Font font) => Font = font;
        public Font GetFont() => Font!;

        public bool OnClicked() => IsClicked;
        public bool OnSelected() => IsSelected;
        public bool OnHovered() => IsHovered;

        public FloatRect GetFloatRect() => new FloatRect(Position, Size);

        public virtual void Update(float deltaTime)
        {
            if (rect != null)
            {
                if (IsHovered)
                    rect.FillColor = HoveredColorRect;
                else
                    rect.FillColor = BaseColorRect;
                if (IsClicked)
                    rect.FillColor = ClickedColorRect;
                if(IsSelected && LeaveРighlightСolor)
                    rect.FillColor = SelectedColorRect;
                else if (!IsClicked && !IsHovered && !IsSelected && !LeaveРighlightСolor)
                    rect.FillColor = BaseColorRect;
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            if(rect != null && IsRectVisible)
                target.Draw(rect, states);

            if(text != null && IsTextVisible)
                target.Draw(text, states);
        }
    }
}
