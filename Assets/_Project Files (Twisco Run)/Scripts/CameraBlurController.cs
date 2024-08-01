using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlurController : MonoBehaviour
{
	private void OnEnable()
	{
        CharacterInputController.instance.IsInputEnalbe = false;
    }
	public void LoadingEndEvent() 
    {
        ApplicationState.isLoadingAfterTutorial = false;
        //this.transform.parent.gameObject.SetActive(false);
    }
	private void OnDisable()
	{
		HelperUtil.CallAfterDelay(() => 
		{
            CharacterInputController.instance.IsInputEnalbe = true;
        }, 1f);
	}
}
