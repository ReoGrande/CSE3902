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
    SpriteFont font;
    Texture2D miniMap;
    Rectangle miniMapPosition;
    int level;
    Texture2D charPositionFill;
    List<int[]> objects;
    List<int[]> doors;

    public IMap(Game1 game){
        //not working with font
        font = game.Content.Load<SpriteFont>("File");
        level = 1;
        Map = new MapLoader(game,level);
        objects = Map.getItems();
        doors = Map.getDoors();
        fullScreen = new SpriteBatch(game.GraphicsDevice);
        MapControl = new MapController(game,Map.getMap(),Map.getScreen(), objects, doors);
        miniMapPosition = new Rectangle(100,100,130,70);
        miniMap = Map.getMiniMap();
        charPositionFill = new Texture2D(game.GraphicsDevice,1,1);
        charPositionFill.SetData<Color>(new Color[]{Color.Green});

        //MapBounds = new IBound(level);
    }
    public void Update(){
        MapControl.Update();
    }
    public void drawMiniMapUI(Rectangle position){
        fullScreen.Begin();
        fullScreen.Draw(miniMap,position,Color.Blue);
        fullScreen.Draw(charPositionFill,Map.changeRoom(position),Color.White);
        fullScreen.DrawString(font,"LEVEL-"+level,new Vector2(position.X, position.Y-30),Color.White);
        fullScreen.DrawString(font,"LIFE",new Vector2(position.X*7, position.Y),Color.Red);
        fullScreen.End();

    }
    public void Draw(){
        MapControl.Draw();//Displays player screen
        drawMiniMapUI(miniMapPosition);
        //Map.Draw();//Displays entire map
    }
}

}

