using System;
using UnityEngine;

namespace Asteroids.Model
{
    public class GameModel
    {
        public event Action<object> OnModelCreated;
        public event Action<object> OnModelDestroy;
        public event Action OnGameOver;
        public ShipModel Player => player;
        public int Score => scoreModel.Score;
        public WeaponLazerModel PlayerLazer => lazer;
        private readonly ObjectsSpawner objectsSpawner;
        private readonly BordersCheckModel bordersCheckModel;
        private readonly CollisionObjectLogic collisionObjectLogic;
        private readonly LazerCollisionLogic lazerCollisionLogic;
        private readonly UpdateObjectsModel updateObjectsModel;
        private readonly ObserverObjectDestroy objectDestroyModel;
        private readonly WeaponLazerModel lazer;
        private readonly ShipModel player;
        private readonly ScoreModel scoreModel;

        private Vector2 playerStartPosition = Vector2.one / 2f;
        private bool isGameOver = false;

        public GameModel(Camera camera)
        {
            player = new ShipModel(playerStartPosition, Config.SHIP_SIZE, Config.SHIP_ROTATION_SPEED, Config.SHIP_ACCELERATION);

            objectsSpawner = new ObjectsSpawner(player, Config.LEVEL_TIME, Config.ASTEROIDS_ON_LEVEL);
            objectsSpawner.OnObjectSpawn += OnObjectSpawn;

            objectDestroyModel = new ObserverObjectDestroy(objectsSpawner.allMoveObjects);
            objectDestroyModel.OnObjectDestroy += OnObjectDestroy;

            lazerCollisionLogic = new LazerCollisionLogic(camera, objectsSpawner, objectDestroyModel);
            lazerCollisionLogic.OnBulletCreate += OnObjectSpawn;
            lazerCollisionLogic.OnBulletDestroy += OnObjectDestroy;

            var gun = new WeaponGunModel(objectsSpawner, objectDestroyModel);
            lazer = new WeaponLazerModel(Config.LAZER_SHOOT_COUNT, Config.LAZER_COOLDOWN, lazerCollisionLogic, objectDestroyModel);
            player.InitWeapons(gun, lazer);

            bordersCheckModel = new BordersCheckModel(objectsSpawner.allMoveObjects);

            collisionObjectLogic = new CollisionObjectLogic(objectsSpawner, objectDestroyModel);
            collisionObjectLogic.OnShipCollide += OnShipCollide;

            updateObjectsModel = new UpdateObjectsModel(new IObjectSpawn[] { objectsSpawner, lazer }, objectDestroyModel);
            updateObjectsModel.Add(lazer);
            scoreModel = new();
        }

        public void Update(float deltaTime)
        {
            if (isGameOver)
            {
                return;
            }
            objectsSpawner.Update(deltaTime);
            bordersCheckModel.Update(deltaTime);
            collisionObjectLogic.ProcessCollisions();
            updateObjectsModel.Update(deltaTime);
            lazerCollisionLogic.ProcessCollisions();
        }

        private void OnObjectSpawn(object model)
        {
            OnModelCreated?.Invoke(model);
        }

        private void OnObjectDestroy(object model)
        {
            scoreModel.AddScore(model);
            OnModelDestroy?.Invoke(model);
        }

        private void OnShipCollide()
        {
            isGameOver = true;
            OnGameOver?.Invoke();
        }
    }
}

