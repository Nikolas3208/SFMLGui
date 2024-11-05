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

            Lable lable = new Lable("lb_1", "");
            lable.Position = new Vector2f(200, 400);

            layer.AddWidget(lable);

            layer.AddWidget(new Lable("lb_1", ""));


            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(Color.White);

                float deltaTime = clock.Restart().AsSeconds();

                layer.Update(deltaTime);
                layer.GetWidgetByStrId("lb_1").Text = $"FPS: {(int)(1 / deltaTime)}";
                lable.Text = $"Hovered: {button2.OnHovered()}, Clicked: {button2.OnClicked()}, Selected: {button2.OnSelected()}";

                if(button2.OnClicked())
                {
                    i++;
                    button2.Text = $"Clicked: {i}";
                }

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