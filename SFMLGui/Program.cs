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
            int i = 0;
            Clock clock = new Clock();

            window = new RenderWindow(VideoMode.DesktopMode, "SFMLGui");

            window.Resized += Window_Resized;
            window.Closed += Window_Closed;

            Layer layer = new Layer(window, "leyer");
            layer.SetFont(new Font("C:\\Windows\\Fonts\\Arial.ttf"));

            Button button = new Button("bt_1");
            button.Position = new Vector2f(100, 500);
            button.Click += Button_Click;

            layer.AddWidget(button);

            Button button2 = new Button("bt_2", "Hello");
            button2.Position = new Vector2f(1000, 500);

            layer.AddWidget(button2);

            layer.AddWidget(new Lable("lb_1", "Hello my frends"));

            TextBox textBox = new TextBox("tb_1");
            textBox.Position = new Vector2f(400, 500);

            layer.AddWidget(textBox);


            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(Color.White);

                float deltaTime = clock.Restart().AsSeconds();

                layer.Update(deltaTime);
                layer.GetWidgetByStrId("lb_1").Text = $"FPS: {(int)(1 / deltaTime)}";
                //button2.Position = new Vector2f(button2.Position.X + 30 * deltaTime, button2.Position.Y);

                window.Draw(layer);

                window.Display();
            }
        }

        private static void Button_Click(Button button)
        {
            button.Text = "Hello";
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