using UnityEngine;
using System.Collections;

public class ScreenResizeCapturer : MonoBehaviour {

	private int lastWidth;
	private int lastHeight;
	private bool alive = true;

	public delegate void ScreenResizeEventHandler(object sender, int screenWidth, int screenHeight);
	public event ScreenResizeEventHandler ScreenResizeEvent;
	
	void Start(){
		#if (UNITY_EDITOR_WIN || UNITY_EDITOR_WIN)
		StartCoroutine( check_for_resize() );
		#endif
	}
	
	IEnumerator check_for_resize(){
		lastWidth = Screen.width;
		lastHeight = Screen.height;
		
		while( alive ){
			if( lastWidth != Screen.width || lastHeight != Screen.height ){
				lastWidth = Screen.width;
				lastHeight = Screen.height;
				if(ScreenResizeEvent != null){
					ScreenResizeEvent(this, lastWidth, lastHeight);
				}
			}
			yield return new WaitForEndOfFrame();
		}
	}
	
	void OnDestroy(){
		alive = false;
	}
}
