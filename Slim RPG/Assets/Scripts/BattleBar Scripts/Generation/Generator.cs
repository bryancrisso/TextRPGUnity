using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

namespace BattleBar
{

    public enum roomTypes
    {
        none,
        empty,
        upstairs,
        downstairs,
        fight,
        trader,
        loot
    }

    public class Room
    {
        public roomTypes content = roomTypes.none;
        public bool[] connections = new bool[] { false, false, false, false };
        public Enemy enemy;
        //traderType
        public bool explored = false;
        public List<Item> loot = new List<Item>();
    }

    public static class Generator
    {
        public static Room[,] Generate(int xSize, int ySize, int roomCount, List<Enemy> enemyList, List<Item> itemList, int floor)
        {
            int i;
            int j;
            System.Random gen = new System.Random();
            int currentRoomCount = 0;
            Room[,] array = new Room[ySize, xSize];

            for (int fDim = 0; fDim < ySize; fDim++)
                for (int sDim = 0; sDim < xSize; sDim++)
                    array[fDim, sDim] = new Room();

            if (roomCount > xSize * ySize)
            {
                Debug.Log("Cannot fit the number of roooms within the floor!");
                return array;
            }
            while (currentRoomCount < roomCount)
            {
                for (i = 0; i < ySize; i++)
                {
                    for (j = 0; j < xSize; j++)
                    {
                        if (!(currentRoomCount >= roomCount))
                        {
                            List<char> adjacentTesting = new List<char>() { 'n', 'e', 's', 'w' };
                            bool found = false;

                            if (array[i, j].content == roomTypes.none)
                            {
                                if (i == 0 && j == 0)
                                {
                                    array[i, j].content = roomTypes.empty;
                                    currentRoomCount++;
                                }
                                else
                                {
                                    if (i != 0 && j != 0 && i != (ySize - 1) && j != (xSize - 1))
                                    {
                                        if (array[i, j - 1].content != roomTypes.none || array[i - 1, j].content != roomTypes.none || array[i, j + 1].content != roomTypes.none || array[i + 1, j].content != roomTypes.none)
                                        {
                                            if (gen.Next(100) < 50)
                                            {
                                                array[i, j].content = roomTypes.empty;
                                                currentRoomCount++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (i == 0)
                                        {
                                            adjacentTesting.Remove('n');
                                        }
                                        if (j == 0)
                                        {
                                            adjacentTesting.Remove('w');
                                        }
                                        if (i == (ySize - 1))
                                        {
                                            adjacentTesting.Remove('s');
                                        }
                                        if (j == (xSize - 1))
                                        {
                                            adjacentTesting.Remove('e');
                                        }

                                        if (adjacentTesting.Contains('n'))
                                        {
                                            if (array[i - 1, j].content != roomTypes.none && (gen.Next(100) < 50) && !found)
                                            {
                                                array[i, j].content = roomTypes.empty;
                                                found = true;
                                                currentRoomCount++;
                                            }
                                        }

                                        if (adjacentTesting.Contains('e'))
                                        {
                                            if (array[i, j + 1].content != roomTypes.none && (gen.Next(100) < 50) && !found)
                                            {
                                                array[i, j].content = roomTypes.empty;
                                                found = true;
                                                currentRoomCount++;
                                            }
                                        }

                                        if (adjacentTesting.Contains('s'))
                                        {
                                            if (array[i + 1, j].content != roomTypes.none && (gen.Next(100) < 50) && !found)
                                            {
                                                array[i, j].content = roomTypes.empty;
                                                found = true;
                                                currentRoomCount++;
                                            }
                                        }

                                        if (adjacentTesting.Contains('w'))
                                        {
                                            if (array[i, j - 1].content != roomTypes.none && (gen.Next(100) < 50) && !found)
                                            {
                                                array[i, j].content = roomTypes.empty;
                                                found = true;
                                                currentRoomCount++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            array = CreateConnections(array);
            array = PopulateFloor(array, floor, enemyList, itemList);
            return array;
        }

        public static Room[,] CreateConnections(Room[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j].content == roomTypes.empty)
                    {
                        List<char> adjacentTesting = new List<char>() { 'n', 'e', 's', 'w' };

                        if (i == 0)
                        {
                            adjacentTesting.Remove('n');
                        }
                        if (j == 0)
                        {
                            adjacentTesting.Remove('w');
                        }
                        if (i == (array.GetLength(0) - 1))
                        {
                            adjacentTesting.Remove('s');
                        }
                        if (j == (array.GetLength(1) - 1))
                        {
                            adjacentTesting.Remove('e');
                        }

                        if (adjacentTesting.Contains('n'))
                        {
                            if (array[i - 1, j].content != roomTypes.none)
                            {
                                array[i, j].connections[0] = true;
                            }
                        }

                        if (adjacentTesting.Contains('e'))
                        {
                            if (array[i, j + 1].content != roomTypes.none)
                            {
                                array[i, j].connections[1] = true;
                            }
                        }

                        if (adjacentTesting.Contains('s'))
                        {
                            if (array[i + 1, j].content != roomTypes.none)
                            {
                                array[i, j].connections[2] = true;
                            }
                        }

                        if (adjacentTesting.Contains('w'))
                        {
                            if (array[i, j - 1].content != roomTypes.none)
                            {
                                array[i, j].connections[3] = true;
                            }
                        }
                    }
                }
            }
            return array;
        }

        public static Room[,] PopulateFloor(Room[,] array, int floor, List<Enemy> enemyList, List<Item> itemList)
        {
            System.Random gen = new System.Random();

            Dictionary<roomTypes, int> types = new Dictionary<roomTypes, int>()
        {
            {roomTypes.upstairs, 1},
            {roomTypes.downstairs,1},
            {roomTypes.fight,6},
            {roomTypes.loot,3},
            {roomTypes.trader,1}
        };

            int difficulty = floor / 10 + 1;

            foreach (roomTypes key in types.Keys.ToArray())
            {
                if (key != roomTypes.upstairs && key != roomTypes.downstairs && difficulty != 1)
                {
                    types[key] *= difficulty;
                }
            }
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (i == 0 && j == 0)
                    {
                        array[i, j].content = roomTypes.upstairs;
                        types.Remove(roomTypes.upstairs);
                    }
                    else
                    {
                        if (array[i, j].content != roomTypes.none)
                        {
                            int randomInt = gen.Next(types.Keys.ToArray().GetLength(0));
                            roomTypes content = types.Keys.ToArray()[randomInt];
                            array[i, j].content = content;
                            types[content]--;

                            foreach (roomTypes type in types.Keys.ToArray())
                            {
                                if (types[type] == 0)
                                {
                                    types.Remove(type);
                                }
                            }
                            if (array[i, j].content == roomTypes.fight)
                            {
                                List<Enemy> _eligibleEnemies = new List<Enemy>();
                                foreach (Enemy _enemy in enemyList)
                                {
                                    if (floor + 1 >= _enemy.level[0] && floor + 1 <= _enemy.level[1])
                                    {
                                        _eligibleEnemies.Add(_enemy);
                                    }
                                }
                                if (_eligibleEnemies.Count == 0)
                                {
                                    _eligibleEnemies = enemyList;
                                }
                                array[i, j].enemy = _eligibleEnemies[gen.Next(_eligibleEnemies.Count)];
                            }
                            else if (array[i, j].content == roomTypes.loot)
                            {
                                List<Item> _eligibleFood = new List<Item>();
                                List<Item> _eligibleWeapon = new List<Item>();

                                int _foodQuality = gen.Next(1, 4); //need choice weights

                                foreach (Item _item in itemList)
                                {
                                    if (difficulty == _item.level && _foodQuality == _item.quality && _item.type == itemType.Food) //add food blacklist
                                    {
                                        _eligibleFood.Add(_item);
                                    }
                                }
                                if (_eligibleFood.Count == 0)
                                {
                                    foreach (Item _item in itemList)
                                    {
                                        if (_item.type == itemType.Food)
                                        {
                                            _eligibleFood.Add(_item);
                                        }
                                    }
                                }

                                if (gen.Next(1, 3) == 1)
                                {
                                    int _weaponQuality = gen.Next(1, 4); //need choice weights

                                    foreach (Item _item in itemList)
                                    {
                                        if (difficulty == _item.level && _foodQuality == _item.quality && _item.type == itemType.Weapon) //add weapon blacklist
                                        {
                                            _eligibleWeapon.Add(_item);
                                        }
                                    }
                                    if (_eligibleWeapon.Count == 0)
                                    {
                                        foreach (Item _item in itemList)
                                        {
                                            if (_item.type == itemType.Weapon)
                                            {
                                                _eligibleWeapon.Add(_item);
                                            }
                                        }
                                    }
                                }

                                if (_eligibleWeapon.Count != 0)
                                {
                                    array[i, j].loot.Add(_eligibleFood[gen.Next(_eligibleFood.Count)]);
                                    array[i, j].loot.Add(_eligibleWeapon[gen.Next(_eligibleWeapon.Count)]);
                                }
                                else
                                {
                                    array[i, j].loot.Add(_eligibleFood[gen.Next(_eligibleFood.Count)]);
                                }
                            }
                        }
                    }
                }
            }
            return array;
        }

        public static void writeFloorToFile(Room[,] floor)
        {
            string fileName = "C:\\Users\\bryan\\floor.txt";
            string row = "";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            StreamWriter sr = File.CreateText(fileName);
            for (int i = 0; i < floor.GetLength(0); i++)
            {
                for (int j = 0; j < floor.GetLength(1); j++)
                {
                    row += floor[i, j].content.ToString() + ", ";
                }
                sr.WriteLine(row);
                row = "";
            }
            sr.Close();
        }

        public static int[] FindFloorSize(int a, int d, int e) //find the factors, b and c, of a, in the ratio d and e
        {
            int b = (int)Mathf.Round(d * Mathf.Sqrt(a / (d * e)));
            int c = (int)Mathf.Round(e * Mathf.Sqrt(a / (d * e)));
            int[] _array = { b, c };
            return _array;
        }
    }

}