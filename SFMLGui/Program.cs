using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFMLGui.Widgets;
using SFMLGui.Widgets.WidgetList;

namespace SFMLGui
{
    public class Program
    {
        private static RenderWindow window;
        public static void Main()
        {
            window = new RenderWindow(VideoMode.DesktopMode, "SFMLGui");

            window.Resized += Window_Resized;
            window.Closed += Window_Closed;

            Layer layer = new Layer(window, "leyer");

            Button button = new Button("bt_1", "Click my");
            button.SetFont(new Font("C:\\Windows\\Fonts\\Arial.ttf"));
            button.Position = new Vector2f(100, 500);

            layer.AddWidget(button);

            while(window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(Color.Cyan);

                layer.Update();

                window.Draw(layer);

                window.Display();
            }
        }

        private static void Window_Closed(object? sender, EventArgs e)
        {
            window.Close();
        }

        private static void Window_Resized(object? sender, SizeEventArgs e)
        {
            window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
        }
    }
}