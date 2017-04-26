using UnityEngine;
using System.Collections.Generic;

public enum DUNGEON_DIFFICULTY {
    EXPLORER = 0,
    WARRIOR = 1,
    BERSERKER = 2,
    VETERAN = 3,
    DUNGEON_MASTER = 4,
};

public class Dungeon : MonoBehaviour {

    public DUNGEON_DIFFICULTY difficulty;
    public int nLevels;
    public List<DungeonLevel> levels;

    public List<Transform> boards;

    public Dungeon(DUNGEON_DIFFICULTY difficulty) {
        this.difficulty = difficulty;

        nLevels = 0;
        levels = new List<DungeonLevel>();
    }

    public void GenerateDungeon() {
        nLevels = RandomAmountOfLevels();

        for(int i = 0; i < nLevels; ++i) {
            DUNGEON_LEVEL_SIZE levelSize = RandomDungeonLevelSize(difficulty);
            Vector2 levelSizeValues = GetDungeonLevelSize(levelSize);
            Vector2 roomSizeValues = RandomDungeonLevelRoomSize(levelSize);

            DungeonLevel level = new DungeonLevel(System.Environment.TickCount, levelSize, (int)levelSizeValues.x, (int)levelSizeValues.y, RandomDungeonLevelLayout(), (int)roomSizeValues.x, (int)roomSizeValues.y,
                                                  RandomDungeonLevelRoomLayout(), RandomDungeonLevelCorridorLayout(), RandomDungeonLevelRemoveDeadends(), (nLevels >= 2) ? true : false, i, (i == 0), (i == nLevels - 1));

            levels.Add(level);
        }

        BuildDungeonGOs();
    }

    public int RandomAmountOfLevels() {
        switch(difficulty) {
            case DUNGEON_DIFFICULTY.EXPLORER:
                return (Random.Range(1, 2));
            case DUNGEON_DIFFICULTY.WARRIOR:
                return (Random.Range(1, 4));
            case DUNGEON_DIFFICULTY.BERSERKER:
                return (Random.Range(3, 5));
            case DUNGEON_DIFFICULTY.VETERAN:
                return (Random.Range(4, 7));
            case DUNGEON_DIFFICULTY.DUNGEON_MASTER:
                return (Random.Range(5, 10));
            default:
                return (1);
        }
    }

    //MINUTE = 0, TINY = 1, SMALL = 2, PETITE = 3, MEDIUM = 4, LARGE = 5, HUGE = 6, MASSIVE = 7, GIGANTIC = 8, EPIC = 9, CUSTOM = 10
    public DUNGEON_LEVEL_SIZE RandomDungeonLevelSize(DUNGEON_DIFFICULTY difficulty) {
        switch(difficulty) {
            case DUNGEON_DIFFICULTY.EXPLORER:
                return ((DUNGEON_LEVEL_SIZE)Random.Range(0, 4));
            case DUNGEON_DIFFICULTY.WARRIOR:
                return ((DUNGEON_LEVEL_SIZE)Random.Range(1, 5));
            case DUNGEON_DIFFICULTY.BERSERKER:
                return ((DUNGEON_LEVEL_SIZE)Random.Range(2, 6));
            case DUNGEON_DIFFICULTY.VETERAN:
                return ((DUNGEON_LEVEL_SIZE)Random.Range(3, 7));
            case DUNGEON_DIFFICULTY.DUNGEON_MASTER:
                return ((DUNGEON_LEVEL_SIZE)Random.Range(4, 9));
            default:
                return ((DUNGEON_LEVEL_SIZE)Random.Range(0, 9));
        }
    }

    public Vector2 GetDungeonLevelSize(DUNGEON_LEVEL_SIZE levelSize, int nRows = 0, int nCols = 0) {
        switch(levelSize) {
            case DUNGEON_LEVEL_SIZE.MINUTE:
                return (new Vector2(20, 20));
            case DUNGEON_LEVEL_SIZE.TINY:
                return (new Vector2(30, 30));
            case DUNGEON_LEVEL_SIZE.SMALL:
                return (new Vector2(40, 40));
            case DUNGEON_LEVEL_SIZE.PETITE:
                return (new Vector2(50, 50));
            case DUNGEON_LEVEL_SIZE.MEDIUM:
                return (new Vector2(70, 70));
            case DUNGEON_LEVEL_SIZE.LARGE:
                return (new Vector2(100, 100));
            case DUNGEON_LEVEL_SIZE.HUGE:
                return (new Vector2(150, 150));
            case DUNGEON_LEVEL_SIZE.MASSIVE:
                return (new Vector2(200, 200));
            case DUNGEON_LEVEL_SIZE.GIGANTIC:
                return (new Vector2(300, 300));
            case DUNGEON_LEVEL_SIZE.EPIC:
                return (new Vector2(500, 500));
            default:
                return (new Vector2(nRows, nCols));
        }
    }

    public DUNGEON_LEVEL_LAYOUT RandomDungeonLevelLayout() {
        return ((DUNGEON_LEVEL_LAYOUT)Random.Range((int)(DUNGEON_LEVEL_LAYOUT.UNKNOWN) + 1, (int)(DUNGEON_LEVEL_LAYOUT.SIZE) - 1));
    }

