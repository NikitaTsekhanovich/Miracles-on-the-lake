using UnityEngine;

namespace GameEntities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private AudioSource _spawnAudio;

        private void Start()
        {
            _spawnAudio.Play();
        }

        public void DoDestroy()
        {
            Destroy(gameObject);
        }
    }
}

