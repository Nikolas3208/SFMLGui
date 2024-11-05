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

            GuiWindow guiWindow = new GuiWindow(window, "Test");
            guiWindow.SetFont(new Font("C:\\Windows\\Fonts\\Arial.ttf"));
            guiWindow.Position = new Vector2f(200, 400);
            guiWindow.InitTitleBar();

            Button button = new Button("");
            button.Font = new Font("C:\\Windows\\Fonts\\Arial.ttf");
            button.SubscribeEvent(window);


            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(Color.Cyan);

                float deltaTime = clock.Restart().AsSeconds();

                guiWindow.Update(deltaTime);

                window.Draw(guiWindow);

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