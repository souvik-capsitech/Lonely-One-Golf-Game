using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public enum TimeOfDay
    {
        Day,
        Evening,
        Night
    }

    public TimeOfDay levelTimeOfDay = TimeOfDay.Day; 

    private DayNightManager dayNightManager;

    void Start()
    {
        dayNightManager = FindFirstObjectByType<DayNightManager>();

        if (dayNightManager != null)
        {
            
            switch (levelTimeOfDay)
            {
                case TimeOfDay.Day:
                    dayNightManager.SetTimeOfDay("day");
                    break;
                case TimeOfDay.Evening:
                    dayNightManager.SetTimeOfDay("evening");
                    break;
                case TimeOfDay.Night:
                    dayNightManager.SetTimeOfDay("night");
                    break;
            }
        }
    }
}
