using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeirsToTheThrone.model
{
    public class Tile
    {
        public Map map;
        public Province province;
        public Tile[] adjacentTiles = new Tile[4];
    }
}