    public Vector2 RandomDungeonLevelRoomSize(DUNGEON_LEVEL_SIZE levelSize, int nRows = 0, int nCols = 0) {
        switch(levelSize) {
            case DUNGEON_LEVEL_SIZE.MINUTE:
                return (new Vector2(2, 5));
            case DUNGEON_LEVEL_SIZE.TINY:
                return (new Vector2(3, 6));
            case DUNGEON_LEVEL_SIZE.SMALL:
                return (new Vector2(3, 7));
            case DUNGEON_LEVEL_SIZE.PETITE:
                return (new Vector2(3, 9));
            case DUNGEON_LEVEL_SIZE.MEDIUM:
                return (new Vector2(3, 11));
            case DUNGEON_LEVEL_SIZE.LARGE:
                return (new Vector2(4, 14));
            case DUNGEON_LEVEL_SIZE.HUGE:
                return (new Vector2(4, 16));
            case DUNGEON_LEVEL_SIZE.MASSIVE:
                return (new Vector2(4, 18));
            case DUNGEON_LEVEL_SIZE.GIGANTIC:
                return (new Vector2(5, 20));
            case DUNGEON_LEVEL_SIZE.EPIC:
                return (new Vector2(6, 25));
            default:
                return (new Vector2((nRows + nCols) / 20, (nCols + nRows) / 10));
        }
    }

    public DUNGEON_LEVEL_ROOM_LAYOUT RandomDungeonLevelRoomLayout() {
        return ((DUNGEON_LEVEL_ROOM_LAYOUT)Random.Range((int)(DUNGEON_LEVEL_ROOM_LAYOUT.UNKNOWN) + 1, (int)(DUNGEON_LEVEL_ROOM_LAYOUT.SIZE) - 1));
    }

    public DUNGEON_LEVEL_CORRIDOR_LAYOUT RandomDungeonLevelCorridorLayout() {
        return ((DUNGEON_LEVEL_CORRIDOR_LAYOUT)Random.Range((int)(DUNGEON_LEVEL_CORRIDOR_LAYOUT.UNKNOWN) + 1, (int)(DUNGEON_LEVEL_CORRIDOR_LAYOUT.SIZE) - 1));
    }

    public DUNGEON_LEVEL_REMOVE_DEADENDS RandomDungeonLevelRemoveDeadends() {
        return ((DUNGEON_LEVEL_REMOVE_DEADENDS)Random.Range((int)(DUNGEON_LEVEL_REMOVE_DEADENDS.NONE), (int)(DUNGEON_LEVEL_REMOVE_DEADENDS.SIZE) - 1));
    }

    public void BuildDungeonGOs() {
        DungeonManager dm = DungeonManager.instance;
        boards = new List<Transform>();
        int i = 0;

        foreach(DungeonLevel level in levels) {
            Transform board = new GameObject("Board" + i).transform;
            boards.Add(board);

            for(int r = 0; r <= level.nRows; r++) {
                for(int c = 0; c <= level.nCols; c++) {
                    GameObject toInstantiate = dm.stoneWallTiles[Random.Range(0, dm.stoneWallTiles.Length)];

                    if((level.cell[r][c] & DungeonLevel.STAIR_UP) != DungeonLevel.NOTHING) {
                        toInstantiate = dm.stoneStairsUpTile;
                    }
                    else if((level.cell[r][c] & DungeonLevel.STAIR_DN) != DungeonLevel.NOTHING) {
                        toInstantiate = dm.stoneStairsDownTile;
                    }
                    else if((level.cell[r][c] & DungeonLevel.ARCH) != DungeonLevel.NOTHING) {
                        toInstantiate = dm.stoneArchTile;
                    }
                    else if((level.cell[r][c] & DungeonLevel.OPEN) != DungeonLevel.NOTHING) {
                        toInstantiate = dm.stoneDoorOpenTile;
                    }
                    else if((level.cell[r][c] & DungeonLevel.CLOSED) != DungeonLevel.NOTHING) {
                        toInstantiate = dm.stoneDoorClosedTile;
                    }
                    else if((level.cell[r][c] & DungeonLevel.LOCKED) != DungeonLevel.NOTHING) {
                        toInstantiate = dm.stoneDoorLockedTile;
                    }
                    else if((level.cell[r][c] & DungeonLevel.SECRET) != DungeonLevel.NOTHING) {
                        toInstantiate = dm.stoneDoorSecretTile;
                    }
                    else if((level.cell[r][c] & DungeonLevel.ROOM) != DungeonLevel.NOTHING) {
                        toInstantiate = dm.stoneFloorTiles[Random.Range(0, dm.stoneFloorTiles.Length)];
                    }
                    else if((level.cell[r][c] & DungeonLevel.CORRIDOR) != DungeonLevel.NOTHING) {
                        toInstantiate = dm.corridorTiles[Random.Range(0, dm.corridorTiles.Length)];
                    }
                    else {
                        toInstantiate = dm.stoneWallTiles[Random.Range(0, dm.stoneWallTiles.Length)];
                    }

                    GameObject instance = Instantiate(toInstantiate, new Vector3(r, c, 0f), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(board);
                }
            }

            if(!level.isFirstLevel) {
                board.gameObject.SetActive(false);
            }

            i++;
        }
    }

    public void DestroyDungeonGOs() {
        List<GameObject> boardChildren = new List<GameObject>();

        foreach(Transform board in boards) {
            foreach(Transform child in board) {
                boardChildren.Add(child.gameObject);
            }

            boardChildren.ForEach(child => Destroy(child));
            boardChildren.Clear();
        }
    }
}