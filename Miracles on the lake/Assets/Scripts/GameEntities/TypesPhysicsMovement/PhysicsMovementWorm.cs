using UnityEngine;

namespace GameEntities.TypesPhysicsMovement
{
    public class PhysicsMovementWorm : PhysicsMovementEntity
    {
        public void ChooseForce(int indexSpawnPoint)
        {
            if (indexSpawnPoint == 0)
                DoForce(Random.Range(4f, 7f), Random.Range(4f, 6f));
            else if (indexSpawnPoint == 1)
                DoForce(Random.Range(1f, 4f), Random.Range(1f, 3f));
            else if (indexSpawnPoint == 6)
                DoForce(Random.Range(-3f, -10f), Random.Range(-1f, -4f));
            else if (indexSpawnPoint == 7)
                DoForce(Random.Range(-4f, -12f), Random.Range(-4f, -6f));
            else 
                DoForce(Random.Range(-2f, 2f), Random.Range(-1f, 1f));
        }

        private void DoForce(float x, float y)
        {
            _rigidbody.AddForce(new Vector2(x, y) * _speed);
        }
    }
}

