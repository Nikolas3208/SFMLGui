﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLGui.Widgets
{
    public enum WidgetStyle
    {
        Default,
        AutoResize
    }
    public abstract class Widget : Transformable, Drawable
    {
        protected Vector2f baseRectSize = new Vector2f(80, 40);

        private bool isClicked = false;
        private bool isSelected = false;
        private bool isHovered = false;
        private bool autoResize = false;

        private WidgetStyle style;

        protected RectangleShape rect;
        protected Text text;
       
        public bool IsRectVisible = true;
        public bool IsTextVisible = true;

        protected bool IsClicked { get => isClicked; set { isClicked = value; UpdateView(); } }
        protected bool IsSelected { get => isSelected; set { isSelected = value; UpdateView(); } }
        protected bool IsHovered { get => isHovered; set { isHovered = value; UpdateView(); } }

        public Color Color { get => rect.FillColor; set => rect.FillColor = value; }
        public Color OutlineColor { get => rect.OutlineColor; set => rect.OutlineColor = value; }

        public Color DefaultColorRect = Color.White;
        public Color DefaultColorText = Color.Black;
        public Color HoveredColor = new Color(255, 255, 130);
        public Color SelectedColor = new Color(170, 170, 170);
        public Color ClickColor = new Color(0, 0, 0);

        public string strId { get; set; }

        public virtual Font Font { get => text.Font; set { text.Font = value; UpdateText(); } }
        public virtual uint TextSize {  get => text.CharacterSize; set { text.CharacterSize = value; UpdateText(); } }
        public virtual string Text { get => text.DisplayedString; set { text.DisplayedString = value; UpdateText(); } }

        public float OutlineThickness { get => rect.OutlineThickness; set => rect.OutlineThickness = value; }
        public Vector2f Size { get => rect.Size; set { rect.Size = value; UpdateText(); } }

        public Texture Texture
        {
            get => rect.Texture;
            set => rect.Texture = value;
        }

        public IntRect TextureRect
        {
            get => rect.TextureRect;
            set => rect.TextureRect = value;
        }

        public Widget(string strId, WidgetStyle style = WidgetStyle.Default)
        {
            this.strId = strId;
            this.style = style;

            rect = new RectangleShape()
            {
                Size = baseRectSize,
                FillColor = DefaultColorRect,
                OutlineColor = Color.Black,
                OutlineThickness = -2
            };

            text = new Text()
            {
                CharacterSize = 25,
                FillColor = DefaultColorText
            };

            if (style == WidgetStyle.AutoResize)
            {
                Size = new Vector2f(text.GetGlobalBounds().Width + 10, text.GetGlobalBounds().Height + TextSize);
                autoResize = true;
            }
        }

        public virtual void SubscribeEvent(RenderWindow window)
        {
            window.MouseMoved += Window_MouseMoved;
            window.MouseButtonPressed += Window_MouseButtonPressed;
            window.MouseButtonReleased += Window_MouseButtonReleased;
        }

        protected virtual void UpdateText()
        {
            text.Origin = new Vector2f(
                text.GetGlobalBounds().Width / 2f,
                text.GetGlobalBounds().Height / 2f);

            text.Position = new Vector2f(
                rect.GetGlobalBounds().Width / 2f,
                rect.GetGlobalBounds().Height / 2.5f);
        }

        protected virtual void UpdateRectSize()
        {
            Size = new Vector2f(text.GetGlobalBounds().Width, text.GetGlobalBounds().Height);
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            if(rect != null && IsRectVisible)
                target.Draw(rect, states);

            if(text != null && IsTextVisible)
                target.Draw(text, states);
        }

        public virtual bool OnClicked() => IsClicked;
        public virtual bool OnSelected() => IsSelected;
        public virtual bool OnHovered() => IsHovered;
        public FloatRect GetFloatRect() => new FloatRect(Position - (Size / 2), Size);

        public virtual void Update(float deltaTime)
        {
            if(autoResize)
            {
                Size = new Vector2f(text.GetGlobalBounds().Width + 10, text.GetGlobalBounds().Height + TextSize);
            }
        }
        protected virtual void Window_MouseButtonReleased(object? sender, MouseButtonEventArgs e) { }
        protected virtual void Window_MouseButtonPressed(object? sender, MouseButtonEventArgs e) { }
        protected virtual void Window_MouseMoved(object? sender, MouseMoveEventArgs e) 
        {
            if (GetFloatRect().Contains(e.X, e.Y))
                IsHovered = true;
            else
                IsHovered = false;
        }

        protected abstract void UpdateView();
    }
}
