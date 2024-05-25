using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids.Model
{
    public class ObjectsSpawner : IObjectSpawn
    {
        public event Action<object> OnObjectSpawn;

        public readonly List<IMoveObject> allMoveObjects;

        private readonly float levelTime;
        private readonly int asteroidsInLevel;

        private List<SpawnData> spawnQuene;
        private IMoveObject player;
        private float currentLevelTime;

        public ObjectsSpawner(IMoveObject player, float levelTime, int asteroidsInLevel)
        {
            this.levelTime = levelTime;
            this.asteroidsInLevel = asteroidsInLevel;
            this.player = player;

            allMoveObjects = new() { player };

            UpdateSpawnObjects();
        }

        public void UpdateSpawnObjects()
        {
            currentLevelTime = 0;

            spawnQuene = new List<SpawnData> {
            new SpawnData() { time = 3, GetObject = CreateUFO },
            new SpawnData() { time = 10, GetObject = CreateUFO },
            new SpawnData() { time = 40, GetObject = CreateUFO },
            new SpawnData() { time = 90, GetObject = CreateUFO },
            new SpawnData() { time = 200, GetObject = CreateUFO },
            new SpawnData() { time = 400, GetObject = CreateUFO },
        };

            //add asteroids
            for (int asteroid = 0; asteroid < asteroidsInLevel; asteroid++)
            {
                spawnQuene.Add(new SpawnData() { time = UnityEngine.Random.Range(0, levelTime), GetObject = CreateAsteroid });
            }
        }

        public void Update(float deltaTime)
        {
            currentLevelTime += deltaTime;

            spawnQuene.Where(x => x.time < currentLevelTime).ToList().ForEach(x => {
                var newMoveObject = x.GetObject();
                allMoveObjects.Add(newMoveObject);
                OnObjectSpawn?.Invoke(newMoveObject);
                spawnQuene.Remove(x);
            });
        }

        public void CreateAsteroidParts(Vector2 position)
        {
            int count = UnityEngine.Random.Range(2, 6);
            for (int i = 0; i < count; i++)
            {
                Vector2 velocity = new Vector2(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f)) * Config.ASTEROID_PART_SPEED;
                var newAsteroid = new AsteroidModel(position, velocity, Config.ASTEROID_PART_SIZE);
                allMoveObjects.Add(newAsteroid);
                OnObjectSpawn?.Invoke(newAsteroid);
            }
        }

        public void AddMoveObject(IMoveObject moveObject)
        {
            allMoveObjects.Add(moveObject);
            OnObjectSpawn?.Invoke(moveObject);
        }

        private IMoveObject CreateAsteroid()
        {
            Vector2 velocity = new Vector2(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f)) * Config.ASTEROID_SPEED;
            return new AsteroidModel(GetRandomPositionOnBorder(), velocity, Config.ASTEROID_SIZE);
        }

        private IMoveObject CreateUFO()
        {
            return new ShipUFOModel(GetRandomPositionOnBorder(), player, Config.UFO_SIZE, Config.UFO_ACCELERATION, Config.MAX_MOVE_SPEED);
        }

        private Vector2 GetRandomPositionOnBorder()
        {
            int selectZeroAxis = UnityEngine.Random.Range(0, 2);
            return new Vector2(selectZeroAxis == 0 ? 0f : UnityEngine.Random.Range(0f, 1f),
                               selectZeroAxis == 0 ? UnityEngine.Random.Range(0f, 1f) : 0f);
        }

        private class SpawnData
        {
            public float time;
            public Func<IMoveObject> GetObject;
        }
    }
}
