using System;
using System.Collections.Generic;

public enum DUNGEON_LEVEL_DIRECTION {
    UNKNOWN = 0,
    NORTH = 1,
    SOUTH = 2,
    EAST = 3,
    WEST = 4
};

public enum DUNGEON_LEVEL_STAIR_DIRECTION {
    UNKNOWN = 0,
    UP = 1,
    DOWN = 2
}

public enum DUNGEON_LEVEL_SIZE {
    MINUTE = 0,
    TINY = 1,
    SMALL = 2,
    PETITE = 3,
    MEDIUM = 4,
    LARGE = 5,
    HUGE = 6,
    MASSIVE = 7,
    GIGANTIC = 8,
    EPIC = 9,
    CUSTOM = 10
};

public enum DUNGEON_LEVEL_LAYOUT {
    UNKNOWN = 0,
    SQUARE = 1,
    BOX = 2,
    KEEP = 3,
    CROSS = 4,
    DAGGER_UP = 5,
    DAGGER_DOWN = 6,
    DAGGER_LEFT = 7,
    DAGGER_RIGHT = 8,
    ROUND = 9,
    SIZE = 10
};

public enum DUNGEON_LEVEL_CORRIDOR_LAYOUT {
    UNKNOWN = 0,
    LABYRINTH = 1,
    BENT = 2,
    STRAIGHT = 3,
    SIZE = 4
};

public enum DUNGEON_LEVEL_ROOM_LAYOUT {
    UNKNOWN = 0,
    PACKED = 1,
    SCATTERED = 2,
    SIZE = 3
};

public enum DUNGEON_LEVEL_REMOVE_DEADENDS {
    NONE = 0,
    SOME = 50,
    ALL = 100,
    SIZE = 3
};

public enum DUNGEON_LEVEL_END_TYPE {
    WALLED = 0,
    CORRIDOR = 1,
    STAIR = 2,
    NEXT = 3,
    OPEN = 4,
    CLOSE = 5,
    RECURSE = 6
};

public enum DUNGEON_LEVEL_DOOR_TYPE {
    OPEN = 0,
    CLOSED = 1,
    LOCKED = 2,
    TRAPPED = 3,
    SECRET = 4,
    ARCH = 5,
    PORTCULLIS = 6
};

public class RoomData {

    public int id;
    public int row;
    public int column;
    public int north;
    public int south;
    public int west;
    public int east;
    public int width;
    public int height;

    public int area;

    public Dictionary<DUNGEON_LEVEL_DIRECTION, DoorData> door;
    public bool doorWasSet;

    public RoomData(int id, int row, int column, int north, int south, int west, int east, int width, int height) {
        this.id = id;
        this.row = row;
        this.column = column;
        this.north = north;
        this.south = south;
        this.west = west;
        this.east = east;
        this.width = width;
        this.height = height;

        this.area = width * height;

        this.door = new Dictionary<DUNGEON_LEVEL_DIRECTION, DoorData>();
        this.doorWasSet = false;
    }
};

public class SillData {

    public int sillR;
    public int sillC;
    public DUNGEON_LEVEL_DIRECTION dir;
    public int doorR;
    public int doorC;

    public int outID;
    public bool outIDWasSet;

    public SillData(int sillR, int sillC, DUNGEON_LEVEL_DIRECTION dir, int doorR, int doorC) {
        this.sillR = sillR;
        this.sillC = sillC;
        this.dir = dir;
        this.doorR = doorR;
        this.doorC = doorC;

        this.outID = 0;
        this.outIDWasSet = false;
    }
};

public class DoorData {

    public int row;
    public int col;
    public DUNGEON_LEVEL_DOOR_TYPE type;
    public string description;

    public int outID;
    public bool outIDWasSet;

    public DoorData(int row, int col, DUNGEON_LEVEL_DOOR_TYPE type, string description) {
        this.row = row;
        this.col = col;
        this.type = type;
        this.description = description;

        this.outID = 0;
        this.outIDWasSet = false;
    }
};

public class StairData {

    public int row;
    public int col;
    public DUNGEON_LEVEL_STAIR_DIRECTION dir;
    public int nextRow;
    public int nextCol;

    public StairData(int row, int col) {
        this.row = row;
        this.col = col;
        this.dir = DUNGEON_LEVEL_STAIR_DIRECTION.UNKNOWN;
        this.nextRow = 0;
        this.nextCol = 0;
    }
};

public class DungeonLevel {

