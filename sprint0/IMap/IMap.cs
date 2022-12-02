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
    Game1 myGame;
    Texture2D health;

    public IMap(Game1 game){
        myGame = game;
        health = game.Content.Load<Texture2D>("item/ZeldaSpriteHeartContainer");
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

    public void drawHealth(SpriteBatch drawing,Rectangle position){
        Rectangle pos = position;
        pos.X = pos.X*6;
        pos.Y = pos.Y +30;
        int hp = myGame.character.HP();
        for(int index = 0; index < hp; index++){
            drawing.Draw(health,new Rectangle(pos.X,pos.Y,20,20),Color.White);
            pos.X = pos.X + 25;

        }
    }
    public void Update(){
        MapControl.Update();
    }
    public void drawMiniMapUI(SpriteBatch drawing,Rectangle position){
        drawing.Draw(miniMap,position,Color.Blue);
        drawing.Draw(charPositionFill,Map.changeRoom(position),Color.White);
        drawing.DrawString(font,"LEVEL-"+level,new Vector2(position.X, position.Y-30),Color.White);
        drawing.DrawString(font,"LIFE",new Vector2(position.X*7, position.Y),Color.Red);
        drawHealth(drawing,position);
    }
    public void Draw(){
        MapControl.Draw();//Displays player screen
        fullScreen.Begin();
        drawMiniMapUI(fullScreen, miniMapPosition);
        fullScreen.End();
        //Map.Draw();//Displays entire map
    }
}

}

