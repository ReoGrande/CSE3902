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
    Game1 myGame;
    SpriteBatch tempDraw;
    string levelname;

    

    public MapLoader(Game1 game, int level){
        levelname = "maps/Level"+level+"Zelda";
        map = game.Content.Load<Texture2D>(levelname);
        myGame = game;
        tempDraw = new SpriteBatch(myGame.GraphicsDevice);
        screen = new Rectangle(0,0, myGame.GraphicsDevice.PresentationParameters.BackBufferWidth, myGame.GraphicsDevice.PresentationParameters.BackBufferHeight);
    }
    public Texture2D getMap(){
        return map;
    }
    public Rectangle getScreen(){
        return screen;
    }
     public void LoadItemsPerRoom(string room){

        var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false
        };

        using var streamReader = File.OpenText(levelname+"Items.csv");
        using var csvReader = new CsvReader(streamReader, csvConfig);
        string value;

        while (csvReader.Read())
        {
            for (int i = 0; csvReader.TryGetField<string>(i, out value); i++)
            {
             Console.Write($"{value} ");
            }

            Console.WriteLine();
        }
     }
    public void Draw(){
        LoadItemsPerRoom("1");
        tempDraw.Begin();
        tempDraw.Draw(map,screen,Color.White);
        tempDraw.End();
    }

}

}