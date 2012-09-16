using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HeirsToTheThrone.graphics
{
    class TileNode : GraphicNode
    {
        public model.Tile tile;
        public Int32 x;
        public Int32 y;
        public MapDisplay mapDisplay;

        private Texture2D getTerrainTexture(model.TerrainType type)
        {
            switch (type)
            {
                case model.TerrainType.Bushes:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/tiles/Bushes");
                case model.TerrainType.Desert:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/tiles/Desert");
                case model.TerrainType.Forest:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/tiles/Forest");
                case model.TerrainType.Grass:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/tiles/Grass");
                case model.TerrainType.Hills:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/tiles/Hills");
                case model.TerrainType.Mountains:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/tiles/Mountains");
                case model.TerrainType.Swamp:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/tiles/Swamp");
                case model.TerrainType.Water:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/tiles/Water");
                default:
                    throw new Exception("Unknown terrain type");
            }
        }

        private Texture2D getFortificationTexture(model.FortificationType type)
        {
            switch (type)
            {
                case model.FortificationType.NotFortified:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/fortifications/NotFortified");
                case model.FortificationType.Tower:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/fortifications/Tower");
                case model.FortificationType.Castle:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/fortifications/Castle");
                case model.FortificationType.Fort:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/fortifications/Fort");
                case model.FortificationType.Palace:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/fortifications/Palace");
                case model.FortificationType.Fortress:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/fortifications/Fortress");
                case model.FortificationType.Citadel:
                    return HeirsGame.instance.Content.Load<Texture2D>("graphics/fortifications/Citadel");
                default:
                    throw new Exception("Unknown fortification type");
            }
        }

        private Texture2D getWhiteTexture()
        {
            return HeirsGame.instance.Content.Load<Texture2D>("graphics/White");
        }

        private Texture2D getFrameTexture()
        {
            return HeirsGame.instance.Content.Load<Texture2D>("graphics/PlayerFrame");
        }

        private SpriteFont getBigFrameFont()
        {
            return HeirsGame.instance.Content.Load<SpriteFont>("fonts/kooten");
        }
       
        public override void draw(SpriteBatch sb, Rectangle area)
        {
            if (tile.province != null)
            {
                if (mapDisplay == MapDisplay.Normal)
                {
                    drawTerrain(sb, bounds);
                    drawFrame(sb, bounds);
                    drawMiniFortification(sb, bounds);
                }
                else if (mapDisplay == MapDisplay.Politic)
                {
                    drawPolitic(sb, bounds);
                    drawFrame(sb, bounds);
                }
                else if (mapDisplay == MapDisplay.Economic)
                {
                    drawEconomic(sb, bounds);
                    drawFrame(sb, bounds);
                }
                else if (mapDisplay == MapDisplay.Military)
                {
                    drawMilitary(sb, bounds);
                    drawFrame(sb, bounds);
                    writeMilitary(sb, bounds);
                }
                else if (mapDisplay == MapDisplay.Fortifications)
                {
                    drawFortifications(sb, bounds);
                    drawFrame(sb, bounds);
                    drawFortification(sb, bounds);
                }
            }
            else
            {
                drawWater(sb, bounds);
            }
            

            base.draw(sb, area);
        }

        private void drawWater(SpriteBatch sb, Rectangle area)
        {
            sb.Draw(getTerrainTexture(model.TerrainType.Water), bounds, Color.White);
        }

        private void drawTerrain(SpriteBatch sb, Rectangle area)
        {
            sb.Draw(getTerrainTexture(tile.province.terrain.type), bounds, Color.White);
        }

        private void drawPolitic(SpriteBatch sb, Rectangle area)
        {
            sb.Draw(getWhiteTexture(), area, tile.province.owner.darkColor);
        }

        private void drawEconomic(SpriteBatch sb, Rectangle area)
        {
            Single value = (Single)(tile.province.getIncome() / tile.map.maxIncome);
            Color c = new Color(0, value, 0);
            sb.Draw(getWhiteTexture(), area, c);
        }

        private void drawMilitary(SpriteBatch sb, Rectangle area)
        {
            Single value = (Single)(tile.province.troops / tile.map.maxMilitary);
            Color c = new Color(value, 0, 0);
            sb.Draw(getWhiteTexture(), area, c);
        }

        private void drawFortifications(SpriteBatch sb, Rectangle area)
        {
            Single value = (Single)(tile.province.getFortificationLevel() / tile.map.maxFortifications);
            Color c = new Color(value, value, value);
            sb.Draw(getWhiteTexture(), area, c);
        }

        private void drawFrame(SpriteBatch sb, Rectangle area)
        {
            if (tile.province.seedTile == tile)
            {
                sb.Draw(getFrameTexture(), area, tile.province.owner.color);
            }
        }

        private void drawMiniFortification(SpriteBatch sb, Rectangle area)
        {
            if (tile.province.seedTile == tile)
            {
                Rectangle fortificationArea = new Rectangle();
                fortificationArea.X = area.X + area.Width / 3;
                fortificationArea.Y = area.Y + area.Height / 12;
                fortificationArea.Width = area.Width / 3;
                fortificationArea.Height = area.Width / 3;
                sb.Draw(getFortificationTexture(tile.province.fortification.type), fortificationArea, Color.White);
            }
        }

        private void drawFortification(SpriteBatch sb, Rectangle area)
        {
            if (tile.province.seedTile == tile)
            {
                Rectangle fortificationArea = new Rectangle();
                fortificationArea.X = area.X + area.Width / 12;
                fortificationArea.Y = area.Y + area.Height / 12;
                fortificationArea.Width = area.Width * 10 / 12;
                fortificationArea.Height = area.Width * 10 / 12;
                sb.Draw(getFortificationTexture(tile.province.fortification.type), fortificationArea, Color.White);
            }
        }

        private void writeMilitary(SpriteBatch sb, Rectangle area)
        {
            if (tile.province.seedTile == tile)
            {
                Vector2 size = getBigFrameFont().MeasureString(tile.province.troops.ToString());
                Vector2 origin = size / 2;
                Single scaleX = (area.Width * 0.9F) / size.X;
                Single scaleY = (area.Height * 0.9F) / size.Y;
                Single scale = Math.Min(scaleX, scaleY);
                Vector2 pos = new Vector2();
                pos.X = area.X + area.Width / 2;
                pos.Y = area.Y + area.Height / 2;
                sb.DrawString(getBigFrameFont(), tile.province.troops.ToString(), pos, Color.Black, 0, origin, scale, SpriteEffects.None, 0);
            }
        }
    }
}
