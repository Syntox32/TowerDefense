using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.Graphics;
using SFML.Audio;

using ProjectGamma.Pathfinding;
using ProjectGamma.Entities;
using ProjectGamma.Tiling;
using ProjectGamma.Utilities;
using ProjectGamma.UI;

namespace ProjectGamma
{
    public class Level : Scene
    {
        public const float SpriteScalar = 8;
        private const int tilesX = 20;
        private const int tilesY = 12;
        private const int damagePerEnemy = 1;

        public int UnitBalance { get; private set; }
        public int WaveNumber { get; private set; }
        public int Health { get; private set; }

        public bool ModeLevelEditor { get; set; }
        public bool ModePlaceTower { get; set; }
        public bool ModeSelectTower { get; set; }

        public bool ModePauseRendering { get; set; }
        public bool ModePauseUpdate { get; set; }

        public WaveState CurrentState { get; private set; }
        public Wave CurrentWave { get; private set; }

        public View Camera;

        private Game _game;
        private LevelOverlay _overlayLevel;
        private InfoOverlay _overlayInfo;
        private TileMap _tileMap;
        private GridLayer<TowerEntity> _towerMap;
        private EntityPool<EnemyEntity> _enemyPool;
        private AStar<Tile> _pathfinding;
        private ScanlineShader _scanlines;
        private RenderTexture _renderTex;

        private readonly List<Tile> Path;

        private Image _spriteSheet;
        private Texture _towerBasic;
        private Texture _towerIntermediate;
        private Texture _towerAdvanced;
        private Texture _towerEndGame;
        private Texture _texTileBasic;
        private Texture _texTileRoad;
        private Texture _texTileTurn;
        
        public TileMap TileMap 
        { 
            get { return _tileMap; }
        }

        public GridLayer<TowerEntity> TowerMap
        {
            get { return _towerMap; }
        }

        public Level(Game game)
        {
            LoadAssets();
            ConfigureEntities();

            UnitBalance = 1200; // default
            Health = 100;
            WaveNumber = 0;
            CurrentState = WaveState.None;
            
            //_enemyPool = new EntityPool(1000);
            _scanlines = new ScanlineShader(new Vector2u((uint)Game.Size.X, (uint)Game.Size.Y));
            _renderTex = new RenderTexture((uint)Game.Size.X, (uint)Game.Size.Y, true);

            var tileSize = _texTileBasic.Size.X * SpriteScalar;

            _game = game;
            _overlayInfo = new InfoOverlay(_game, this);
            _overlayLevel = new LevelOverlay(this, SpriteScalar, tileSize);
            _overlayLevel.SetSelectionState(_texTileRoad, 128);
            Camera = new View(new Vector2f(Game.Size.X / 2, Game.Size.Y / 2), Game.Size);

            _tileMap = new TileMap(this, _texTileBasic, tilesX, tilesY, tileSize);
            _towerMap = new GridLayer<TowerEntity>(tilesX, tilesY, tileSize);
            _pathfinding = new AStar<Tile>(HeuristicMethod.Manhatten);
            //Path = _pathfinding.CalculatePath(_tileMap.StartTile, _tileMap.EndTile, _tileMap).ToList<Tile>();
            //game.MouseButtonPressed += (sender, e) => { Camera.Move(new Vector2f(-10f, -10f)); };

            _enemyPool = new EntityPool<EnemyEntity>(1000);

            UpdateInfo();
        }

        public void UpdateInfo()
        {
            _overlayInfo.Invalidate = true;
        }

        public void NextWave() 
        {
             
        }

        public void DelegateTowerActions() { }

        public bool BuyTower(Vector2f pos, TowerEntityType type)
        {
            switch(type)
            {
                // TODO: Check if a tower is null before subtrackting units
            case TowerEntityType.Basic:
                var stats = EntityStats.TowerEntityStatTable[type];

                if (UnitBalance >= stats.UnitCost)
                {
                    UnitBalance -= stats.UnitCost;

                    var normIndex = _tileMap.GetGridCoords(pos);
                    int index = _towerMap.GetIndex((int)normIndex.X, (int)normIndex.Y);

                    var mPos = _overlayLevel.GetMouseGridCoordsToLevel();
                    var tileSize = 8 * SpriteScalar;
                    var texSize = _towerBasic.Size * (uint)SpriteScalar;

                    var newPos = new Vector2f(
                        mPos.X + ((tileSize / 2) - (texSize.X / 2)),
                        mPos.Y + ((tileSize / 2) - (texSize.Y / 2)));

                    if (_towerMap[index] == null)
                    {
                        _towerMap[index] = new BasicTower(index, newPos, this, _towerBasic);

                        UpdateInfo();
                        return true;
                    }
                }
                break;
            }

            return false;
        }

        public bool SellTower() { return false; }

        public void SetTile(Vector2f pos, bool road)
        {
            var coords = _tileMap.GetGridCoords(pos);
            _tileMap[(int)coords.X, (int)coords.Y].SetState(true, _texTileRoad, false);
        }

        public void RotateTile(Vector2f pos)
        {
            var coords = _tileMap.GetGridCoords(pos);
            _tileMap[(int)coords.X, (int)coords.Y].CreateRotation(90.0f);
        }

        private void ConfigureEntities() { }

        private void LoadAssets()
        {
            var assetPath = @"D:\Dev\Cs\ProjectGamma\Resources\Img\";
            _spriteSheet = new Image(assetPath + "spritesheet.png");

            _texTileBasic = Utils.GetTexture(_spriteSheet, 16, 0, 8, 8);
            _texTileRoad = Utils.GetTexture(_spriteSheet, 16, 8, 8, 8);
            _texTileTurn = Utils.GetTexture(_spriteSheet, 16, 16, 8, 8);
            _towerBasic = Utils.GetTexture(_spriteSheet, 0, 16, 5, 7);
        }

        public void LoadLevel() { }

        public bool SaveCurrentState() { return false; }

        public override void Update(float delta)
        {
            if (!ModePauseUpdate)
            {
                base.Update(delta);
                _towerMap.Update(delta);


            }

            _overlayInfo.Update(delta);
            _overlayLevel.Update(delta);
        }
        
        public override void Render(RenderTarget target, RenderStates states)
        {
            if (!ModePauseRendering)
            {
                target.SetView(Camera);

                _tileMap.Draw(target, states);
                _towerMap.Draw(target, states);

                //_selection.Draw(target, states);
            }

            target.SetView(target.DefaultView);

            // Draw the interface without the level camera
            _overlayInfo.Render(target, states);
            _overlayLevel.Render(target, states);
        }
    }
}
