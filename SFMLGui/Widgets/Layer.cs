using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLGui.Widgets
{
    public class Layer : Transformable, Drawable
    {
        private List<Widget> widgets;
        private RenderWindow window;
        private Font font;

        private Vector2f position;

        public new Vector2f Position { get => position; set { position = value; } }

        public string Name;
        public uint Id = 0;
        public uint Depth = 0;

        public Layer(RenderWindow window, string name)
        {
            this.window = window;
            Name = name;

            widgets = new List<Widget>();
        }

        public void AddWidget(Widget widget)
        {
            if (!ContainsStrId(widget.strId))
            {
                widget.SubscribeEvent(window);
                widget.Position += Position;
                if (font != null && widget.Font == null)
                    widget.Font = font;
                widgets.Add(widget);
            }
            else
            {
                throw new Exception("This strId is already taken");
            }
        }

        public void UpdatePosition(Vector2f pos)
        {
            foreach (var widget in widgets)
            {
                widget.Position += pos;
            }
        }

        public T GetWidgetByType<T>(string strId) where T : Widget
        {
            foreach (var widget in widgets)
            {
                if (widget.GetType() == typeof(T) && widget.strId == strId)
                    return (T)widget;
            }

            return null;
        }
        public Widget GetWidgetByStrId(string strId)
        {
            foreach (Widget widget in widgets)
            {
                if(widget.strId == strId)
                    return widget;
            }

            return null;
        }

        public bool ContainsStrId(string strId)
        {
            foreach (var widget in widgets)
            {
                if(widget.strId == strId)
                    return true;
            }

            return false;
        }

        public void RemoveWidget(Widget widget) => widgets.Remove(widget);
        public bool RemoveWidgetByStrId(string strId)
        {
            Widget w = null;

            foreach (var widget in widgets)
            {
                if(widget.strId == strId)
                    w = widget;
            }

            if (w != null)
            {
                widgets.Remove(w);

                return true;
            }

            return false;
        }

        public void SetFont(Font font) => this.font = font;
        public Font GetFont() => this.font;

        public void Update(float deltaTime)
        {
            foreach (var widget in widgets)
            {
                widget.Update(deltaTime);
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            //states.Transform *= Transform;

            foreach (var widget in widgets)
            {
                widget.Draw(target, states);
            }
        }
    }
}
