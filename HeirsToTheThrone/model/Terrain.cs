using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeirsToTheThrone.model
{
    public class Terrain
    {
        public TerrainType type;
        public Double incomeMultiplier;
        public Double defenseBonus;

        private static Dictionary<TerrainType, Terrain> terrains = new Dictionary<TerrainType,Terrain>();

        private Terrain()
        {
        }

        public static Terrain forType(TerrainType type){
            Terrain terrain;
            if (terrains.TryGetValue(type, out terrain)){
                return terrain;
            }
            terrain = createForType(type);
            terrains.Add(type, terrain);
            return terrain;
        }

        private static Terrain createForType(TerrainType type)
        {
            Terrain terrain = new Terrain();
            terrain.type = type;
            switch (type)
            {
                case TerrainType.Bushes:
                    terrain.incomeMultiplier = 1.4;
                    terrain.defenseBonus = 0.30;
                    break;
                case TerrainType.Desert:
                    terrain.incomeMultiplier = 1.0;
                    terrain.defenseBonus = 0.25;
                    break;
                case TerrainType.Forest:
                    terrain.incomeMultiplier = 1.3;
                    terrain.defenseBonus = 0.35;
                    break;
                case TerrainType.Grass:
                    terrain.incomeMultiplier = 1.5;
                    terrain.defenseBonus = 0.25;
                    break;
                case TerrainType.Mountains:
                    terrain.incomeMultiplier = 1.1;
                    terrain.defenseBonus = 0.50;
                    break;
                case TerrainType.Swamp:
                    terrain.incomeMultiplier = 1.0;
                    terrain.defenseBonus = 0.45;
                    break;
                case TerrainType.Hills:
                    terrain.incomeMultiplier = 1.2;
                    terrain.defenseBonus = 0.40;
                    break;
                case TerrainType.Water:
                    terrain.incomeMultiplier = 0.0;
                    terrain.defenseBonus = 0.0;
                    break;
                default:
                    throw new Exception("Unknown terrain type");
            }
            return terrain;
        }
    }
}
