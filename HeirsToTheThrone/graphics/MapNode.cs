using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HeirsToTheThrone.graphics
{
    public class MapNode : GraphicNode
    {
        public model.Map map;
        public MapDisplay mapDisplay = MapDisplay.Normal;
        public MapZoom mapZoom = MapZoom.Panomaric;

        private double zoom = 100.0;

        public MapNode(model.Map map)
        {
            this.map = map;
            
            for ( int x = 0 ; x < map.tiles.Length ; x++)
            {
                for (int y = 0; y < map.tiles[x].Length; y++ )
                {
                    TileNode tn = new TileNode();
                    tn.tile = map.tiles[x][y];
                    tn.x = x;
                    tn.y = y;
                    children.Add(tn);
                }
            }
        }

        public override void update()
        {
            base.update();
        }

        public override void draw(SpriteBatch sb, Rectangle area)
        {
            bounds = area;

            calculateZoom();
            map.calculateMaxValues();

            // Adjust children bounds
            foreach (TileNode tn in children)
            {
                /*
                tn.bounds = new Rectangle(
                    tn.x * area.Width / map.tiles.Length,
                    tn.y * area.Height / map.tiles[0].Length,
                    (tn.x + 1) * area.Width / map.tiles.Length,
                    (tn.y + 1) * area.Height / map.tiles[0].Length);*/
                tn.bounds = new Rectangle(
                    (int)(tn.x * zoom),
                    (int)(tn.y * zoom),
                    (int)((tn.x + 1) * zoom),
                    (int)((tn.y + 1) * zoom));
                tn.bounds.Width -= tn.bounds.X;
                tn.bounds.Height -= tn.bounds.Y;
                tn.mapDisplay = mapDisplay;
            }

            sb.Begin(SpriteBlendMode.AlphaBlend);
            base.draw(sb, area);
            sb.End();
        }

        public void nextMapDisplay()
        {
            Array values = Enum.GetValues(typeof(MapDisplay));
            int index = Array.IndexOf(values, mapDisplay);
            index = (index+1) % values.Length;
            mapDisplay = (MapDisplay) values.GetValue(index);
        }

        public void nextMapZoom()
        {
            Array values = Enum.GetValues(typeof(MapZoom));
            int index = Array.IndexOf(values, mapZoom);
            index = (index + 1) % values.Length;
            mapZoom = (MapZoom)values.GetValue(index);
        }

        private void calculateZoom()
        {
            // Calculate desired zoom
            int w = map.tiles.Length;
            int h = map.tiles[0].Length;

            double wRel = 1.0 * bounds.Width / w;
            double hRel = 1.0 * bounds.Height / h;

            double rel = 1.0;
            
            if (mapZoom == MapZoom.Panomaric) {
                rel = Math.Min(wRel, hRel);
            } else if (mapZoom == MapZoom.Normal) {
                rel = 1.0 * bounds.Width / 10;
            } else {
                throw new Exception("Unknown map zoom");
            }

            zoom += (rel - zoom) / 5;
        }

        public override void keyEvent(KeyEvent e)
        {
            if (e.key == Keys.LeftShift && e.pressed){
                this.nextMapDisplay();
            } else if (e.key == Keys.LeftControl && e.pressed){
                this.nextMapZoom();
            }


            base.keyEvent(e);
        }
    }
}
