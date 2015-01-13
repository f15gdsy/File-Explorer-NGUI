using UnityEngine;
using System.Collections;
using FileExplorerNGUI.UI;

namespace FileExplorerNGUI.Ex {

	public class WindowControllerNGUI {

		public WindowBaseNGUI window {get;set;}
		
		
		public void RegisterCancelButton (UIButton cancelButton) {
			if (cancelButton == null) return ;

			cancelButton.onClick.Add(new EventDelegate(OnCancelButtonPressed));
		}
		
		public void RegisterOtherButton (UIButton otherButton) {
			if (otherButton == null) return ;

			otherButton.onClick.Add(new EventDelegate(OnOtherButtonPressed));
		}
		
		// Override these functions to customize.
		public virtual void OnStart () {}
		public virtual void OnFileHighlighted (string path) {}
		protected virtual void OnCancelButtonPressed () {
			FileExplorerExNGUI.Close();
		}
		protected virtual void OnOtherButtonPressed () {}
	}

}