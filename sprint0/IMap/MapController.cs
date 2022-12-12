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
using System.ComponentModel;

namespace sprint0
{

    public class MapController
    {
        Texture2D allMap;
        Rectangle currentScreen;
        Game1 myGame;
        SpriteBatch drawScreen;
        Rectangle screenSize;
        int unlocked = 0;
        int offset = 43;
        int roomX;
        int roomY;
        Rectangle[] rooms;
        Rectangle currentRoom;
        private Rectangle[] unlockedCurrentRoomDoors;
        private Rectangle[] lockedCurrentRoomDoors;

        private List<int[]> currentRoomDoors;
        private Rectangle[] bounds;
        Boolean changed;
        int roomNum;
        Boolean overrided;
        Texture2D tempFill;
        List<int[]> objects;
        List<int[]> doors;
        public List<IEnemy> enemy;
        List<int[]> ePos;
        public List<IItem> iitem;
        List<int[]> iPos;
        int levelI;

        public MapController(Game1 game, Texture2D map, Rectangle screen, List<int[]> obj, List<int[]> inDoors, List<int[]> inRooms, int startRoom, int level)
        {
            currentRoomDoors = new List<int[]>();
            ePos = new List<int[]>();
            enemy = new List<IEnemy>();
            iPos = new List<int[]>();
            iitem = new List<IItem>();
            levelI = level;
            objects = obj;
            doors = inDoors;
            tempFill = new Texture2D(game.GraphicsDevice, 1, 1);
            tempFill.SetData<Color>(new Color[] { Color.White });
            changed = false;
            overrided = false;
            allMap = map;
            currentScreen = screen;
            myGame = game;
            drawScreen = new SpriteBatch(game.GraphicsDevice);
            screenSize = game._playerScreen;
            rooms = loadRooms(inRooms);
            bounds = new Rectangle[4];//the number of sides a room has
            bounds[0] = new Rectangle(screenSize.X, screenSize.Y + (((int)Math.Ceiling(offset * 0.8))), screenSize.Width, offset);//top side
            bounds[1] = new Rectangle(screenSize.X + (offset), screenSize.Y, offset, screenSize.Height);//left side
            bounds[2] = new Rectangle(screenSize.X, screenSize.Y + screenSize.Height - (((int)Math.Ceiling(offset * 1.7))), screenSize.Width, offset);//bottom side
            bounds[3] = new Rectangle(screenSize.X + screenSize.Width - (((int)Math.Ceiling(offset * 1.7))), screenSize.Y, offset, screenSize.Height);//right side

            currentRoom = rooms[startRoom];
            roomNum = startRoom;
            roomX = currentRoom.X;
            roomY = currentRoom.Y;
            LoadBoundsPerRoom();
            drawObjects();
        }
        public Rectangle[] loadRooms(List<int[]> tempRoomsL)
        {
            Rectangle[] tempRooms = new Rectangle[tempRoomsL.Count()];
            for (int index = 0; index < tempRoomsL.Count(); index++)
            {
                tempRooms[index] = new Rectangle(tempRoomsL[index][1], tempRoomsL[index][2], tempRoomsL[index][3], tempRoomsL[index][4]);
            }

            return tempRooms;
        }
        public Rectangle[] getRoomDoors()
        {
            return unlockedCurrentRoomDoors;
        }
        public Rectangle[] getLockedRoomDoors()
        {
            return lockedCurrentRoomDoors;
        }
        public Rectangle[] getRoomBounds()
        {
            return bounds;
        }

        public Rectangle[] getRooms()
        {
            return rooms;
        }
        public int getRoomNum()
        {
            return roomNum;
        }


