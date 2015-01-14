using UnityEngine;
using System.Collections;
using FileExplorerNGUI.UI;

namespace FileExplorerNGUI.Ex {
	
	// FileExplorerNGUIEx provides interfaces for other applications to use.
	public class FileExplorerNGUIEx {
		
		private static GameObject _windowPrefab;
		private static GameObject _windowGo;
		
		
		public static void Open (WindowControllerNGUI controller, string prefabPath) {
			if (_windowPrefab == null) {
				_windowPrefab = Resources.Load(prefabPath) as GameObject;
			}

			_windowGo = GameObject.Instantiate(_windowPrefab) as GameObject;
			_windowGo.transform.localPosition = Vector3.zero;		// TODO: leave an interface for positioning the window?
			
			WindowBaseNGUI window = _windowGo.GetComponent<WindowBaseNGUI>();
			window.RegisterWindowController(controller);
		}
		
		// Used to open a File Explorer window.
		// 	controller: a customized controller to responds to window UI interaction.
		//	style: wanted window style.
		public static void Open (WindowControllerNGUI controller, WindowStyle style = WindowStyle.Default) {
			string prefabPath = "";

			if (_windowPrefab == null) {
				switch (style) {
				case WindowStyle.List:
					prefabPath = "Prefabs/File Explorer List Window NGUI";
					break;
					
				default:
					prefabPath = "Prefabs/File Explorer List Window NGUI";
					break;
				}
			}

			Open(controller, prefabPath);
		}
		
		// Close the window.
		public static void Close () {
			GameObject.Destroy(_windowGo);
		}
	}
	
	
	public enum WindowStyle {
		Default = 0,
		List = 1,
	}
}