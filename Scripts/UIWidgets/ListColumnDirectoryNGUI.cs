using UnityEngine;
using System.Collections;

namespace FileExplorerNGUI.UI {

	public class ListColumnDirectoryNGUI : ListColumnNGUI {

		public Transform indicatorTrans;
		
		
		
		
		public override int indentLevel {
			get {
				return base.indentLevel;
			}
			set {
				base.indentLevel = value;
				
				float indent = indentLevel * indentPerLevel;
				Vector3 indicatorLocalPos = indicatorTrans.localPosition;
				indicatorLocalPos.x += indent;
				indicatorTrans.localPosition = indicatorLocalPos;
			}
		}
		
		protected override void OnActive () {
			window.CreateColumns(this);
			PlayIndicatorAnimationOpen();
			
			window.highlightColumn = null;
		}
		
		protected override void OnInactive () {
			window.DeleteColumns(this);
			PlayIndicatorAnimationClose();
		}
		
		private void PlayIndicatorAnimationOpen () {
			indicatorTrans.localEulerAngles = new Vector3(0, 0, -90);
		}
		
		private void PlayIndicatorAnimationClose () {
			indicatorTrans.localEulerAngles = new Vector3(0, 0, 0);
		}
	}

}