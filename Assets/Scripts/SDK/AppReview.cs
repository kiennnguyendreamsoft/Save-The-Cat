using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Google.Play.Review;

public class AppReview : ManualSingleton<AppReview>
{
    // private ReviewManager _reviewManager;
    // private PlayReviewInfo _playReviewInfo;

    void Start()
    {
        // _reviewManager = new ReviewManager();
    }

    public void OpenRating(){
        if(PlayerPrefs.GetInt("RATED",0) == 1) return;
        // StartCoroutine(OpenReview());
    }
    // IEnumerator OpenReview(){
        // var requestFlowOperation = _reviewManager.RequestReviewFlow();
        // yield return requestFlowOperation;
        // if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        // {
        //     yield break;
        // }
        // _playReviewInfo = requestFlowOperation.GetResult();
        //
        // var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        // yield return launchFlowOperation;
        // _playReviewInfo = null; // Reset the object
        // if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        // {
        //     // Log error. For example, using requestFlowOperation.Error.ToString().
        //     yield break;
        // }
        // PlayerPrefs.SetInt("RATED",1);
    // }
}
