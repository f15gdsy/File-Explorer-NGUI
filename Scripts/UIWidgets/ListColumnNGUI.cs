using UnityEngine;
using System.Collections;

namespace FileExplorerNGUI.UI {

	public class ListColumnNGUI : MonoBehaviour {

		public UILabel columnName;
		public UIBasicSprite icon;
		public UIBasicSprite backgroundImage;
		public UIButton button;
		
		public static float indentPerLevel;
		private int _indentLevel;
		
		protected bool _active = false;
		
		private Transform _trans;
		
		
		public Color color {
			get {return backgroundImage.color;}
			set {backgroundImage.color = value;}
		}
		
		public string text {
			get {return columnName.text;}
			set {columnName.text = value;}
		}
		
		public float localPositionY {
			get {
				return _trans.localPosition.y;
			}
		}
		
		public virtual int indentLevel {
			get {return _indentLevel;} 
			set {
				_indentLevel = value;
				
				float indent = _indentLevel * indentPerLevel;
				Transform iconTrans = icon.transform;
				Transform textTrans = columnName.transform;
				
				Vector3 iconLocalPos = iconTrans.localPosition;
				iconLocalPos.x += indent;
				iconTrans.localPosition = iconLocalPos;
				
				Vector3 textLocalPos = textTrans.localPosition;
				textLocalPos.x += indent;
				textTrans.localPosition = textLocalPos;
			}
		}
		
		public string path {get; set;}
		
		public ListColumnNGUI parent {get; set;}
		
		public ListWindowNGUI window {get; set;}
		
		public Transform trans {get {return _trans;}}
		
		
		void Awake () {
			_trans = GetComponent<Transform>();
		}
		
		void Start () {
			button.onClick.Add(new EventDelegate(ToggleActive));
		}
		
		public void ToggleActive () {
			window.highlightColumn = this;
			
			if (_active) {
				OnInactive();
			}
			else {
				OnActive();
			}
			
			_active = !_active;
		}
		
		// --- Not used ---
		protected virtual void OnActive () {}
		protected virtual void OnInactive () {}
		// 
		
		public override string ToString () {
			return string.Format ("[ListColumn: path={0}, height={1}, indentLevel={2}]", path, localPositionY, indentLevel);
		}
	}

}
