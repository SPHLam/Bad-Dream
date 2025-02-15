using UnityEngine;

public class Clown : MonoBehaviour
{
    private int _numsOfBalloon = 30;
    private void OnEnable()
    {
        SpawnBalloons();
    }

    private void SpawnBalloons()
    {
        for (int i = 0; i < _numsOfBalloon; i++)
        {
            Balloon balloon = BalloonObjectPool.Instance.Get();
            Vector3 spawnPos = transform.position + new Vector3(Random.Range(-100f, 100f), Random.Range(1f, 4f), 0);
            balloon.transform.position = spawnPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerDetect")
        {
            Debug.Log("Clown touches Player");
        }
    }
}
