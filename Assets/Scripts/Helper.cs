
using Leopotam.Ecs;
using LeopotamGroup.Globals;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zlodey {
    public class Helper
    {
        public static void LoadNextLevel()
        {
            var level = Progress.CurrentLevel;
            var configuration = Service<StaticData>.Get();
            
            var totalLevels = configuration.Levels.Scenes.Length;
            var index = level;
            if (level >= totalLevels)
            {
                index = level % totalLevels;
                index = configuration.Levels.StartScene + index % (totalLevels - configuration.Levels.StartScene);
            }
            
            var levelName = configuration.Levels.Scenes[index];
            SceneManager.LoadScene(levelName);
        }

        public static void LoadLevelOnBoot(Levels scenesData)
        {
            var totalLevels = scenesData.Scenes.Length;
            var currentLevel = Progress.CurrentLevel;
            var currentSound = Progress.CurentSound;
            var currentHaptic = Progress.CurentHaptic;
            if (currentLevel >= totalLevels)
            {
                currentLevel = currentLevel % totalLevels;
                currentLevel = scenesData.SkipLevels + currentLevel % (totalLevels - scenesData.SkipLevels);
            }
            SceneManager.LoadScene(scenesData[currentLevel]);
        }
    }
}
