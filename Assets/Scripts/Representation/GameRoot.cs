using Asteroids.Input;
using Asteroids.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids.View
{
    public class GameRoot : MonoBehaviour
    {
        [SerializeField] private ShipUIView shipView;
        [SerializeField] private ShipInputController shipInput;
        [SerializeField] private ObjectsViewFactory objectsViewFactory;
        [SerializeField] private MoveObjectView player;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PopupGameOver popupGameOver;

        private GameModel gameModel;

        private void Awake()
        {
            gameModel = new GameModel(mainCamera);

            player.Init(mainCamera, gameModel.Player);
            shipInput.Init(gameModel.Player);

            gameModel.OnModelCreated += OnModelCreateObject;
            gameModel.OnModelDestroy += OnModelDestroyObject;
            gameModel.OnGameOver += OnGameOver;
            gameModel.PlayerLazer.OnBulletCountChanged += OnLazerShootCountChanged;

            shipView.SetTextLazerShootCount($"Lazer shoots {gameModel.PlayerLazer.Bullets}:{gameModel.PlayerLazer.MaxBullets}");
        }

        private void OnDestroy()
        {
            gameModel.OnModelCreated -= OnModelCreateObject;
            gameModel.OnModelDestroy -= OnModelDestroyObject;
            gameModel.PlayerLazer.OnBulletCountChanged -= OnLazerShootCountChanged;
        }

        private void Update()
        {
            gameModel.Update(Time.deltaTime);

            if (gameModel.PlayerLazer.Bullets < gameModel.PlayerLazer.MaxBullets)
            {
                shipView.SetLazerCooldown($"Lazer restore: {gameModel.PlayerLazer.RestoreLazerTime:0.00} sec");
            }
            else
            {
                shipView.SetLazerCooldown("Lazer ready.");
            }
            shipView.SetCoordinates($"Coordinates X:{gameModel.Player.Position.x:0.00} Y:{gameModel.Player.Position.y:0.00}");
            shipView.SetAngle($"Angle: {gameModel.Player.Angle:0.00}");
            shipView.SetSpeed($"Speed: {gameModel.Player.Velocity.magnitude:0.00}");
        }

        private void OnLazerShootCountChanged(int count)
        {
            shipView.SetTextLazerShootCount($"Lazer shoots {count}:{gameModel.PlayerLazer.MaxBullets}");
        }

        private void OnModelCreateObject(object reference)
        {
            objectsViewFactory.Create(reference);
        }

        private void OnModelDestroyObject(object reference)
        {
            objectsViewFactory.Remove(reference);
        }

        private void OnGameOver()
        {
            popupGameOver.Show(gameModel.Score, () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }
    }
}

