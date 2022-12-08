using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace sprint0
{

public class MapLoader{
    Rectangle screen;
    Texture2D map;
    Texture2D miniMap;
    Game1 myGame;
    SpriteBatch tempDraw;
    string levelname;
    int startRoom;

    

    public MapLoader(Game1 game, int level){
        levelname = "maps/Level"+level+"Zelda";
        map = game.Content.Load<Texture2D>(levelname);
        miniMap = game.Content.Load<Texture2D>(levelname+"Icon");
        myGame = game;
        tempDraw = new SpriteBatch(myGame.GraphicsDevice);
        screen = new Rectangle(0,0, myGame.GraphicsDevice.PresentationParameters.BackBufferWidth, myGame.GraphicsDevice.PresentationParameters.BackBufferHeight);
        
    }
   
   public Texture2D getMiniMap(){
        return miniMap;
   }

    public Texture2D getMap(){
        return map;
    }
    
    public int getStartRoom(){
        return startRoom;
    }

    public Rectangle changeRoom(Rectangle miniMap){
        Rectangle roomSpot = new Rectangle(miniMap.X,miniMap.Y+2,9,7);
        Rectangle[] rooms = myGame._currentMap.MapControl.getRooms();
        int roomNum = myGame._currentMap.MapControl.getRoomNum();
        roomSpot.X += (int)Math.Ceiling((double)rooms[roomNum].X/10.8);
        roomSpot.Y += (int)Math.Ceiling((double)rooms[roomNum].Y/15.0);
        return roomSpot;
    }
    public Rectangle getScreen(){
        return screen;
    }

    public List<int[]> getItems(){
        List<int[]> allItems = new List <int[]>();
        var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false
        };
        using var streamReaderItems = File.OpenText("Content/"+levelname+"Items.csv");
        using var csvReaderItems = new CsvReader(streamReaderItems, csvConfig);
        string value;
        int[] item;
        int spotItem;
        if(csvReaderItems.Read()){
        while (csvReaderItems.Read())
        {
            item = new int[7];
            spotItem = 0;
            for (int i = 0; csvReaderItems.TryGetField<string>(i, out value); i++)
            {
                    try{
                    item[spotItem] = Int32.Parse(value);
                    spotItem = spotItem+1;
                    }catch{
                        Console.WriteLine("Cannot parse integer from file Items");
                    }
            }
            allItems.Add(item);
        }
        }
        streamReaderItems.Close();
        return allItems;
    }
    public List<int[]> getRooms(){
        List<int[]> rooms = new List<int[]>();
        var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false
        };
        using var streamReaderDoors = File.OpenText("Content/"+levelname+"Bounds.csv");
        using var csvReaderDoors = new CsvReader(streamReaderDoors, csvConfig);
        string value;
        int[] item;
        int spotItem;
        if(csvReaderDoors.Read()){
            csvReaderDoors.Read();
            csvReaderDoors.TryGetField<int>(0, out startRoom);

        while (csvReaderDoors.Read())
        {
            item = new int[7];
            spotItem = 0;
            for (int i = 0; csvReaderDoors.TryGetField<string>(i, out value); i++)
            {
                    try{
                    item[spotItem] = Int32.Parse(value);
                    spotItem = spotItem+1;
                    }catch{
                        Console.WriteLine("Cannot parse integer from file doors");
                    }
            }
            rooms.Add(item);
        }
        }
        streamReaderDoors.Close();
        return rooms;
    }
    public List<int[]> getDoors(){
        List<int[]> allDoors = new List <int[]>();
        var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false
        };
        using var streamReaderDoors = File.OpenText("Content/"+levelname+"Doors.csv");
        using var csvReaderDoors = new CsvReader(streamReaderDoors, csvConfig);
        string value;
        int[] item;
        int spotItem;
        if(csvReaderDoors.Read()){
        while (csvReaderDoors.Read())
        {
            item = new int[7];
            spotItem = 0;
            for (int i = 0; csvReaderDoors.TryGetField<string>(i, out value); i++)
            {
                    try{
                    item[spotItem] = Int32.Parse(value);
                    Console.Write(value+" ");
                    spotItem = spotItem+1;
                    }catch{
                        Console.WriteLine("Cannot parse integer from file doors");
                    }
            }
            Console.WriteLine();
            allDoors.Add(item);
        }
        }

        streamReaderDoors.Close();
        return allDoors;
    }
    public void Draw(){
        tempDraw.Begin();
        tempDraw.Draw(map,screen,Color.White);
        tempDraw.End();
    }

}

}