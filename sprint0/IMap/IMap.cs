using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0
{

public class IMap{
    MapLoader Map;
    MapController MapControl;
    int level;

    public IMap(Game1 game){
        level = 0;
        Map = new MapLoader(game,level);
        MapControl = new MapController();
    }
}

}

