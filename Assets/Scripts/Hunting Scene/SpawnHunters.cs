using UnityEngine;

public class SpawnHunters : MonoBehaviour
{
    public GameObject hunter;
    public int hunterCount;

    // Spawn hunters randomly along the edge of the map
    public void Spawn()
    {
        for (int h = 0; h < hunterCount; h++)
        {
            int edge = Random.Range(0, 4);
            float x_pos = 0f;
            float z_pos = 0f;
            switch(edge)
            {
                case 0: // North
                    x_pos = Random.Range(-20, 20);
                    z_pos = 20;
                    break;
                case 1: // South
                    x_pos = Random.Range(-20, 20);
                    z_pos = -20;
                    break;
                case 2: // East
                    x_pos = 20;
                    z_pos = Random.Range(-20, 20);
                    break;
                case 3: // West
                    x_pos = -20;
                    z_pos = Random.Range(-20, 20);
                    break;
            }
            Vector3 hunterPosition = new Vector3(x_pos, 1f, z_pos);
            GameObject hunterClone = Instantiate(hunter, hunterPosition, Quaternion.identity);
            hunterClone.transform.parent = transform;
        }
    }

    // Kill all children
    public void Despawn()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
