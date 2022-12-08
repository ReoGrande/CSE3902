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
    public int level;
    Texture2D charPositionFill;
    List<int[]> objects;
    List<int[]> doors;
    List<int[]> rooms;

    Game1 myGame;
    Texture2D health;

    public IMap(Game1 game, int levelI){
        myGame = game;
        health = game.Content.Load<Texture2D>("item/ZeldaSpriteHeartContainer");
        font = game.Content.Load<SpriteFont>("File");
        level = levelI;
        Map = new MapLoader(game,level);
        objects = Map.getItems();
        doors = Map.getDoors();
        rooms = Map.getRooms();
        int startRoom = Map.getStartRoom();
        fullScreen = new SpriteBatch(game.GraphicsDevice);
        MapControl = new MapController(game,Map.getMap(),Map.getScreen(), objects, doors,rooms,startRoom,level);
        miniMap = Map.getMiniMap();
        miniMapPosition = new Rectangle(100,80,miniMap.Bounds.Width*3,miniMap.Bounds.Height*3);
        charPositionFill = new Texture2D(game.GraphicsDevice,1,1);
        charPositionFill.SetData<Color>(new Color[]{Color.Green});

    }

    public void drawHealth(SpriteBatch drawing,Rectangle position){
        Rectangle pos = position;
        pos.X = pos.X*6;
        int originalX = pos.X;

        pos.Y = pos.Y +30;
        int hp = myGame.character.HP();
        for(int index = 1; index <= hp; index++){
            drawing.Draw(health,new Rectangle(pos.X,pos.Y,20,20),Color.White);
            if(index%9 >0){
            pos.X = pos.X + 25;
            }else{
                pos.Y = pos.Y+ 25;
                pos.X = originalX;
            }

        }
    }
    public void Update(){
        MapControl.Update();
    }
    public void drawMiniMapUI(SpriteBatch drawing,Rectangle position){
        drawing.Draw(miniMap,position,Color.Blue);
        Rectangle tempPosition = position;
        tempPosition.X = tempPosition.X+4;
        drawing.Draw(charPositionFill,Map.changeRoom(tempPosition),Color.White);
        drawing.DrawString(font,"LEVEL-"+level,new Vector2(position.X, position.Y-30),Color.White);
        drawing.DrawString(font,"LIFE",new Vector2(position.X*7, position.Y),Color.Red);
        drawHealth(drawing,position);
    }
    public void Draw(){
        MapControl.Draw();//Displays player screen
        fullScreen.Begin();
        if(myGame._testMode){
            fullScreen.DrawString(font,"TEST MODE",new Vector2(miniMapPosition.X, miniMapPosition.Y-50),Color.White);
        }
        drawMiniMapUI(fullScreen, miniMapPosition);
        fullScreen.End();
        //Map.Draw();//Displays entire map
    }
}

}

