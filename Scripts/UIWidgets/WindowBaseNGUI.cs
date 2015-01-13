using UnityEngine;
using System.Collections;
using FileExplorerNGUI.Ex;

namespace FileExplorerNGUI.UI {

	public class WindowBaseNGUI : MonoBehaviour {

		// ----- UI elements that are required -----
		public UIButton cancelButton;
		public UIButton otherButton;
		
		protected WindowControllerNGUI _controller;
		
		protected string _activeFilePath;
		
		
		public string activeFilePath {get {return _activeFilePath;}}
		
		
		protected virtual void Start () {
			_controller.OnStart();
		}
		
		public void RegisterWindowController (WindowControllerNGUI controller) {
			_controller = controller;
			_controller.window = this;
			
			_controller.RegisterCancelButton(cancelButton);
			_controller.RegisterOtherButton(otherButton);
		}
	}

}