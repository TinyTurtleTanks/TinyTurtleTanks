using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GameSettings : MonoBehaviour {
    public static GameSettings Instance;
    public bool isPaused = false;
    public bool useVFX = false;
    public bool useSound = false;
    [HideInInspector]
    public bool useParticle = false;
    [Range(0, 5)]
    public float particleSlider = 1.0f;
    public bool useGrass = false;
    public bool useSeaweed = false;
    public bool useFootPrints = false;
    public bool useClouds = false;
    public bool useMoons = false;
    public bool useAtmosphere = false;
    public bool useWater = false;
    public bool daylightCycle = false;
    [Range(0, 1)]
    public float soundVolume = 1.0f;

    [Header("Test Settings")]
    public bool useCrates = false;
    public bool useEnvironmentObjects = false;
    public bool useEnemies = false;
    public int currentLevel = 1;

    void Awake() {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this) {
            Destroy(gameObject);
        }

        if (particleSlider == 0) {
            useParticle = false;
        }
    }

    public void SetParticleValues(ParticleSystem ps) {
        if (ps != null) {
            var main = ps.main;
            var emissionModule = ps.emission;

            int oldMaxParticles = main.maxParticles;
            float oldRateOverTime = emissionModule.rateOverTime.constant;
            Burst oldBurst = emissionModule.GetBurst(0);

            emissionModule = ps.emission;
            emissionModule.rateOverTime = new MinMaxCurve(oldRateOverTime * particleSlider);

            emissionModule.SetBurst(0, new Burst(0, oldBurst.count.constant * particleSlider));

            main.maxParticles = (int)(oldMaxParticles * particleSlider);
        }
    }
}