        public void DisplayItem(int[] item)
        {
            Rectangle itemDetail = new Rectangle(screenSize.X + item[2], screenSize.Y + item[3], item[4], item[5]);
            if (item[1] < 9)
            {
                switch (item[1])
                {
                    case 0://SquareBlock
                        myGame.blockSpace.Add(BlockFactory.Instance.CreateSquareBlock(itemDetail));
                        break;
                    case 1://Pushable block
                        myGame.blockSpace.Add(BlockFactory.Instance.CreatePushAbleBlock(itemDetail));
                        break;
                    case 2://Fire
                        myGame.blockSpace.Add(BlockFactory.Instance.CreateFire(itemDetail));
                        break;
                    case 3://Blue gap
                        myGame.blockSpace.Add(BlockFactory.Instance.CreateBlueGap(itemDetail));
                        break;
                    case 4://Stairs
                        myGame.blockSpace.Add(BlockFactory.Instance.CreateStairs(itemDetail));
                        break;
                    case 5://Destroyable block
                        myGame.blockSpace.Add(BlockFactory.Instance.CreateDestroyableBlock(itemDetail));
                        break;
                    case 6://Ladder
                        myGame.blockSpace.Add(BlockFactory.Instance.CreateLadder(itemDetail));
                        break;
                    case 7://Blue floor
                        myGame.blockSpace.Add(BlockFactory.Instance.CreateBlueFloor(itemDetail));
                        break;
                    case 8://Blue sand
                        myGame.blockSpace.Add(BlockFactory.Instance.CreateBlueSand(itemDetail));
                        break;
                    default:
                        Console.WriteLine("Invalid item ID");
                        break;

                }
            }
            else if (item[1] < 23)
            {
                IItem temp;
                iPos.Add(item);
                switch (item[1])
                {

                    case 9://Compass
                        temp = ItemFactory.Instance.CreateCompass(itemDetail);
                        iitem.Add(temp);
                        myGame.outItemSpace.Add(temp);
                        break;
                    case 10://Map
                        temp = ItemFactory.Instance.CreateMap(itemDetail);
                        iitem.Add(temp);
                        myGame.outItemSpace.Add(temp);
                        break;
                    case 11://Key
                        temp = ItemFactory.Instance.CreateKey(itemDetail);
                        iitem.Add(temp);
                        myGame.outItemSpace.Add(temp);
                        break;
                    case 12://Heart
                        temp = ItemFactory.Instance.CreateHeart(itemDetail);
                        iitem.Add(temp);
                        myGame.outItemSpace.Add(temp);
                        break;
                    case 13://TriForcePiece
                        temp = ItemFactory.Instance.CreateTriforcePiece(itemDetail);
                        iitem.Add(temp);
                        myGame.outItemSpace.Add(temp);
                        break;
                    case 14://WoodenBoomerang
                        temp = ItemFactory.Instance.CreateWoodenBoomerang(itemDetail);
                        temp.ChangeAttribute(ItemAttribute.WaitForPick);
                        iitem.Add(temp);
                        myGame.outItemSpace.Add(temp);
                        break;
                    case 15://Bow
                        temp = ItemFactory.Instance.CreateBow(itemDetail);
                        iitem.Add(temp);
                        myGame.outItemSpace.Add(temp);
                        break;
                    case 16://Rupee
                        temp = ItemFactory.Instance.Createrupee(itemDetail);
                        temp.ChangeAttribute(ItemAttribute.WaitForPick);
                        iitem.Add(temp);
                        myGame.outItemSpace.Add(temp);
                        break;
                    case 17://Arrow
                        temp = ItemFactory.Instance.CreateArrow(itemDetail);
                        temp.ChangeAttribute(ItemAttribute.WaitForPick);
                        temp.SetPickable(true);
                        iitem.Add(temp);
                        myGame.outItemSpace.Add(temp);
                        break;
                    case 18://Bomb
                        temp = ItemFactory.Instance.CreateBomb(itemDetail);
                        temp.SetPickable(true);
                        iitem.Add(temp);
                        myGame.outItemSpace.Add(temp);
                        break;
                    case 19://Fairy
                        temp = ItemFactory.Instance.CreateFairy(itemDetail);
                        iitem.Add(temp);
                        myGame.outItemSpace.Add(temp);
                        break;
                    case 20://Clock
                        temp = ItemFactory.Instance.CreateClock(itemDetail);
                        iitem.Add(temp);
                        myGame.outItemSpace.Add(temp);
                        break;

                    //need BlueCandle then
                    case 21://Staff
                        temp = ItemFactory.Instance.CreateStaff(itemDetail);
                        iitem.Add(temp);
                        myGame.outItemSpace.Add(temp);
                        break;
                    //need BluePotion then
                    case 22://pickaxe
                        temp = ItemFactory.Instance.CreatePickaxe(itemDetail);
                        iitem.Add(temp);
                        myGame.outItemSpace.Add(temp);
                        break;
                    default:
                        Console.WriteLine("Invalid item ID");
                        break;
                }
            }
            else
            {
                IEnemy temp;
                ePos.Add(item);
                switch (item[1])
                {

                    case 23://Boss
                        temp = EnemyFactory.Instance.CreateBoss(itemDetail);
                        enemy.Add(temp);
                        myGame.enemySpace.Add(temp);
                        break;
                    case 24://Bat
                        temp = EnemyFactory.Instance.CreateBat(itemDetail);
                        enemy.Add(temp);
                        temp.SetMovePattern(random());
                        myGame.enemySpace.Add(temp);
                        break;
                    case 25://Skeleton
                        temp = EnemyFactory.Instance.CreateSkeleton(itemDetail);
                        enemy.Add(temp);
                        temp.SetMovePattern(random());
                        myGame.enemySpace.Add(temp);
                        break;
                    case 26://Rope
                        temp = EnemyFactory.Instance.CreateRope(itemDetail);
                        enemy.Add(temp);
                        temp.SetMovePattern(random());
                        myGame.enemySpace.Add(temp);
                        break;
                    case 27://Trap
                        temp = EnemyFactory.Instance.CreateTrap(itemDetail);
                        enemy.Add(temp);
                        myGame.enemySpace.Add(EnemyFactory.Instance.CreateTrap(itemDetail));
                        break;
                    case 28://WallMaster
                        temp = EnemyFactory.Instance.CreateWallMaster(itemDetail);
                        enemy.Add(temp);
                        myGame.enemySpace.Add(EnemyFactory.Instance.CreateWallMaster(itemDetail));
                        break;
                    case 29://Goriya Blue
                        temp = EnemyFactory.Instance.CreateGoriyaBlue(itemDetail);
                        enemy.Add(temp);
                        temp.SetMovePattern(random());
                        myGame.enemySpace.Add(temp);
                        break;
                    case 30://Old Man
                        myGame.nPCSpace.Add(NPCFactory.Instance.CreateOldMan(itemDetail));
                        break;
                    case 31://Death Cloud
                        temp = EnemyFactory.Instance.CreateDeathCloud(itemDetail);
                        enemy.Add(temp);
                        myGame.enemySpace.Add(temp);
                        break;
                    case 32://Goriya Red
                        temp = EnemyFactory.Instance.CreateGoriyaRed(itemDetail);
                        enemy.Add(temp);
                        temp.SetMovePattern(random());
                        myGame.enemySpace.Add(temp);
                        break;
                    default:
                        Console.WriteLine("Invalid item ID");
                        break;
                }
            }
        }

