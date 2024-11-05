using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFMLGui.Widgets
{
    public class LayerStack : Drawable
    {
        private List<Layer> layers = new List<Layer>();

        public LayerStack() { }

        public void AddLayer(Layer layer)
        {
            layer.Id = (uint)layers.Count;
            layers.Add(layer);
        }

        public Layer GetLayer(uint id)
        {
            foreach (Layer layer in layers)
            {
                if(layer.Id == id) return layer;
            }

            return null;
        }

        public void RemoveLayer(Layer layer)
        {
            layers.Remove(layer);
        }

        public bool RemoveLayerById(int id)
        {
            if(id < layers.Count)
            {
                layers.RemoveAt(id);
                return true;
            }
            else
                throw new Exception("This id is outside the scope of the list");
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach(Layer layer in layers)
            {
                layer.Draw(target, states);
            }
        }
    }
}
