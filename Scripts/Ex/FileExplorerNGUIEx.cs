using UnityEngine;
using System.Collections;
using FileExplorerNGUI.UI;

namespace FileExplorerNGUI.Ex {
	
	// FileExplorerNGUIEx provides interfaces for other applications to use.
	public class FileExplorerNGUIEx {
		
		private static GameObject _windowPrefab;
		private static GameObject _windowGo;
		private static GameObject _rootGo;


		public static bool hidden {
			set {
				if (_windowGo != null) {
					_windowGo.SetActive(!value);
				}
			}
		}

		
		public static void Open (WindowControllerNGUI controller, string prefabPath) {
			if (_rootGo == null) {
				UIRoot root = GameObject.FindObjectOfType<UIRoot>() as UIRoot;
				if (root != null) {
					_rootGo = root.gameObject;
				}
			}
			if (_windowPrefab == null) {
				_windowPrefab = Resources.Load(prefabPath) as GameObject;
			}

//			_windowGo = GameObject.Instantiate(_windowPrefab) as GameObject;
//			_windowGo.transform.localPosition = Vector3.zero;		// TODO: leave an interface for positioning the window?
//
//			if (_rootGo != null) {
//				NGUITools.add
//			}
			if (_rootGo != null) {
				_windowGo = NGUITools.AddChild(_rootGo, _windowPrefab);
				_windowGo.transform.localPosition = Vector3.zero;
				Utilities.SetLayerRecursively(_windowGo, _rootGo.layer);
			}

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
			if (_windowGo != null) {
				GameObject.Destroy(_windowGo);
			}
		}

		public static bool CheckWindowOpened () {
			return _windowGo != null;
		}
	}
	
	
	public enum WindowStyle {
		Default = 0,
		List = 1,
	}
}