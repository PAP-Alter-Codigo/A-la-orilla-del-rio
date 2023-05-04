using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour{
    [SerializeField]
    private GameObject obstacleSquare, obstacleFly;
    [SerializeField]
    private float mountainSpeed, mountainFinalX, maxPeakScale, minPeakScale, maxPeakXOffset, maxPeakYOffset, maxPeakRotation, flyingObstacleYOffset;
    [SerializeField]
    private float spawnMountainChance = 0.003f, spawnFlyingChance = 0.001f;

    // Update is called once per frame
    void Update(){
        if(Time.timeScale == 0.0f) return;
        if(Random.value < spawnMountainChance) {
            int peaks = Random.Range(1, 4);
            GameObject mountain = new GameObject("mountain");
            for(int i = 0; i < peaks; i++) {
                GameObject mntnPeak = Instantiate(obstacleSquare, mountain.transform);
                mntnPeak.transform.localScale = new Vector3(Random.value * maxPeakScale + minPeakScale,Random.value * maxPeakScale + minPeakScale,Random.value * maxPeakScale + minPeakScale);
                mntnPeak.transform.localPosition += maxPeakXOffset * Random.value * Vector3.right;
                mntnPeak.transform.localPosition += maxPeakYOffset * Random.value * Vector3.up;
                mntnPeak.transform.rotation = Quaternion.Euler(0, 0, maxPeakRotation * Random.value);
            }
            MovingStuff mntnStuff = mountain.AddComponent<MovingStuff>();
            mntnStuff.finalX = mountainFinalX;
            mntnStuff.speed = mountainSpeed;
        }
        if(Random.value < spawnFlyingChance) {
            Instantiate(obstacleFly).transform.position += Random.value * flyingObstacleYOffset * Vector3.up;
        }
    }
}
