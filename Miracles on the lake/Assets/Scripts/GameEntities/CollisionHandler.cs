using UnityEngine;

namespace GameEntities
{
    public class CollisionHandler : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("DestroyingWalls"))
            {
                GetComponent<Entity>().DoDestroy();
            }
        }
    }
}

