using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeirsToTheThrone.model
{
    public class Province
    {
        public Terrain terrain;
        public Fortification fortification;
        public List<Tile> tiles = new List<Tile>();
        public List<Tile> adjacentTiles = new List<Tile>();
        public Tile seedTile;
        public List<Province> adjacentProvinces = new List<Province>();
        public Player owner;
        public Int32 villages = 0;
        public Int32 troops = 0;

        public Double getIncome()
        {
            return villages * terrain.incomeMultiplier;
        }
        public Double getFortificationLevel()
        {
            return fortification.defenseBonus + terrain.defenseBonus;
        }
    }
}
