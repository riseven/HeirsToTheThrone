using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeirsToTheThrone.graphics
{
    public class GameScreen : GraphicNode
    {
        public MapNode mapNode;

        public GameScreen(model.Map map)
        {
            this.mapNode = new MapNode(map);
            children.Add(mapNode);
        }

        public override void update()
        {
            base.update();
        }

        public override void draw(SpriteBatch sb, Rectangle area)
        {
            bounds = area;

            base.draw(sb, area);
        }

        public override void keyEvent(KeyEvent e)
        {
            mapNode.keyEvent(e);
        }

    }


}
