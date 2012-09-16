using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeirsToTheThrone.model
{
    public class Map
    {
        public Tile[][] tiles;
        public List<Province> provinces = new List<Province>();
        public Double maxIncome;
        public Double maxMilitary;
        public Double maxFortifications;

        protected Map(GameConfiguration conf)
        {
            tiles = new Tile[conf.MapWidth][];
            for (int x = 0; x < conf.MapWidth; x++)
            {
                tiles[x] = new Tile[conf.MapHeight];
            }
        }

        public static Map CreateMap(GameConfiguration conf)
        {
            Map map = new Map(conf);
            map.generate(conf);
            return map;
        }

        private void generate(GameConfiguration conf)
        {
            Random random = new Random();

            // First step is generate the tiles
            for (int x = 0; x < conf.MapWidth; x++)
            {
                for (int y = 0; y < conf.MapHeight; y++)
                {
                    tiles[x][y] = new Tile();
                    tiles[x][y].map = this;
                }
            }

            // Stablish tiles adjacencies
            for ( int x = 0 ; x < conf.MapWidth ; x++)
            {
                for (int y = 0; y < conf.MapHeight; y++)
                {
                    Tile tile = tiles[x][y];
                    if (x > 0)
                    {
                        tile.adjacentTiles[(int)Direction.left] = tiles[x - 1][y];
                    }
                    if (y > 0)
                    {
                        tile.adjacentTiles[(int)Direction.up] = tiles[x][y - 1];
                    }
                    if (x < conf.MapWidth - 1)
                    {
                        tile.adjacentTiles[(int)Direction.right] = tiles[x + 1][y];
                    }
                    if (y < conf.MapHeight - 1)
                    {
                        tile.adjacentTiles[(int)Direction.down] = tiles[x][y + 1];
                    }
                }
            }

            // Create provinces
            for (int i = 0; i < conf.NumProvinces; i++)
            {
                Province p = new Province();
                
                // Assign random terrain
                Array terrains = Enum.GetValues(typeof(TerrainType));
                do
                {
                    p.terrain = Terrain.forType((TerrainType)terrains.GetValue(random.Next(terrains.Length)));
                } while (p.terrain.type == TerrainType.Water);
                p.villages = random.Next(1, 6);
                p.troops = random.Next(1, 1000);
                p.fortification = Fortification.forType((FortificationType) random.Next(0, 7));

                // Assign random start position
                bool validPos = false;
                while (!validPos)
                {
                    int x = random.Next(conf.MapWidth);
                    int y = random.Next(conf.MapHeight);

                    if (tiles[x][y].province == null)
                    {
                        tiles[x][y].province = p;
                        p.tiles.Add(tiles[x][y]);
                        p.seedTile = tiles[x][y];
                        validPos = true;
                    }
                }

                provinces.Add(p);
            }

            // Calculate initial province adjacent tiles
            foreach (Province province in provinces)
            {
                foreach (Tile tile in province.tiles)
                {
                    foreach (Tile atile in tile.adjacentTiles)
                    {
                        if (atile != null)
                        {
                            if (atile.province != tile.province)
                            {
                                province.adjacentTiles.Add(atile);
                            }
                        }
                    }
                }
            }

            // Grow randomly each province to a random adjacent tile
            int numIters = ((int) ((conf.MapWidth * conf.MapHeight) * (1.0 - conf.waterPercentage))) - provinces.Count;
            for ( int i = 0 ; i < numIters ; i++ )
            {
                Province growProvince = provinces.ElementAt(random.Next(provinces.Count));
                bool validTile = false;
                for (int numTry = 0; numTry < 20 && !validTile; numTry++)
                {
                    Tile growTile = growProvince.adjacentTiles.ElementAt(random.Next(growProvince.adjacentTiles.Count));
                    if (growTile.province == null)
                    {
                        validTile = true;
                        growTile.province = growProvince;
                        growProvince.tiles.Add(growTile);
                        growProvince.adjacentTiles.Remove(growTile);
                        foreach (Tile newAdjacentTile in growTile.adjacentTiles)
                        {
                            if (newAdjacentTile != null)
                            {
                                if (newAdjacentTile.province != growProvince)
                                {
                                    growProvince.adjacentTiles.Add(newAdjacentTile);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void calculateMaxValues()
        {
            maxIncome = 0;
            maxMilitary = 0;
            maxFortifications = 0;
            foreach (Province p in provinces)
            {
                if (p.getIncome() > maxIncome)
                {
                    maxIncome = p.getIncome();
                }
                if (p.troops > maxMilitary)
                {
                    maxMilitary = p.troops;
                }
                if (p.getFortificationLevel() > maxFortifications)
                {
                    maxFortifications = p.getFortificationLevel();
                }
            }
        }
    }
}
