using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using HawarIT.WebControls;

namespace HawarIT.WebControls
{
	/// <summary>
	/// This IntegerControl has all the attributes of FieldControl and the following attributes
	/// ----------------------------------------------------------------------------------
	/// 1. intValue	--> Returns the Integer value of this control
	/// 2. MaxValue --> Returns the maximum value of this control
	/// 3. MinValue --> Returns the minimum value of this control
	/// </summary>
	public class IntegerControl : BaseFieldControl
	{
//		private int _intValue; //Returns the Integer value of this control

		/// <summary>
		/// Set/Get the Minimum acceptable Value
		/// </summary>
		[Category("Display"),Browsable(false),Bindable(true)]
		[Description("Set/Get the Minimum Value")]	
		protected int _minValue;
		public int Minvalue
		{
			get 
			{ 
				return _minValue; 
			}
			set 
			{ 
				_minValue = value;
			}
		}

		/// <summary>
		/// Set/Get the Maximum acceptable Value
		/// </summary>
		[Category("Display"),Browsable(false),Bindable(true)]
		[Description("Set/Get the Maximum Value")]	
		protected int _maxValue;
		public int Maxvalue
		{
			get 
			{ 
				
				return _maxValue; 
			}
			set 
			{ 
				_maxValue = value;
			}
		}

		protected override void OnInit(EventArgs e) 
		{
			string str = @"
			<script language='javascript' type='text/javascript'>
			<!--
				function "+this.ID+@"_CheckInteger(integer) 
				{
					var integerRE = /^(\+|-)?[0-9][0-9]*$/; //Regular Expression for Integer including '-' and '+'
					var x = integer.value;
					if (x.match(integerRE))		//if the Regular Expression match with the value
					{
						return true;
					}
					else 
					{
						if(x == '')		//if contain nothing
						{	
							return true;
						}
						alert( 'Integer not valid' );
					}
					return false;
				}


				function "+this.ID+@"_CheckNumber(evt)
				{
					evt = (evt) ? evt : window.event;
					var charCode = (evt.which)?evt.which:evt.keyCode
					//Allowable chrarecter set for this control
					if(charCode != 45 && charCode > 31 && (charCode < 48 || charCode > 57))
					{
						status = 'Only Numbers';		//Show this in the Status bar if Charecter set doesn't match
						return false;
					}
					status = '';
					return true;
				}
			//-->
			</script>";

			if(!Page.IsClientScriptBlockRegistered(this.ID))
			{
				Page.RegisterClientScriptBlock(this.ID,str);   //write this script
			}
			this.Attributes["OnKeyPress"]	= "return "+this.ID+@"_CheckNumber(event)";  // if key pressed
			this.Attributes["OnChange"]		= "return "+this.ID+@"_CheckInteger(this)";  // if any change in the text box when blur
		}
		

		/// <summary>
		/// This section add a range validator with this control
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			rangeValidator = new RangeValidator();
			rangeValidator.ErrorMessage			="Range: {"+_minValue+","+ _maxValue+"}";
			rangeValidator.MinimumValue			=_minValue.ToString();
			rangeValidator.MaximumValue			=_maxValue.ToString();
			rangeValidator.ControlToValidate	= this.ID;
			Controls.Add(rangeValidator);

			base.OnPreRender (e);
		}

		protected override void Render(HtmlTextWriter w) 
		{
			if (_value != null)
			{
				this.Text = _value.ToString();
			}

			base.Attributes["style"] = "text-align:right";
			base.Render(w);				//Render this control
	
			if(!IsInDesignMode())
			{
				if(Required == true )	//if this field is Required then render RequiredFieldValidator with this control
				{
					req.RenderControl(w);
				}
				if(_minValue  > _maxValue)	//If minimum value is higher than Maximum value
				{
					w.Write("<font color=red> Invalid range value.</font>");
					//Page.IsValid=false;
				}
				else if(_minValue !=0 && _maxValue!=0)
				{
					rangeValidator.RenderControl(w);	//render the range validator if minimum value and Maximum value both are 0
				} 
			}
			else
			{
				if(Required == true)
				{
					w.Write("<font color=red>Value Required!</font>");
				}
			}
		}
	}
}