        int random()
        {
            return (new Random().Next() % 4) + 1;
        }
        public void LoadBoundsPerRoom()
        {//Maximum number of doors in a single room is 10
         // Rectangle[] tempDoors = new Rectangle[10];
            currentRoomDoors.Clear();
            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = false
            };
            using var streamReaderDoors = File.OpenText("Content/maps/Level" + levelI + "ZeldaDoors.csv");
            using var csvReaderDoors = new CsvReader(streamReaderDoors, csvConfig);
            string value;
            int[] item;
            int spotItem;
            int count = 0;
            if (csvReaderDoors.Read())
            {
                while (csvReaderDoors.Read())
                {
                    item = new int[7];
                    spotItem = 0;
                    for (int i = 0; csvReaderDoors.TryGetField<string>(i, out value); i++)
                    {
                        try
                        {
                            item[spotItem] = Int32.Parse(value);
                            spotItem = spotItem + 1;
                        }
                        catch
                        {
                            Console.WriteLine("Cannot parse integer from file");
                        }
                    }
                    if (spotItem == item.Length && item[0] == roomNum)
                    {
                        // tempDoors[count] = new Rectangle(screenSize.X+item[1],screenSize.Y+item[2],item[3],item[4]);
                        currentRoomDoors.Add(new int[]{item[0],
                                                            screenSize.X+item[1],
                                                            screenSize.Y+item[2],
                                                            item[3],
                                                            item[4],
                                                            item[5],
                                                            item[6]});
                        count = count + 1;
                    }
                }
            }
            // unlockedCurrentRoomDoors = new Rectangle[count+1];
            // for(int i = 0; i <unlockedCurrentRoomDoors.Length; i++){
            //         unlockedCurrentRoomDoors[i] = tempDoors[i];
            //     }


