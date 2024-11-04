using SFML.Graphics;
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

        public string Name;

        public Layer(RenderWindow window, string name)
        {
            this.window = window;
            Name = name;

            widgets = new List<Widget>();
        }

        public void AddWidget(Widget widget) { widget.SetWindow(window); widgets.Add(widget); }

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

        public void Update(float deltaTime = 0)
        {
            foreach (var widget in widgets)
            {
                widget.Update(deltaTime);
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            foreach (var widget in widgets)
            {
                widget.Draw(target, states);
            }
        }
    }
}
