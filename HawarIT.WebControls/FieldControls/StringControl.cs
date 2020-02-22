using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using HawarIT.WebControls;

namespace HawarIT.WebControls
{
	/// <summary>
	/// Summary description for IntegerControl.
	/// </summary>
	public class StringControl : BaseFieldControl
	{
		/// <summary>
		/// Rendering the control
		/// </summary>
		/// <param name="w">HtmlTextWriter</param>
		protected override void Render(HtmlTextWriter w) 
		{
			if (_value != null)
			{
				this.Text = _value.ToString();
			}
			//in case of length exceed 150 charecter then the TextMode will be Multiline
			//otherwise single line
			if (this.MaxLength > 150 || this.MaxLength == -1 )
			{
				this.Height = Unit.Parse("100px");				
				this.TextMode = System.Web.UI.WebControls.TextBoxMode.MultiLine;
			}

			base.Render(w);
			
			// For Required Property
			if(Required == true )
			{
				if(!IsInDesignMode())
				{
					req.RenderControl(w);
				}
				else
				{
					w.Write("<font color=red>Value Required!</font>");
				}
			}
		}
	}
}
