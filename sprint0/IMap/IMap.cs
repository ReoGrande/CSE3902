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
    SpriteBatch fullScreen;
    Texture2D miniMap;
    Rectangle miniMapPosition;
    int level;
    Texture2D charPositionFill;

    public IMap(Game1 game){
        level = 1;
        Map = new MapLoader(game,level);
        fullScreen = new SpriteBatch(game.GraphicsDevice);
        MapControl = new MapController(game,Map.getMap(),Map.getScreen());
        miniMapPosition = new Rectangle(100,100,130,70);
        miniMap = Map.getMiniMap();
        charPositionFill = new Texture2D(game.GraphicsDevice,1,1);
        charPositionFill.SetData<Color>(new Color[]{Color.Green});

        //MapBounds = new IBound(level);
    }
    public void Update(){
        MapControl.Update();
    }
    public void Draw(){
        MapControl.Draw();//Displays player screen
        fullScreen.Begin();
        fullScreen.Draw(miniMap,miniMapPosition,Color.Blue);
        fullScreen.Draw(charPositionFill,Map.changeRoom(miniMapPosition),Color.White);
        fullScreen.End();
        //Map.Draw();//Displays entire map
    }
}

}

