using RLNETConsoleGame.Core;
using RogueSharp;
using RLNET;
using RogueSharp.DiceNotation;
using RLNETConsoleGame.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLNETConsoleGame.Systems
{
    internal class MapGenerator
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _maxRooms;
        private readonly int _roomMaxSize;
        private readonly int _roomMinSize;
        private readonly int _mapLevel;
        private readonly DungeonMap _map;
        private readonly EquipmentGenerator _equipmentGenerator;

        // constructing a new MapGenerator requires the demensions of the maps it will create
        public MapGenerator(int width, int height, int maxRooms, int roomMaxSize, int roomMinSize, int mapLevel)
        {
            _width = width;
            _height = height;
            _maxRooms = maxRooms;
            _roomMaxSize = roomMaxSize;
            _roomMinSize = roomMinSize;
            _mapLevel = mapLevel;
            _map = new DungeonMap();
            _equipmentGenerator = new EquipmentGenerator(mapLevel);
        }
        public DungeonMap CreateMap()
        {
            //inistailize the cells in the map by setting walkable, transparency and explored to true
            _map.Initialize(_width, _height);

            for (int r = _maxRooms; r > 0; r--)
            {
                //this determines the size and position of the rooms randomly 
                int roomWidth = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomHeight = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomXPosition = Game.Random.Next(0, _width - roomWidth - 1);
                int roomYPosition = Game.Random.Next(0, _height - roomHeight - 1);

                //this will make all the rooms rectangles with in the above size and Position parameters 
                var newRoom = new Rectangle(roomXPosition, roomYPosition, roomWidth, roomHeight);

                //this uses a bool to check if any rooms intersect (bool because it can only  return true or false)
                bool newRoomIntersects = _map.Rooms.Any(room => newRoom.Intersects(room));

                if (!newRoomIntersects)
                {
                    _map.Rooms.Add(newRoom);   // as long as it doesnt intersect it will be added to the list(the list in DungeonMap of type rectangle) of rooms
                }
            }

            for (int r = 1; r < _map.Rooms.Count; r++)
            {   
                if (r == 0)
                {
                    continue;
                }
                //cycle thru each room that was created starting from the second room
                int previousRoomCenterX = _map.Rooms[r - 1].Center.X;
                int previousRoomCenterY = _map.Rooms[r - 1].Center.Y;
                int currentRoomCenterX = _map.Rooms[r].Center.X;
                int currentRoomCenterY = _map.Rooms[r].Center.Y;

                //this creates a 50% chance for L shapped hallway to tunnel out 
                if (Game.Random.Next(1, 2) == 1)
                {
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                }
                else
                {
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                }
            }

            foreach (Rectangle room in _map.Rooms)
            {
                CreateRoom(room);
                CreateDoors(room);
            }
            CreateStairs();
            PlacePlayer();
            PlaceMonsters();
            PlaceEquipment();
            PlaceItems();
            PlaceAbility();
            return _map;
        }

        

        private void CreateRoom(Rectangle room)
        {
            for (int x = room.Left + 1; x < room.Right; x++)
            {
                for (int y = room.Top + 1; y < room.Bottom; y++)
                {
                    _map.SetCellProperties(x , y , true, true, false);   // thisi set the properties of the cell to be true in any given rectangular area on the map
                }                                               //set to false so that the rooms dont show before you can explore them 
            }

        }

        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                _map.SetCellProperties(x, yPosition, true, true);
            }
        }
                                                                            //code above and below place tunnels horizonally and vertiacally 
        private void CreateVerticalTunnel(int yStart, int yEnd,int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                _map.SetCellProperties(xPosition, y, true, true);
            }
        }

        private void CreateDoors(Rectangle room)
        {
            //these are the rooms boundries
            int xMin = room.Left;
            int xMax = room.Right;
            int yMin = room.Top;
            int yMax = room.Bottom;

            //putting the Room border cells into a list 
            // the directly below line of code is an explicit convertion from RogueSharp.ICell to RogueSharp.Cell 
            List<RogueSharp.Cell> borderCells = _map.GetCellsAlongLine(xMin, yMin, xMax, yMin).Cast<RogueSharp.Cell>().ToList();
                                                                                           //Cast<RogueSharp.Cell>() makes sure each .ICell is treated like a .Cell
            borderCells.AddRange(_map.GetCellsAlongLine(xMin, yMin, xMin, yMax).Cast<RogueSharp.Cell>());
            borderCells.AddRange(_map.GetCellsAlongLine(xMin, yMax, xMax, yMax).Cast<RogueSharp.Cell>());
            borderCells.AddRange(_map.GetCellsAlongLine(xMax, yMin, xMax, yMax).Cast<RogueSharp.Cell>());

            foreach (Cell cell in borderCells)
            {
                if (IsPotentialDoor(cell))
                {
                    _map.SetCellProperties(cell.X, cell.Y, false, true);
                    _map.Doors.Add(new Door
                    {
                        X = cell.X,
                        Y = cell.Y,
                        IsOpen = false
                    });
                }
            }

        }

       
        //this will check to see if the cell will be a good candidate for a dooor
        private bool IsPotentialDoor(Cell cell)
        {
            if (!cell.IsWalkable)       //if cell is not walkable it will not be a good candidate for a door
            {
                return false;
            }

            //this will store all the references to all the neighboring cells
            Cell right = (Cell)_map.GetCell(cell.X + 1, cell.Y);
            Cell left = (Cell)_map.GetCell(cell.X - 1, cell.Y);
            Cell top = (Cell)_map.GetCell(cell.X, cell.Y - 1);
            Cell bottom = (Cell)_map.GetCell(cell.X, cell.Y + 1);

            //to make sure that the door isnt created on top of another door
            if(_map.GetDoor(cell.X,cell.Y) != null || _map.GetDoor(right.X, right.Y) != null || _map.GetDoor(left.X, left.Y) != null || _map.GetDoor(top.X, top.Y) != null || _map.GetDoor(bottom.X, bottom.Y) != null)
            {
                return false;
            }

            if (right.IsWalkable && left.IsWalkable && !top.IsWalkable && !bottom.IsWalkable)
            {
                return true;
            }
            if (!right.IsWalkable && !left.IsWalkable && top.IsWalkable && bottom.IsWalkable)
            {
                return true;
            }
            return false;
        }

        private void PlacePlayer()
        {
            Player player = Game.Player ?? new Player();        //chancged from an if statment
            player.X = _map.Rooms[0].Center.X;              //this adds player to the center of the first room in the list of Rooms of type rectangle
            player.Y = _map.Rooms[0].Center.Y;                   //X and Y coordineates

            _map.AddPlayer(player);
        }

        private void CreateStairs()     // this will create the stairs in the ceenter of each room theyre in 
        {
            _map.StairsUp = new Stairs()
            {
                X = _map.Rooms[0].Center.X + 1,
                Y = _map.Rooms[0].Center.Y,
                IsUp = true
            };
            _map.StairsDown = new Stairs
            {
                X = _map.Rooms[0].Center.X + 1,
                Y = _map.Rooms[0].Center.Y,
                IsUp = false
            };
        }

        private void PlaceMonsters()
        {
            foreach (var room in _map.Rooms)
            {
                if (Dice.Roll("1D10") < 7)      //roll 1 die with 10 sides but must less than 7 which gives it a 60% chance of monsters being in the room 
                {
                    //this will create between 1 and 4 monsters per room 
                    var numberOfMonsters = Dice.Roll("1D4");
                    for (int i = 0; i < numberOfMonsters; i++)
                    {
                        //now we need to fine a random walkable location to place each monster in each room
                        Point randomRoomLocation = (Point)_map.GetRandomWalkableLocationInRoom(room);


                        //in the rare occorance that a room doesnt have space to place a monster the bottom code will prevent a error
                        if (randomRoomLocation != null)
                        {
                            _map.AddMonsters(ActorGenerator.CreateMonster(_mapLevel, _map.GetRandomLocationInRoom(room)));
                            //var monster = Roadman.Create(1);
                            //monster.X = randomRoomLocation.X;
                            //monster.Y = randomRoomLocation.Y;
                            //_map.AddMonsters(monster);
                        }
                    }
                }
            }

        }

        private void PlaceEquipment()
        {
            foreach (var room in _map.Rooms)
            {
                if (Dice.Roll("1D10") < 3)
                {
                    if (_map.DoesRoomHaveWalkableSpace(room))
                    {
                        Point randomRoomLocation = (Point)_map.GetRandomWalkableLocationInRoom(room);
                        if (randomRoomLocation != null)
                        {
                            Core.Equipment equipment;
                            try
                            {
                                equipment = _equipmentGenerator.CreateEquipment();
                            }
                            catch (InvalidOperationException)
                            {
                                //this is when theres no more equipment to add so it stops addin gto the map level
                                return;
                            }
                            Point location = (Point)_map.GetRandomWalkableLocationInRoom(room);
                            _map.AddTreasure(location.X, location.Y, equipment);
                        }
                    }
                }
            }
        }

        private void PlaceItems()
        {
            foreach (var room in _map.Rooms)
            {
                if (Dice.Roll("1D10") < 3) //1 10 sided Die is used and if it rolls less than 3 the following happens 
                {
                    if (_map.DoesRoomHaveWalkableSpace(room))   //check if rooms have walkable space 
                    {
                        //this points to a random location in the room
                        Point randomRoomLocation = (Point)_map.GetRandomWalkableLocationInRoom(room);  //(point) is making use of an explicit cast
                        if (randomRoomLocation != null)
                        {
                            //this creates a new item in theh random location of the room and adds it to the specific location of the map 
                            Item item = ItemGenerator.CreateItem();
                            Point location = (Point)_map.GetRandomWalkableLocationInRoom(room);
                            _map.AddTreasure(location.X, location.Y, item);
                        }
                    }
                }
            }
        }

        private void PlaceAbility()
        {
            if (_mapLevel == 1 || _mapLevel % 3 == 0)       // check if the level is 1 or a multiple of 3 (modolus)
            {
                try
                {   //this creats an abilty , gets random room index , poiints to a random location in room then places the ability 
                    var ability = AbilityGenerator.CreateAbility();
                    int roomIndex = Game.Random.Next(0, _map.Rooms.Count - 1);
                    Point location = (Point)_map.GetRandomWalkableLocationInRoom(_map.Rooms[roomIndex]);
                    _map.AddTreasure(location.X, location.Y, ability);
                }
                catch (InvalidOperationException)
                {
                    // this being left empty will catch and ignore the invalid op exception
                }
            }
        }
    }

}