    protected static Dictionary<DUNGEON_LEVEL_LAYOUT, int[][]> levelLayouts = new Dictionary<DUNGEON_LEVEL_LAYOUT, int[][]>() {
        { DUNGEON_LEVEL_LAYOUT.UNKNOWN,               new int[][] { new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 } }},
        { DUNGEON_LEVEL_LAYOUT.SQUARE,                new int[][] { new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 }, new int[] { 1, 1, 1 } }},
        { DUNGEON_LEVEL_LAYOUT.BOX,                   new int[][] { new int[] { 1, 1, 1 }, new int[] { 1, 0, 1 }, new int[] { 1, 1, 1 } }},
        { DUNGEON_LEVEL_LAYOUT.KEEP,                  new int[][] { new int[] { 1, 1, 1 }, new int[] { 0, 1, 0 }, new int[] { 1, 1, 1 } }},
        { DUNGEON_LEVEL_LAYOUT.CROSS,                 new int[][] { new int[] { 0, 1, 0 }, new int[] { 1, 1, 1 }, new int[] { 0, 1, 0 } }},
        { DUNGEON_LEVEL_LAYOUT.DAGGER_UP,             new int[][] { new int[] { 0, 1, 0, 0 }, new int[] { 1, 1, 1, 1 }, new int[] { 0, 1, 0, 0 } }},
        { DUNGEON_LEVEL_LAYOUT.DAGGER_DOWN,           new int[][] { new int[] { 0, 0, 1, 0 }, new int[] { 1, 1, 1, 1 }, new int[] { 0, 0, 1, 0 } }},
        { DUNGEON_LEVEL_LAYOUT.DAGGER_LEFT,           new int[][] { new int[] { 0, 1, 0 }, new int[] { 1, 1, 1 }, new int[] { 0, 1, 0 }, new int[] { 0, 1, 0 } }},
        { DUNGEON_LEVEL_LAYOUT.DAGGER_RIGHT,          new int[][] { new int[] { 0, 1, 0 }, new int[] { 0, 1, 0 }, new int[] { 1, 1, 1 }, new int[] { 0, 1, 0 } }}
    };

    protected static Dictionary<DUNGEON_LEVEL_CORRIDOR_LAYOUT, int> corridorLayouts = new Dictionary<DUNGEON_LEVEL_CORRIDOR_LAYOUT, int>() {
        { DUNGEON_LEVEL_CORRIDOR_LAYOUT.UNKNOWN,      100 },
        { DUNGEON_LEVEL_CORRIDOR_LAYOUT.LABYRINTH,    0 },
        { DUNGEON_LEVEL_CORRIDOR_LAYOUT.BENT,         50 },
        { DUNGEON_LEVEL_CORRIDOR_LAYOUT.STRAIGHT,     100 }
    };

    protected static Dictionary<DUNGEON_LEVEL_DIRECTION, int> di = new Dictionary<DUNGEON_LEVEL_DIRECTION, int>() {
        { DUNGEON_LEVEL_DIRECTION.NORTH,  -1 },
        { DUNGEON_LEVEL_DIRECTION.SOUTH,  1 },
        { DUNGEON_LEVEL_DIRECTION.EAST,   0 },
        { DUNGEON_LEVEL_DIRECTION.WEST,   0 }
    };
    protected static Dictionary<DUNGEON_LEVEL_DIRECTION, int> dj = new Dictionary<DUNGEON_LEVEL_DIRECTION, int>() {
        { DUNGEON_LEVEL_DIRECTION.NORTH,  0 },
        { DUNGEON_LEVEL_DIRECTION.SOUTH,  0 },
        { DUNGEON_LEVEL_DIRECTION.EAST,   1 },
        { DUNGEON_LEVEL_DIRECTION.WEST,   -1 }
    };

    protected static Dictionary<DUNGEON_LEVEL_DIRECTION, DUNGEON_LEVEL_DIRECTION> opposite = new Dictionary<DUNGEON_LEVEL_DIRECTION, DUNGEON_LEVEL_DIRECTION>() {
        { DUNGEON_LEVEL_DIRECTION.NORTH, DUNGEON_LEVEL_DIRECTION.SOUTH },
        { DUNGEON_LEVEL_DIRECTION.SOUTH, DUNGEON_LEVEL_DIRECTION.NORTH },
        { DUNGEON_LEVEL_DIRECTION.WEST, DUNGEON_LEVEL_DIRECTION.EAST },
        { DUNGEON_LEVEL_DIRECTION.EAST, DUNGEON_LEVEL_DIRECTION.WEST }
    };

    protected static Dictionary<DUNGEON_LEVEL_DIRECTION, Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>> stairEnd = new Dictionary<DUNGEON_LEVEL_DIRECTION, Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>>() {
        { DUNGEON_LEVEL_DIRECTION.NORTH, new Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>() {
            { DUNGEON_LEVEL_END_TYPE.WALLED,     new int[][] { new int[] {1, -1}, new int[] {0, -1}, new int[] {-1, -1}, new int[] {-1, 0}, new int[] {-1, 1}, new int[] {0, 1}, new int[] {1, 1} }},
            { DUNGEON_LEVEL_END_TYPE.CORRIDOR,   new int[][] { new int[] {0, 0}, new int[] {1, 0}, new int[] {2, 0} }},
            { DUNGEON_LEVEL_END_TYPE.STAIR,      new int[][] { new int[] {0, 0} }},
            { DUNGEON_LEVEL_END_TYPE.NEXT,       new int[][] { new int[] {1, 0} }}
        }},
        { DUNGEON_LEVEL_DIRECTION.SOUTH, new Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>() {
            { DUNGEON_LEVEL_END_TYPE.WALLED,     new int[][] { new int[] {-1, -1}, new int[] {0, -1}, new int[] {1, -1}, new int[] {1, 0}, new int[] {1, 1}, new int[] {0, 1}, new int[] {-1, 1} }},
            { DUNGEON_LEVEL_END_TYPE.CORRIDOR,   new int[][] { new int[] {0, 0}, new int[] {-1, 0}, new int[] {-2, 0} }},
            { DUNGEON_LEVEL_END_TYPE.STAIR,      new int[][] { new int[] {0, 0} }},
            { DUNGEON_LEVEL_END_TYPE.NEXT,       new int[][] { new int[] {-1, 0} }}
        }},
        { DUNGEON_LEVEL_DIRECTION.WEST, new Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>() {
            { DUNGEON_LEVEL_END_TYPE.WALLED,     new int[][] { new int[] {-1, 1}, new int[] {-1, 0}, new int[] {-1, -1}, new int[] {0, -1}, new int[] {1, -1}, new int[] {1, 0}, new int[] {1, 1} }},
            { DUNGEON_LEVEL_END_TYPE.CORRIDOR,   new int[][] { new int[] {0, 0}, new int[] {0, 1}, new int[] {0, 2} }},
            { DUNGEON_LEVEL_END_TYPE.STAIR,      new int[][] { new int[] {0, 0} }},
            { DUNGEON_LEVEL_END_TYPE.NEXT,       new int[][] { new int[] {0, 1} }}
        }},
        { DUNGEON_LEVEL_DIRECTION.EAST, new Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>() {
            { DUNGEON_LEVEL_END_TYPE.WALLED,     new int[][] { new int[] {-1, -1}, new int[] {-1, 0}, new int[] {-1, 1}, new int[] {0, 1}, new int[] {1, 1}, new int[] {1, 0}, new int[] {1, -1} }},
            { DUNGEON_LEVEL_END_TYPE.CORRIDOR,   new int[][] { new int[] {0, 0}, new int[] {0, -1}, new int[] {0, -2} }},
            { DUNGEON_LEVEL_END_TYPE.STAIR,      new int[][] { new int[] {0, 0} }},
            { DUNGEON_LEVEL_END_TYPE.NEXT,       new int[][] { new int[] {0, -1} }}
        }}
    };

    protected static Dictionary<DUNGEON_LEVEL_DIRECTION, Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>> closeEnd = new Dictionary<DUNGEON_LEVEL_DIRECTION, Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>>() {
        { DUNGEON_LEVEL_DIRECTION.NORTH, new Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>() {
            { DUNGEON_LEVEL_END_TYPE.WALLED,     new int[][] { new int[] {0, -1}, new int[] {1, -1}, new int[] {1, 0}, new int[] {1, 1}, new int[] {0, 1} }},
            { DUNGEON_LEVEL_END_TYPE.CLOSE,      new int[][] { new int[] {0, 0} }},
            { DUNGEON_LEVEL_END_TYPE.RECURSE,    new int[][] { new int[] {-1, 0} }}
        }},
        { DUNGEON_LEVEL_DIRECTION.SOUTH, new Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>() {
            { DUNGEON_LEVEL_END_TYPE.WALLED,     new int[][] { new int[] {0, -1}, new int[] {-1, -1}, new int[] {-1, 0}, new int[] {-1, 1}, new int[] {0, 1} }},
            { DUNGEON_LEVEL_END_TYPE.CLOSE,      new int[][] { new int[] {0, 0} }},
            { DUNGEON_LEVEL_END_TYPE.RECURSE,    new int[][] { new int[] {1, 0} }}
        }},
        { DUNGEON_LEVEL_DIRECTION.WEST, new Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>() {
            { DUNGEON_LEVEL_END_TYPE.WALLED,     new int[][] { new int[] {-1, 0}, new int[] {-1, 1}, new int[] {0, 1}, new int[] {1, 1}, new int[] {1, 0} }},
            { DUNGEON_LEVEL_END_TYPE.CLOSE,      new int[][] { new int[] {0, 0} }},
            { DUNGEON_LEVEL_END_TYPE.RECURSE,    new int[][] { new int[] {0, -1} }}
        }},
        { DUNGEON_LEVEL_DIRECTION.EAST, new Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>() {
            { DUNGEON_LEVEL_END_TYPE.WALLED,     new int[][] { new int[] {-1, 0}, new int[] {-1, -1}, new int[] {0, -1}, new int[] {1, -1}, new int[] {1, 0} }},
            { DUNGEON_LEVEL_END_TYPE.CLOSE,      new int[][] { new int[] {0, 0} }},
            { DUNGEON_LEVEL_END_TYPE.RECURSE,    new int[][] { new int[] {0, 1} }}
        }}
    };

    public static uint NOTHING = 0x00000000;

    public static uint BLOCKED = 0x00000001;
    public static uint ROOM = 0x00000002;
    public static uint CORRIDOR = 0x00000004;

    public static uint PERIMETER = 0x00000010;
    public static uint ENTRANCE = 0x00000020;
    public static uint ROOM_ID = 0x0000FFC0;

    public static uint ARCH =      0x00010000;
    public static uint OPEN =      0x00020000;
    public static uint CLOSED =    0x00040000;
    public static uint LOCKED =    0x00080000;
    public static uint SECRET =    0x00100000;
    public static uint PORTC =     0x00200000;
    public static uint STAIR_DN =  0x00400000;
    public static uint STAIR_UP =  0x00800000;

    public static uint LABEL = 0xFF000000;

    public static uint OPENSPACE = ROOM | CORRIDOR;
    public static uint DOORSPACE = ARCH | OPEN | CLOSED | LOCKED | SECRET | PORTC;
    public static uint ESPACE = ENTRANCE | DOORSPACE | 0xFF000000;
    public static uint STAIRS = STAIR_DN | STAIR_UP;

    public static uint BLOCK_ROOM = BLOCKED | ROOM;
    public static uint BLOCK_CORR = BLOCKED | PERIMETER | CORRIDOR;
    public static uint BLOCK_DOOR = BLOCKED | DOORSPACE;

    //TODO
    public string mapStyle;
    public int cellSize;

    private int nI;
    private int nJ;
    private int maxRow;
    private int maxCol;
    private int nRooms;
    private int roomBase;
    private int roomRadix;
    public uint[][] cell;
    private int lastRoomId;

    private Random random;

    public int seed;                                            //Ensures randomness.
    public DUNGEON_LEVEL_SIZE levelSize;                      //A pre-selected size for the dungeon. If custom is selected, the code will look for its size in the nRows and nCols variables.
    public int nRows;                                           //The amount of tiles in the x coordinate.
    public int nCols;                                           //The amount of tiles in the y coordinate.
    public DUNGEON_LEVEL_LAYOUT levelLayout;                  //gives a unique layout to the dungeon.
    public int roomMin;                                         //The minimum size of a room for both x and y coordinates.
    public int roomMax;                                         //The maximum size of a room for both x and y coordinates.
    public DUNGEON_LEVEL_ROOM_LAYOUT roomLayout;                //The way the rooms are layid out throughout the dungeon.
    public DUNGEON_LEVEL_CORRIDOR_LAYOUT corridorLayout;        //The way the corridors are laid out throughout the dungeon.
    public DUNGEON_LEVEL_REMOVE_DEADENDS removeDeadends;        //Option to remove deadends entirely, some of them, or none at all.                  
    public bool addStairs;                                      //If it has stairs, it means the dungeon has multiple levels.
    public int level;                                           //Level this dungeon is in. If it's the first level, the dungeon will have an entrance, which will be the spawn point of the player.
    public bool isFirstLevel;
    public bool isLastLevel;

    public SortedList<int, RoomData> rooms;
    public List<DoorData> doors;
    public List<StairData> stairs;
    public Dictionary<string, int> connect;

    public DungeonLevel(int seed, DUNGEON_LEVEL_SIZE levelSize, int nRows, int nCols, DUNGEON_LEVEL_LAYOUT levelLayout, int roomMin, int roomMax, DUNGEON_LEVEL_ROOM_LAYOUT roomLayout,
                        DUNGEON_LEVEL_CORRIDOR_LAYOUT corridorLayout, DUNGEON_LEVEL_REMOVE_DEADENDS removeDeadends, bool addStairs, int level, bool isFirstLevel, bool isLastLevel)
    {
        this.seed = seed;
        this.levelSize = levelSize;
        this.nRows = nRows;
        this.nCols = nCols;
        this.levelLayout = levelLayout;
        this.roomMin = roomMin;
        this.roomMax = roomMax;
        this.roomLayout = roomLayout;
        this.corridorLayout = corridorLayout;
        this.removeDeadends = removeDeadends;
        this.addStairs = addStairs;
        this.level = level;
        this.isFirstLevel = isFirstLevel;
        this.isLastLevel = isLastLevel;

        CreateDungeon();
    }

    private void CreateDungeon() {
        //The first 2 set of division gives us the middle point, and the second set ensures there are no odd number of rows.
        nI = nRows / 2;
        nJ = nCols / 2;
        nRows = nI * 2;
        nCols = nJ * 2;

        //NOTE: maxRow and maxCol denote the last row and column respectively by array index laws.
        maxRow = nRows - 1;
        maxCol = nCols - 1;
        nRooms = 0;

        int max = roomMax;
        int min = roomMin;
        roomBase = (min + 1) / 2;
        roomRadix = ((max - min) / 2) + 1;

        rooms = new SortedList<int, RoomData>();
        connect = new Dictionary<string, int>();
        stairs = new List<StairData>();
        doors = new List<DoorData>();

        InitCells();
        EmplaceRooms();
        OpenRooms();
        LabelRooms();
        Corridors();

        if(addStairs) {
            EmplaceStairs();
        }

        CleanDungeon();
    }

    private void InitCells() {
        cell = new uint[nRows + 1][];

        for(int r = 0; r <= nRows; ++r) {
            cell[r] = new uint[nCols + 1];

            for(int c = 0; c <= nCols; ++c) {
                cell[r][c] = NOTHING;
            }
        }

        random = new System.Random(seed);

        if(levelLayouts.ContainsKey(levelLayout)) {
            MaskCells(levelLayouts[levelLayout]);
        }
        else if(levelLayout == DUNGEON_LEVEL_LAYOUT.ROUND) {
            RoundMask();
        }
    }

    private void RoundMask() {
        int centerR = nRows / 2;
        int centerC = nCols / 2;

        for(int r = 0; r <= nRows; ++r) {
            for(int c = 0; c <= nCols; ++c) {
                double d = Math.Sqrt(Math.Pow(r - centerR, 2) + Math.Pow(c - centerC, 2));
                if(d > centerC)
                    cell[r][c] = BLOCKED;
            }
        }
    }

    private void MaskCells(int[][] mask) {
        float rX = mask.Length * 1.0f / (nRows + 1);
        float cX = mask[0].Length * 1.0f / (nCols + 1);

        for(int r = 0; r <= nRows; ++r) {
            for(int c = 0; c <= nCols; ++c) {
                if(mask[(int)(r * rX)][(int)(c * cX)] == 0) {
                    cell[r][c] = BLOCKED;
                }
            }
        }
    }

    private void EmplaceRooms() {
        if(roomLayout == DUNGEON_LEVEL_ROOM_LAYOUT.PACKED) {
            PackRooms();
        }
        else if(roomLayout == DUNGEON_LEVEL_ROOM_LAYOUT.SCATTERED) {
            ScatterRooms();
        }
    }

    private void PackRooms() {
        for(int i = 0; i < nI; ++i) {
            int r = (i * 2) + 1;

            for(int j = 0; j < nJ; ++j) {
                int c = (j * 2) + 1;

                if((cell[r][c] & ROOM) != NOTHING) {
                    continue;
                }
                if((i == 0 || j == 0) && (random.Next(2) == 1)) {
                    continue;
                }

                Dictionary<string, int> proto = new Dictionary<string, int>() {
                    { "i", i }, { "j", j }
                };

                EmplaceRoom(proto);
            }
        }
    }

    private void ScatterRooms() {
        int nR = AllocRooms();
        for(int i = 0; i < nR; ++i) {
            EmplaceRoom();
        }
    }

    private int AllocRooms() {
        int levelArea = nCols * nRows;
        int roomArea = roomMax * roomMax;
        return (levelArea / roomArea);
    }

    private void EmplaceRoom(Dictionary<string, int> proto = null) {
        if(nRooms == 999) {
            return;
        }
        if(proto == null) {
            proto = new Dictionary<string, int>();
        }

        int r;
        int c;

        //Room position and size.
        SetRoom(proto);

        //Room boundaries.
        int r1 = (proto["i"] * 2) + 1;
        int c1 = (proto["j"] * 2) + 1;
        int r2 = ((proto["i"] + proto["height"]) * 2) - 1;
        int c2 = ((proto["j"] + proto["width"]) * 2) - 1;

        if(r1 < 1 || r2 > maxRow) {
            return;
        }
        if(c1 < 1 || c2 > maxCol) {
            return;
        }

        //Check for collisions with existing rooms.
        Dictionary<string, int> hit = SoundRoom(r1, c1, r2, c2);
        if(hit.ContainsKey("blocked")) {
            return;
        }

        int nHits = hit.Count;
        int roomID;

        if(nHits == 0) {
            roomID = nRooms + 1;
            nRooms = roomID;
        }
        else {
            return;
        }

        lastRoomId = roomID;

        //Emplace room.
        for(r = r1; r <= r2; ++r) {
            for(c = c1; c <= c2; ++c) {
                if((cell[r][c] & ENTRANCE) != NOTHING) {
                    cell[r][c] &= ~ESPACE;
                }
                else if((cell[r][c] & PERIMETER) != NOTHING) {
                    cell[r][c] &= ~PERIMETER;
                }

                cell[r][c] |= ROOM | (uint)(roomID << 6);
            }
        }

        int height = ((r2 - r1) + 1);
        int width = ((c2 - c1) + 1);

        RoomData roomData = new RoomData(roomID, r1, c1, r1, r2, c1, c2, width, height);
        rooms.Add(roomID, roomData);

        //Bblock corridors from room boundary. Check for door openings from adjacent rooms.
        for(r = r1 - 1; r <= r2 + 1; ++r) {
            if((cell[r][c1 - 1] & (ROOM | ENTRANCE)) == NOTHING) {
                cell[r][c1 - 1] |= PERIMETER;
            }
            if((cell[r][c2 + 1] & (ROOM | ENTRANCE)) == NOTHING) {
                cell[r][c2 + 1] |= PERIMETER;
            }
        }
        for(c = c1 - 1; c <= c2 + 1; ++c) {
            if((cell[r1 - 1][c] & (ROOM | ENTRANCE)) == NOTHING) {
                cell[r1 - 1][c] |= PERIMETER;
            }
            if((cell[r2 + 1][c] & (ROOM | ENTRANCE)) == NOTHING) {
                cell[r2 + 1][c] |= PERIMETER;
            }
        }
    }

    private void SetRoom(Dictionary<string, int> proto) {
        int iBase = roomBase;
        int radix = roomRadix;

        if(!proto.ContainsKey("height")) {
            if(proto.ContainsKey("i")) {
                int a = nI - iBase - proto["i"];

                if(a < 0) {
                    a = 0;
                }

                int r = (a < radix) ? a : radix;
                proto.Add("height", random.Next(r) + iBase);
            }
            else {
                proto.Add("height", random.Next(radix) + iBase);
            }
        }

        if(!proto.ContainsKey("width")) {
            if(proto.ContainsKey("j")) {
                int a = nJ - iBase - proto["j"];

                if(a < 0) {
                    a = 0;
                }

                int r = (a < radix) ? a : radix;
                proto.Add("width", random.Next(r) + iBase);
            }
            else {
                proto.Add("width", random.Next(radix) + iBase);
            }
        }

        if(!proto.ContainsKey("i")) {
            proto.Add("i", random.Next(nI - proto["height"]));
        }
        if(!proto.ContainsKey("j")) {
            proto.Add("j", random.Next(nJ - proto["width"]));
        }
    }

    private Dictionary<string, int> SoundRoom(int r1, int c1, int r2, int c2) {
        Dictionary<string, int> hit = new Dictionary<string, int>();

        for(int r = r1; r <= r2; ++r) {
            for(int c = c1; c <= c2; ++c) {
                if((cell[r][c] & BLOCKED) != NOTHING) {
                    hit.Add("blocked", 1);
                    return (hit);
                }
                if((cell[r][c] & ROOM) != NOTHING) {
                    uint id = (cell[r][c] & ROOM_ID) >> 6;

                    if(hit.ContainsKey(id.ToString())) {
                        hit[id.ToString()]++;
                    }
                    else {
                        hit.Add(id.ToString(), 1);
                    }
                }
            }
        }

        return (hit);
    }

    private void OpenRooms() {
        for(int id = 1; id <= nRooms; ++id) {
            OpenRoom(rooms[id]);
        }

        connect.Clear();
    }

    private void OpenRoom(RoomData room) {
        List<SillData> list = DoorSills(room);

        if(list == null || list.Count == 0) {
            return;
        }

        int nOpens = AllocOpens(room);

        for(int i = 0; i < nOpens; ++i) {
            SillData sill = null;

            int doorR = 0;
            int doorC = 0;
            uint doorCell = 0;
            bool doContinue = false;

            do {
                doContinue = false;

                do {
                    sill = Splice(list, random.Next(list.Count));

                    if(sill == null) {
                        goto next;
                    }

                    doorR = sill.doorR;
                    doorC = sill.doorC;
                    doorCell = cell[doorR][doorC];
                }
                while((doorCell & DOORSPACE) != NOTHING);

                int outID = 0;

                if(sill.outIDWasSet) {
                    outID = sill.outID;
                    string strConnect = Math.Min(room.id, outID).ToString() + "," + Math.Max(room.id, outID).ToString();

                    if(connect.ContainsKey(strConnect)) {
                        connect[strConnect]++;
                        doContinue = true;
                    }
                    else {
                        connect.Add(strConnect, 1);
                    }
                }
            } while(doContinue);

            int openR = sill.sillR;
            int openC = sill.sillC;
            DUNGEON_LEVEL_DIRECTION openDir = sill.dir;

            //Open door.
            for(int x = 0; x < 3; ++x) {
                int r = openR + (di[openDir] * x);
                int c = openC + (dj[openDir] * x);

                cell[r][c] &= ~PERIMETER;
                cell[r][c] |= ENTRANCE;
            }

            uint doorType = SetDoorType();

            DoorData door = new DoorData(doorR, doorC, DUNGEON_LEVEL_DOOR_TYPE.ARCH, "Archway");

            if(doorType == ARCH) {
                cell[doorR][doorC] |= ARCH;
                door.type = DUNGEON_LEVEL_DOOR_TYPE.ARCH;
                door.description = "Archway";
            }
            else if(doorType == OPEN) {
                cell[doorR][doorC] |= OPEN;
                cell[doorR][doorC] |= (Convert.ToUInt32('o') << 24);
                door.type = DUNGEON_LEVEL_DOOR_TYPE.OPEN;
                door.description = "Open Door";
            }
            else if(doorType == CLOSED) {
                cell[doorR][doorC] |= CLOSED;
                cell[doorR][doorC] |= (Convert.ToUInt32('c') << 24);
                door.type = DUNGEON_LEVEL_DOOR_TYPE.CLOSED;
                door.description = "Closed Door";
            }
            else if(doorType == LOCKED) {
                cell[doorR][doorC] |= LOCKED;
                cell[doorR][doorC] |= (Convert.ToUInt32('x') << 24);
                door.type = DUNGEON_LEVEL_DOOR_TYPE.LOCKED;
                door.description = "Locked Door";
            }
            else if(doorType == SECRET) {
                cell[doorR][doorC] |= SECRET;
                cell[doorR][doorC] |= (Convert.ToUInt32('s') << 24);
                door.type = DUNGEON_LEVEL_DOOR_TYPE.SECRET;
                door.description = "Secret Door";
            }
            else if(doorType == PORTC) {
                cell[doorR][doorC] |= PORTC;
                cell[doorR][doorC] |= (Convert.ToUInt32('#') << 24);
                door.type = DUNGEON_LEVEL_DOOR_TYPE.PORTCULLIS;
                door.description = "Portcullis";
            }

            if(sill.outIDWasSet) {
                door.outID = sill.outID;
            }

            if(!room.doorWasSet) {
                room.doorWasSet = true;
                room.door = new Dictionary<DUNGEON_LEVEL_DIRECTION, DoorData>();
            }

            if(!room.door.ContainsKey(openDir)) {
                room.door.Add(openDir, door);
            }
        }

    //Hehe...
    next:;
    }

    private List<SillData> DoorSills(RoomData room) {
        List<SillData> list = new List<SillData>();

        if(room.north >= 3) {
            for(int c = room.west; c <= room.east; c += 2) {
                SillData sill = CheckSill(room, room.north, c, DUNGEON_LEVEL_DIRECTION.NORTH);

                if(sill != null) {
                    list.Add(sill);
                }
            }
        }

        if(room.south <= (nRows - 3)) {
            for(int c = room.west; c <= room.east; c += 2) {
                SillData sill = CheckSill(room, room.south, c, DUNGEON_LEVEL_DIRECTION.SOUTH);

                if(sill != null) {
                    list.Add(sill);
                }
            }
        }

        if(room.west >= 3) {
            for(int r = room.north; r <= room.south; r += 2) {
                SillData sill = CheckSill(room, r, room.west, DUNGEON_LEVEL_DIRECTION.WEST);

                if(sill != null) {
                    list.Add(sill);
                }
            }
        }
        if(room.east <= (nCols - 3)) {
            for(int r = room.north; r <= room.south; r += 2) {
                SillData sill = CheckSill(room, r, room.east, DUNGEON_LEVEL_DIRECTION.EAST);

                if(sill != null) {
                    list.Add(sill);
                }
            }
        }

        return(Shuffle(list));
    }

    private SillData CheckSill(RoomData room, int sillR, int sillC, DUNGEON_LEVEL_DIRECTION dir) {
        int doorR = sillR + di[dir];
        int doorC = sillC + dj[dir];
        uint doorCell = cell[doorR][doorC];

        if((doorCell & PERIMETER) == NOTHING) {
            return (null);
        }
        if((doorCell & BLOCK_DOOR) != NOTHING) {
            return (null);
        }

        int outR = doorR + di[dir];
        int outC = doorC + dj[dir];
        uint outCell = cell[outR][outC];

        if((outCell & BLOCKED) != NOTHING) {
            return (null);
        }

        SillData sillData = new SillData(sillR, sillC, dir, doorR, doorC);

        if((outCell & ROOM) != NOTHING) {
            int outID = (int)(outCell & ROOM_ID) >> 6;

            if(outID == room.id) {
                return (null);
            }

            sillData.outID = outID;
        }

        return(sillData);
    }

    private List<T> Shuffle<T>(List<T> list) {
        int n = list.Count;

        while(n > 1) {
            n--;
            int k = random.Next(n + 1);

            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return (list);
    }

    private int AllocOpens(RoomData room) {
        int roomH = ((room.south - room.north) / 2) + 1;
        int roomW = ((room.east - room.west) / 2) + 1;
        int flumph = (int)Math.Sqrt(roomW * roomH);
        int nOpens = flumph + random.Next(flumph);

        return(nOpens);
    }

    private uint SetDoorType() {
        int i = random.Next(100);

        if(i < 40) {
            return(ARCH);
        }
        else if(i < 60) {
            return(OPEN);
        }
        else if(i < 70) {
            return(CLOSED);
        }
        else if(i < 80) {
            return(LOCKED);
        }
        else if(i < 90) {
            return(SECRET);
        }
        else {
            return(PORTC);
        }
    }

    public T Splice<T>(List<T> Source, int Start) {
        if(Source.Count == 0) {
            return(default(T));
        }

        T retVal = Source[Start];
        Source.RemoveRange(Start, 1);

        return(retVal);
    }

    private void LabelRooms() {
        for(int id = 1; id <= nRooms; ++id) {
            RoomData room = rooms[id];
            string label = room.id.ToString();
            int len = label.Length;
            int labelR = (room.north + room.south) / 2;
            int labelC = ((room.west + room.east - len) / 2) + 1;
            
            for(int c = 0; c < len; ++c) {
                char ch = label[c];
                cell[labelR][labelC + c] |= (Convert.ToUInt32(ch) << 24);
            }
        }
    }

    private void Corridors() {
        for(int i = 1; i < nI; ++i) {
            int r = (i * 2) + 1;

            for(int j = 1; j < nJ; ++j) {
                int c = (j * 2) + 1;

                if((cell[r][c] & CORRIDOR) != NOTHING) {
                    continue;
                }

                Tunnel(i, j);
            }
        }
    }

    private void Tunnel(int i, int j, DUNGEON_LEVEL_DIRECTION lastDir = DUNGEON_LEVEL_DIRECTION.UNKNOWN) {
        DUNGEON_LEVEL_DIRECTION[] dirs = TunnelDirs(lastDir);

        foreach(DUNGEON_LEVEL_DIRECTION dir in dirs) {
            if(OpenTunnel(i, j, dir)) {
                int nextI = i + di[dir];
                int nextJ = j + dj[dir];

                Tunnel(nextI, nextJ, dir);
            }
        }
    }

    private DUNGEON_LEVEL_DIRECTION[] TunnelDirs(DUNGEON_LEVEL_DIRECTION lastDir) {
        int p = corridorLayouts[corridorLayout];
        List<DUNGEON_LEVEL_DIRECTION> dirs = Shuffle(new List<DUNGEON_LEVEL_DIRECTION>(dj.Keys));

        if(lastDir != DUNGEON_LEVEL_DIRECTION.UNKNOWN && p > 0 && random.Next(100) < p) {
            dirs.Insert(0, lastDir);
        }
        
        return(dirs.ToArray());
    }

    private bool OpenTunnel(int i, int j, DUNGEON_LEVEL_DIRECTION dir) {
        int thisR = (i * 2) + 1;
        int thisC = (j * 2) + 1;
        int nextR = ((i + di[dir]) * 2) + 1;
        int nextC = ((j + dj[dir]) * 2) + 1;
        int midR = (thisR + nextR) / 2;
        int midC = (thisC + nextC) / 2;

        if(SoundTunnel(midR, midC, nextR, nextC)) {
            return(DelveTunnel(thisR, thisC, nextR, nextC));
        }
        else {
            return(false);
        }
    }

    private bool SoundTunnel(int midR, int midC, int nextR, int nextC) {
        if(nextR < 0 || nextR > nRows) {
            return(false);
        }
        if(nextC < 0 || nextC > nCols) {
            return(false);
        }
        
        int r1 = Math.Min(midR, nextR);
        int r2 = Math.Max(midR, nextR);
        int c1 = Math.Min(midC, nextC);
        int c2 = Math.Max(midC, nextC);

        for(int r = r1; r <= r2; ++r) {
            for(int c = c1; c <= c2; ++c) {
                if((cell[r][c] & BLOCK_CORR) != NOTHING) {
                    return(false);
                }
            }
        }

        return(true);
    }

    private bool DelveTunnel(int thisR, int thisC, int nextR, int nextC) {
        int r1 = Math.Min(thisR, nextR);
        int r2 = Math.Max(thisR, nextR);
        int c1 = Math.Min(thisC, nextC);
        int c2 = Math.Max(thisC, nextC);

        for(int r = r1; r <= r2; ++r) {
            for(int c = c1; c <= c2; ++c) {
                cell[r][c] &= ~ENTRANCE;
                cell[r][c] |= CORRIDOR;
            }
        }

        return(true);
    }

    private void EmplaceStairs() {
        //TODO: Add the ability to add more stairs.
        int n = 2;
        int nextType = 0;

        if(n == 0) {
            return;
        }

        List<StairData> list = StairEnds();

        if(list.Count == 0) {
            return;
        }

        //Add one stair that goes down.
        if(isFirstLevel) {
            StairData stair = Splice(list, random.Next(list.Count));

            int r = stair.row;
            int c = stair.col;

            cell[r][c] |= STAIR_DN;
            cell[r][c] |= (Convert.ToUInt32('d') << 24);
            stair.dir = DUNGEON_LEVEL_STAIR_DIRECTION.DOWN;
            stairs.Add(stair);
        }
        //Add one stair that goes up.
        else if(isLastLevel) {
            StairData stair = Splice(list, random.Next(list.Count));

            int r = stair.row;
            int c = stair.col;
            
            cell[r][c] |= STAIR_UP;
            cell[r][c] |= (Convert.ToUInt32('u') << 24);
            stair.dir = DUNGEON_LEVEL_STAIR_DIRECTION.UP;
            nextType--;

            stairs.Add(stair);
        }
        //Add 2 sets of stairs, one goes up, the other goes down.
        else {
            for(int i = 0; i < n; ++i) {
                StairData stair = Splice(list, random.Next(list.Count));

                int r = stair.row;
                int c = stair.col;

                if(nextType == 0) {
                    cell[r][c] |= STAIR_DN;
                    cell[r][c] |= (Convert.ToUInt32('d') << 24);
                    stair.dir = DUNGEON_LEVEL_STAIR_DIRECTION.DOWN;
                    nextType++;
                }
                else {
                    cell[r][c] |= STAIR_UP;
                    cell[r][c] |= (Convert.ToUInt32('u') << 24);
                    stair.dir = DUNGEON_LEVEL_STAIR_DIRECTION.UP;
                    nextType--;
                }

                stairs.Add(stair);
            }
        }
    }

    private List<StairData> StairEnds() {
        List<StairData> list = new List<StairData>();
        
        for(int i = 0; i < nI; ++i) {
            int r = (i * 2) + 1;
            
            for(int j = 0; j < nJ; ++j) {
                int c = (j * 2) + 1;

                if((cell[r][c] & CORRIDOR) == NOTHING) {
                    continue;
                }
                if((cell[r][c] & STAIRS) != NOTHING) {
                    continue;
                }

                foreach(DUNGEON_LEVEL_DIRECTION dir in stairEnd.Keys) {
                    //if(CheckTunnel(r, c, stairEnd[dir])) {
                        StairData end = new StairData(r, c);

                        int[] n = stairEnd[dir][DUNGEON_LEVEL_END_TYPE.NEXT][0];

                        end.nextRow = end.row + n[0];
                        end.nextCol = end.col + n[1];

                        list.Add(end);

                        break;
                    //}
                }
            }
        }

        return(list);
    }

    private bool CheckTunnel(int r, int c, Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>check) {
        int[][] list;

        if(check.ContainsKey(DUNGEON_LEVEL_END_TYPE.CORRIDOR)) {
            list = check[DUNGEON_LEVEL_END_TYPE.CORRIDOR];

            foreach(int[] p in list) {
                int rI = r + p[0];
                int cI = c + p[1];

                if(rI < 0 || rI > maxRow) {
                    continue;
                }
                if(cI < 0 || cI > maxCol) {
                    continue;
                }

                if(cell[rI][cI] == CORRIDOR) {
                    return(false);
                }
            }
        }

        if(check.ContainsKey(DUNGEON_LEVEL_END_TYPE.WALLED)) {
            list = check[DUNGEON_LEVEL_END_TYPE.WALLED];

            foreach(int[] p in list) {
                int rI = r + p[0];
                int cI = c + p[1];

                if(rI < 0 || rI > maxRow) {
                    continue;
                }
                if(cI < 0 || cI > maxCol) {
                    continue;
                }

                if((cell[rI][cI] & OPENSPACE) != NOTHING) {
                    return(false);
                }
            }
        }

        return(true);
    }

    private void CleanDungeon() {
        if(removeDeadends > 0) {
            RemoveDeadends();
        }

        FixDoors();
        EmptyBlocks();
    }

    private void RemoveDeadends() {
        CollapseTunnels(removeDeadends, closeEnd);
    }

    private void CollapseTunnels(DUNGEON_LEVEL_REMOVE_DEADENDS deadends, Dictionary<DUNGEON_LEVEL_DIRECTION, Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>> xc) {
        if((int)deadends == 0) {
            return;
        }
        
        bool all = ((int)deadends == 100);

        for(int i = 0; i < nI; ++i) {
            int r = (i * 2) + 1;

            for(int j = 0; j < nJ; ++j) {
                int c = (j * 2) + 1;

                if((cell[r][c] & OPENSPACE) == NOTHING) {
                    continue;
                }
                if((cell[r][c] & STAIRS) != NOTHING) {
                    continue;
                }
                if((all || (random.Next(100)) < (int)deadends) == false) {
                    continue;
                }

                Collapse(r, c, xc);
            }
        }
    }

    private void Collapse(int r, int c, Dictionary<DUNGEON_LEVEL_DIRECTION, Dictionary<DUNGEON_LEVEL_END_TYPE, int[][]>> xc) {
        if((cell[r][c] & OPENSPACE) == NOTHING) {
            return;
        }

        foreach(DUNGEON_LEVEL_DIRECTION dir in xc.Keys) {
            if(CheckTunnel(r, c, xc[dir])) {
                foreach(int[] p in xc[dir][DUNGEON_LEVEL_END_TYPE.CLOSE]) {
                    cell[r + p[0]][c + p[1]] = NOTHING;
                }

                if(xc[dir].ContainsKey(DUNGEON_LEVEL_END_TYPE.OPEN)) {
                    int[] p = xc[dir][DUNGEON_LEVEL_END_TYPE.OPEN][0];
                    cell[r + p[0]][c + p[1]] |= CORRIDOR;
                }
                if(xc[dir].ContainsKey(DUNGEON_LEVEL_END_TYPE.RECURSE)) {
                    int[] p = xc[dir][DUNGEON_LEVEL_END_TYPE.RECURSE][0];
                    Collapse(r + p[0], c + p[1], xc);
                }
            }
        }
    }

    private void FixDoors() {
        try {
            bool[][] fix = new bool[nRows + 1][];
            for(int r = 0; r <= nRows; ++r) {
                fix[r] = new bool[nCols + 1];

                for(int c = 0; c <= nCols; ++c) {
                    fix[r][c] = false;
                }
            }

            foreach(RoomData room in rooms.Values) {
                List<DUNGEON_LEVEL_DIRECTION> dirs = new List<DUNGEON_LEVEL_DIRECTION>(room.door.Keys);

                foreach(DUNGEON_LEVEL_DIRECTION dir in dirs) {
                    List<DoorData> shiny = new List<DoorData>();

                    foreach(DUNGEON_LEVEL_DIRECTION dirDoor in room.door.Keys) {
                        int doorR = room.door[dirDoor].row;
                        int doorC = room.door[dirDoor].col;
                        uint doorCell = cell[doorR][doorC];

                        if((doorCell & OPENSPACE) == NOTHING) {
                            continue;
                        }

                        if(fix[doorR][doorC]) {
                            shiny.Add(room.door[dirDoor]);
                        }
                        else {
                            int outID;

                            if(room.door[dirDoor].outIDWasSet) {
                                outID = room.door[dirDoor].outID;
                                DUNGEON_LEVEL_DIRECTION outDir = opposite[dir];
                                RoomData outRoom = rooms[outID];

                                if(!outRoom.doorWasSet) {
                                    outRoom.door = new Dictionary<DUNGEON_LEVEL_DIRECTION, DoorData>();
                                }

                                if(!outRoom.door.ContainsKey(outDir)) {
                                    outRoom.door.Add(outDir, room.door[dirDoor]);
                                }
                            }

                            shiny.Add(room.door[dirDoor]);
                            fix[doorR][doorC] = true;
                        }
                    }

                    if(shiny.Count > 0) {
                        doors = shiny;
                        doors.AddRange(shiny);
                    }
                    else {
                        room.door.Remove(dir);
                    }
                }
            }
        }
        catch {
            throw;
        }
    }

    private void EmptyBlocks() {
        for(int r = 0; r <= nRows; ++r) {
            for(int c = 0; c <= nCols; ++c) {
                if((cell[r][c] & BLOCKED) != NOTHING) {
                    cell[r][c] = NOTHING;
                }
            }
        }
    }
}
