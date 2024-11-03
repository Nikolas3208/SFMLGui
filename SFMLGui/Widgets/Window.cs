﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLGui.Widgets
{
    public class Window
    {
        private List<Layer> layers;

        public string Name;

        public Window(string name)
        {
            Name = name;

            layers = new List<Layer>();
        }
    }
}
