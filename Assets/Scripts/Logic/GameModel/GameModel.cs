using System;
using UnityEngine;

public class GameModel
{
    public event Action<object> OnModelCreated;
    public event Action<object> OnModelDestroy;
    public event Action OnGameOver;
    public ShipModel Player => player;
    public WeaponLazerModel PlayerLazer => lazer;

    private readonly ObjectsSpawner objectsSpawner;
    private readonly MoveObjectsModel moveObjectsModel;    
    private readonly CollisionObjectLogic collisionObjectLogic;
    private readonly LazerCollisionLogic lazerCollisionLogic;
    private readonly ShipModel player;
    private readonly WeaponLazerModel lazer;    

    private Vector2 playerStartPosition = Vector2.one / 2f;

    public GameModel()
    {
        player = new ShipModel(playerStartPosition, Config.SHIP_SIZE, Config.SHIP_ROTATION_SPEED, Config.SHIP_ACCELERATION);
        objectsSpawner = new ObjectsSpawner(player, Config.LEVEL_TIME, Config.ASTEROIDS_ON_LEVEL);
        objectsSpawner.OnObjectSpawn += OnObjectSpawn;

        var gun = new WeaponGunModel(objectsSpawner);
        lazer = new WeaponLazerModel(Config.LAZER_SHOOT_COUNT, Config.LAZER_COOLDOWN);
        player.InitWeapons(gun, lazer);

        moveObjectsModel = new MoveObjectsModel(objectsSpawner.AllSpawned);
        collisionObjectLogic = new CollisionObjectLogic(objectsSpawner);
        collisionObjectLogic.OnObjectDestroy += OnObjectDestroy;
        collisionObjectLogic.OnShipCollide += OnShipCollide;
    }

    public void Update(float deltaTime)
    {
        objectsSpawner.Update(deltaTime);
        moveObjectsModel.Update(deltaTime);
        collisionObjectLogic.ProcessCollisions();
    }

    private void OnObjectSpawn(IMoveObject model)
    {
        OnModelCreated?.Invoke(model);
    }

    private void OnObjectDestroy(object model)
    {
        OnModelDestroy?.Invoke(model);
    }

    private void OnShipCollide()
    {
        OnGameOver?.Invoke();
    }
}
