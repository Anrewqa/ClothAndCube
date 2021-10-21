using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameLogic
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private ClothGenerator _clothGenerator;
        [SerializeField] private PathFollower _pathFollower;

        private void Start()
        {
            _pathFollower.Initialize(_clothGenerator.Path);
            _pathFollower.PathComplete += OnPathComplete;
        }

        private void OnPathComplete()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}