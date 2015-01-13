﻿using UnityEngine;
using System.Collections;

namespace FileExplorerNGUI.UI {

	public class ListColumnFileNGUI : ListColumnNGUI {

		public class ListColumnFile : ListColumnNGUI {
			private float _previousClickTime;
			
			
			protected override void OnActive () {
				_previousClickTime = Time.time;
			}
			
			protected override void OnInactive () {
				if (Time.time - _previousClickTime < 0.25f) {
					LoadFile();
				}
			}
			
			private void LoadFile () {
				// TODO: 
				// 1. Check file format
				// 2.1 Load if valid
				// 2.2 Alert if not valid
			}
		}
	}

}