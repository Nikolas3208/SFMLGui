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

            DebugRender debugRender = new DebugRender();

            window = new RenderWindow(VideoMode.DesktopMode, "SFMLGui");

            window.Resized += Window_Resized;
            window.Closed += Window_Closed;

            GuiWindow guiWindow = new GuiWindow(window, "Test", WindowStyle.Clouse);
            guiWindow.Position = new Vector2f(200, 400);
            guiWindow.SetFont(new Font("C:\\Windows\\Fonts\\Arial.ttf"));
            guiWindow.InitTitleBar();

            

            Button button = new Button("bt_1", WidgetStyle.AutoResize);
            button.TextSize = 40;
            button.Text = "Click";

            Lable lable = new Lable("lb_1", "click count");
            lable.Position = new Vector2f(button.Size.X - 5, 5);

            Slider slider = new Slider("slider", 1, min:-100, max:100, step:1f);
            slider.Position = new Vector2f(5, 100);

            Lable lable1 = new Lable("lb_2", "");
            lable1.Position = new Vector2f(5, 140);

            guiWindow.AddWidget(button);
            guiWindow.AddWidget(lable);
            guiWindow.AddWidget(slider);
            guiWindow.AddWidget(lable1);


            while (window.IsOpen)
            {
                window.DispatchEvents();

                window.Clear(Color.Black);

                float deltaTime = clock.Restart().AsSeconds();

                if (guiWindow != null)
                {
                    guiWindow.Update(deltaTime);

                    window.Draw(guiWindow);

                    if(guiWindow.GetWidget("bt_1").OnClicked())
                    {
                        i++;
                        guiWindow.GetWidget("lb_1").Text = $"Clicked: {i}";
                    }


                    lable1.Text = $"Value: {slider.Value}";

                    if (guiWindow.IsClouse)
                        guiWindow = null;
                }

                window.Draw(debugRender);

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