using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Ocean_game_handler : MonoBehaviour
{
    [Header("Building Settings")]
    // Add the tags of your 4 buildings here in the Inspector
    public List<string> targetBuildingTags; 
    public string victorySceneName = "EndWin";

    // Track which unique tags have been visited
    private HashSet<string> visitedBuildings = new HashSet<string>();

    public void CheckBuilding(string tag)
    {
        if (targetBuildingTags.Contains(tag))
        {
            if (!visitedBuildings.Contains(tag))
            {
                visitedBuildings.Add(tag);
                Debug.Log("Visited: " + tag + ". Progress: " + visitedBuildings.Count + "/" + targetBuildingTags.Count);

                if (visitedBuildings.Count >= targetBuildingTags.Count)
                {
                    SceneManager.LoadScene(victorySceneName);
                }
            }
        }
    }
}