            streamReaderDoors.Close();
        }
        public void LoadItemsPerRoom()
        {

            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = false
            };

            using var streamReaderItems = File.OpenText("Content/maps/Level" + levelI + "ZeldaItems.csv");
            using var csvReaderItems = new CsvReader(streamReaderItems, csvConfig);

            string value;
            int[] item = new int[6];
            int spotItem;
            myGame.blockSpace.Clear();
            myGame.outItemSpace.Clear();
            myGame.enemySpace.Clear();
            myGame.nPCSpace.Clear();

            if (csvReaderItems.Read())
            {
                while (csvReaderItems.Read())
                {
                    spotItem = 0;

                    for (int i = 0; csvReaderItems.TryGetField<string>(i, out value); i++)
                    {
                        try
                        {
                            item[spotItem] = Int32.Parse(value);
                            spotItem = spotItem + 1;
                        }
                        catch
                        {
                            Console.WriteLine("Cannot parse integer from file");
                        }
                    }
                    if (spotItem == item.Length && item[0] == roomNum)
                    {
                        DisplayItem(item);
                    }
                }
            }

            streamReaderItems.Close();


        }


        public void ChangeRoom()
        {

            int oldX = roomX;
            int oldY = roomY;

            Rectangle tempPosition = myGame.character.GetPosition();
            if (myGame.character.GetPosition().Right >
             bounds[3].Left + myGame.character.GetPosition().Width)
            {//character moving right
                roomX = roomX + 256;
                tempPosition.X = bounds[1].Right + 1;//right bound of left side
            }
            else if (myGame.character.GetPosition().Left < bounds[1].Right - myGame.character.GetPosition().Width)
            {//character moving left
                roomX = roomX - 256;
                tempPosition.X = bounds[3].Left - tempPosition.Width - 1;//left bound of right side
            }
            else if (myGame.character.GetPosition().Bottom > bounds[2].Top + myGame.character.GetPosition().Height)
            {//character moving down
                roomY = roomY + 176;
                tempPosition.Y = bounds[0].Bottom - 1;
            }
            else if (myGame.character.GetPosition().Top < bounds[0].Bottom - myGame.character.GetPosition().Height)
            {//character moving up
                roomY = roomY - 176;
                tempPosition.Y = bounds[2].Top - tempPosition.Height - 1;
            }
            if (oldX != roomX || oldY != roomY || overrided)
            {
                for (int i = 0; i < rooms.Length; i++)
                {
                    if (rooms[i].X == roomX && rooms[i].Y == roomY && changed == false)
                    {
                        currentRoom = rooms[i];
                        changed = true;
                        roomNum = i;
                        LoadContent();
                        break;
                    }
                    else
                    {
                        changed = false;
                    }
                }
            }

            if (!changed && (oldX != roomX || oldY != roomY))
            {
                roomX = oldX;
                roomY = oldY;

            }
            else
            {
                myGame.character.ChangePosition(tempPosition);

            }


        }
        public void NextRoom()
        {
            roomNum = (roomNum + 1) % (rooms.Length);
            currentRoom = rooms[roomNum];
            roomX = currentRoom.X;
            roomY = currentRoom.Y;
            changed = true;
            LoadContent();

        }
        public void PreviousRoom()
        {
            roomNum = (roomNum - 1);
            if (roomNum < 0)
            {
                roomNum = rooms.Length - 1;
            }
            currentRoom = rooms[roomNum];
            roomX = currentRoom.X;
            roomY = currentRoom.Y;
            changed = true;
            LoadContent();
        }

        void drawDoors()
        {
            for (int i = 0; i < unlockedCurrentRoomDoors.Length; i++)
            {
                drawScreen.Draw(tempFill, unlockedCurrentRoomDoors[i], Color.Green);
            }
        }
        void drawBounds()
        {
            for (int i = 0; i < bounds.Length; i++)
            {
                drawScreen.Draw(tempFill, bounds[i], Color.Red);
            }
        }

        public void removeEnemy(IEnemy toRemove)
        {//TODO: LAST ENEMY IS NOT REMOVED CORRECTLY
            int index = enemy.FindIndex(delegate (IEnemy spot) { return spot.GetPosition() == toRemove.GetPosition(); });
            if (index < ePos.Count && index > -1)
            {
                int[] enemyy = ePos[index];
                objects.Remove(objects.Find(delegate (int[] spot) { return spot == enemyy; }));
                ePos.Remove(enemyy);
            }
        }

        public void removeItem(IItem toRemove)
        {
            //TODO: some bugs happen with pickaxe

            int index = iitem.FindIndex(delegate (IItem spot) { return spot.GetPosition() == toRemove.GetPosition(); });
            int[] iitemm = iPos[index];
            objects.Remove(objects.Find(delegate (int[] spot) { return spot == iitemm; }));
            iPos.Remove(iitemm);
            iitem.Remove(iitem[index]);

        }
        public void drawObjects()
        {
            List<int[]> roomObjects = objects.FindAll(delegate (int[] i) { return i[0] == roomNum; });
            foreach (int[] obj in roomObjects)
            {
                DisplayItem(obj);
            }
        }
        /*
        After a certain event, PAIR door ID is searched for and found.
        */
        public void enableDoor(Rectangle locked)
        {
            int doorID = 0;
            LoadBoundsPerRoom();
            //within each room finds appropriate door and doorID
            foreach (int[] door in currentRoomDoors)
            {
                if (door[1] == locked.X && door[2] == locked.Y)
                {
                    doorID = door[6];
                }
            }
            //position 6 stores doorID for pair
            foreach (int[] door in doors)
            {
                if (door[6] == doorID)
                {
                    int index = doors.FindIndex(delegate (int[] i) { return i == door; });
                    //position 5, 0 == unlocked, 1 == locked, 2 == not bombed.
                    doors[index][5] = 0;
                }
            }
            setDoors();
        }
        public void keyEnableDoor(Rectangle locked, ItemSpace itemspace)
        {
            List<IItem> itemList = itemspace.ItemList();
            for (int i = 0; i < itemList.Count; i++)
            {
                IItem item = itemList[i];
                if (item.ReturnSpecialType() == SpecialType.Key)
                {
                    itemList[i].SetNumber(item.Number() - 1);
                    //open the door
                    enableDoor(locked);
                    break;
                }
            }


        }
        public void setDoors()
        {
            List<int[]> roomDoors = doors.FindAll(delegate (int[] i) { return i[0] == roomNum; });
            //List<int[]> roomDoors = currentRoomDoors;
            int countU = 0;
            int countL = 0;

            unlockedCurrentRoomDoors = new Rectangle[roomDoors.Count];
            lockedCurrentRoomDoors = new Rectangle[roomDoors.Count];

            //position 5, 0 == unlocked, 1 == locked, 2 == not bombed.
            for (int i = 0; i < unlockedCurrentRoomDoors.Length; i++)
            {
                int checklock = roomDoors[i][5];
                //if (myGame._testMode || unlocked == checklock)
                if (unlocked == checklock)
                {
                    unlockedCurrentRoomDoors[countU] = new Rectangle(
                                    screenSize.X + roomDoors[i][1],
                                    screenSize.Y + roomDoors[i][2],
                                    roomDoors[i][3],
                                    roomDoors[i][4]);
                    countU = countU + 1;
                }
                if (myGame._testMode || 1 == checklock)
                {
                    lockedCurrentRoomDoors[countL] = new Rectangle(
                                    screenSize.X + roomDoors[i][1],
                                    screenSize.Y + roomDoors[i][2],
                                    roomDoors[i][3],
                                    roomDoors[i][4]);
                    countL = countL + 1;
                }

                //**currently commented out as Keyhole functionality is not available.
            }
        }

        public void LoadContent()
        {
            myGame.blockSpace.Clear();
            myGame.outItemSpace.Clear();
            myGame.enemySpace.Clear();
            myGame.nPCSpace.Clear();
            //LoadBoundsPerRoom();
            //LoadItemsPerRoom();
            setDoors();
            drawObjects();
            //enableDoor(new Rectangle(700,220,100,40));
        }
        public void Update()
        {
            ChangeRoom();
        }

        public void Draw()
        {
            drawScreen.Begin();
            drawScreen.Draw(allMap, screenSize, currentRoom, Color.White);
            //COMMENT/UNCOMMENT TO TOGGLE DOOR AND BOUND DRAWING
            if (myGame._testMode)
            {
                drawBounds();
                drawDoors();
            }
            drawScreen.End();

        }
    }

}