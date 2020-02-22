using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Globalization;

namespace HawarIT.WebControls
{
	// IPostBackDataHandler from TextBox
	//
	public interface FieldControl
	{		
		string Connectionstring
		{
			get;
			set;
		}
		bool Required
		{
			get;
			set;
		}
		bool ReadOnly
		{
			get;
			set;
		}
		int Length
		{
			get;
			set;
		}
		object Value
		{
			get;
			set;
		}
		string Parentid 
		{
			get;
			set;
		}
		string ID
		{
			get;
			set;
		}
		string Text
		{
			get;
			set;
		}
		System.Web.UI.Control getControl();
	}
}
