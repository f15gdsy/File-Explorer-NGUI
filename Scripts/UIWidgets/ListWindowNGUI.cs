using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FileExplorerNGUI.UI {

	public class ListWindowNGUI : WindowBaseNGUI {
		
		public ListColumnNGUI fileColumnPrefab;
		public ListColumnNGUI directoryColumnPrefab;
		public Color columnColor1;
		public Color columnColor2;
		public Color columnHighlightColor;
		public UIScrollView scrollView;
		public float indent;
		
		private float _stepY;
		
		private List<ListColumnNGUI> _columns;
		private Dictionary<ListColumnNGUI, ColumnRange> _columnsToChildren;
		
		private ListColumnNGUI _highlightColumn;
		
		
		public ListColumnNGUI highlightColumn {
			get {return _highlightColumn;}
			set {
				if (_highlightColumn != null) {	// Reset previous highlighted column to correct color
					int previousActiveColumnIndex = _columns.IndexOf(_highlightColumn);
					_highlightColumn.color = ChooseColumnColor(previousActiveColumnIndex);
				}
				
				if (value != null) {
					_highlightColumn = value;
					_highlightColumn.color = columnHighlightColor;
					
					_controller.OnFileHighlighted(_highlightColumn.path);
					
					_activeFilePath = _highlightColumn.path;
				}
			}
		}
		
		
		protected override void Start () {
			_columns = new List<ListColumnNGUI>();
			_columnsToChildren = new Dictionary<ListColumnNGUI, ColumnRange>();
			
			ListColumnNGUI.indentPerLevel = indent;
			
			_stepY = fileColumnPrefab.GetComponent<UIWidget>().localSize.y;
			
			CreateColumns(null);
			
			base.Start();
		}
		
		public void CreateColumns (ListColumnNGUI parent) {
			float currentY = 0;
			int columnIndex = 0;
			int indentLevel = 0;
			string directoryPath = parent == null ? Utilities.GetUserRoot() : parent.path;
			
			List<FileSystemInfo> filesAndDirectories = Utilities.GetFilesInDirectory(directoryPath);
			
			if (parent != null) {
				currentY = parent.localPositionY - _stepY;
				columnIndex = _columns.IndexOf(parent) + 1;
				indentLevel = parent.indentLevel + 1;
				
				ColumnRange range = new ColumnRange();
				if (filesAndDirectories.Count > 0) {
					range.min = columnIndex;
					range.count = filesAndDirectories.Count;
				}
				else {
					range.min = -1;
				}
				
				if (range.CheckValid()) {
					_columnsToChildren.Add(parent, range);
				}
			}
			else {
				_columns.Clear();
			}
			
			foreach (FileSystemInfo fileOrDirectory in filesAndDirectories) {
				ListColumnNGUI column = null;
				
				if ((fileOrDirectory.Attributes & FileAttributes.Directory) == FileAttributes.Directory) {
					column = GameObject.Instantiate(directoryColumnPrefab) as ListColumnNGUI;
				}
				else {
					column = GameObject.Instantiate(fileColumnPrefab) as ListColumnNGUI;
				}

				column.name = fileOrDirectory.Name + " Column";
				column.text = fileOrDirectory.Name;
				column.path = fileOrDirectory.FullName;
				column.parent = parent;
				column.indentLevel = indentLevel;
				column.window = this;

				column.transform.SetParent(scrollView.transform);
				column.transform.localPosition = new Vector3 (0, currentY, 0);
				column.trans.localScale = new Vector3(1, 1, 1);
				
				currentY -= _stepY;
				
				_columns.Insert(columnIndex++, column);
			}
			
			for (int i=columnIndex; i<_columns.Count; i++) {
				_columns[i].transform.localPosition = new Vector3(0, currentY, 0);
				currentY -= _stepY;
			}
			
			SetColumnsColor();

//			Vector3 contentPos = contentPanel.transform.localPosition;
//			contentPanel.SetRect(contentPos.x, contentPos.y, contentPanel.GetViewSize().x, Mathf.Abs(currentY));

			scrollView.UpdateScrollbars();
		}
		
		// Delete parent column's children columns.
		public void DeleteColumns (ListColumnNGUI parent) {
			if (!_columnsToChildren.ContainsKey(parent)) return;
			
			ColumnRange range;
			if (!_columnsToChildren.TryGetValue(parent, out range)) return;
			
			if (!range.CheckValid()) return;
			
			for (int i=range.min; i<range.min + range.count; i++) {
				DeleteColumns(_columns[i]);
			}
			
			float currentY = _columns[range.min].localPositionY;
			
			List<ListColumnNGUI> columnsToRemove = new List<ListColumnNGUI>();
			for (int i=range.min; i<range.min + range.count; i++) {
				columnsToRemove.Add(_columns[i]);
			}
			_columns.RemoveRange(range.min, range.count);
			
			for (int i=columnsToRemove.Count-1; i>=0; i--) {
				Destroy(columnsToRemove[i].gameObject);
			}
			
			for (int i=range.min; i<_columns.Count; i++) {
				ListColumnNGUI column = _columns[i];
				column.transform.localPosition = new Vector3(0, currentY, 0);
				currentY -= _stepY;
			}
			
//			Vector3 contentPos = contentPanel.transform.localPosition;
//			contentPanel.SetRect(contentPos.x, contentPos.y, contentPanel.GetViewSize().x, Mathf.Abs(currentY));
			
			_columnsToChildren.Remove(parent);
			
			SetColumnsColor();

			scrollView.UpdateScrollbars();
		}
		
		private void SetColumnsColor () {
			for (int i=0; i<_columns.Count; i++) {
				ListColumnNGUI column = _columns[i];
				column.color = ChooseColumnColor(i);
			}
		}
		
		private Color ChooseColumnColor (int index) {
			return index % 2 == 0 ? columnColor1 : columnColor2;
		}
		
		
		private struct ColumnRange {
			public int min;
			public int count;
			
			public bool CheckValid () {
				return min >= 0;
			}
		}
	}

}

