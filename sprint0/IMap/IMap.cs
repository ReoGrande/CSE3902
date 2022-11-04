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
    public MapLoader Map;
    public MapController MapControl;
    //public IBound MapBounds;
    int level;

    public IMap(Game1 game){
        level = 1;
        Map = new MapLoader(game,level);
        MapControl = new MapController(game,Map.getMap(),Map.getScreen());
        //MapBounds = new IBound(level);
    }

    public void Update(){
        MapControl.Update();
    }
    public void Draw(){
        MapControl.Draw();//Displays player screen
        //Map.Draw();//Displays entire map
    }
}

}

