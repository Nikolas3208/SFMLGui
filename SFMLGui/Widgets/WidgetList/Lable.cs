using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLGui.Widgets.WidgetList
{
    public class Lable : Widget
    {
        public Lable(string strId, string text) : base(strId)
        {
            Text = text;
            IsRectVisible = false;
        }

        public Lable(string strId, string text, Font font) : this(strId, text)
        {
            Font = font;
        }

        public override Font Font { get => base.Font; set { base.Font = value; UpdateRectSize(); }  }
        public override string Text { get => base.Text; set { base.Text = value; UpdateRectSize(); } }
        public override uint TextSize { get => base.TextSize; set { base.TextSize = value; UpdateRectSize(); } }

        protected override void UpdateView()
        {
            
        }
    }
}
