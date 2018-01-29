using UnityEngine;

public class EndLevelTrigger : MonoBehaviour {

    public GameManager gameManager;

    void OnTriggerEnter ()
    {
        ///Andreas: Will add function to test for Player only, in case anything else triggers it...
        gameManager.CompleteLevel();
    }

}
