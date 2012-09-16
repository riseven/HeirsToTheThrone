using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeirsToTheThrone.graphics
{
    public class GraphicNode
    {
        public Rectangle bounds = new Rectangle();
        protected List<GraphicNode> children = new List<GraphicNode>();

        public virtual void update()
        {
            foreach (GraphicNode node in children)
            {
                node.update();
            }
        }

        public virtual void draw(SpriteBatch sb, Rectangle area)
        {
            foreach (GraphicNode node in children)
            {
                Rectangle childArea = new Rectangle(area.X + bounds.X, area.Y + bounds.Y, bounds.Width, bounds.Height);
                if ( childArea.X > area.Width || 
                    childArea.Y > area.Height || 
                    childArea.X + childArea.Width < 0 ||
                    childArea.Y + childArea.Height < 0)
                {
                    continue;
                }
                node.draw(sb, childArea);
            }
        }

        public virtual void keyEvent(KeyEvent e)
        {
            // Ignore this event
        }
    }
}
