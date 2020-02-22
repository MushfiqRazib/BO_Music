using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text.RegularExpressions;


namespace HawarIT.WebControls
{
	/// <summary>
	/// This DecimalControl has all the attributes of FieldControl and the following attributes
	/// ----------------------------------------------------------------------------------
	/// 1. decValue	--> Returns the Decimal value of this control.
	/// 2. Format	--> Get/set the format of the decimal.
	///					There are various patterns of this format as follows:
	///					{a,b}	-->	where 'a' is the number of digit before decimal.
	///								and 'b' is number of digit after decimal.
	///					{a}		--> Behaves as an Integer control contains maximum 'a' digit.
	///					{a,}	--> same as {a}.
	///					{,b}	--> There is no binding on number of digit before decimal
	///								but contains maximum 'b' digit after decimal.
	///					{empty}	--> No limitation.
	///					{,}		--> Invalid format.
	///					If format is not given, then {15,5} is the default format.
	/// </summary>
	public class DecimalControl : BaseFieldControl
	{

		private string _format;
		private double _decValue;

		/// <summary>
		/// Get/set the format of the decimal
		/// </summary>
		[Category("Display"),Browsable(true),Bindable(true)]
		[Description("Get/set the format of the decimal")]	
		public string Format
		{
			get 
			{ 
				
				return _format; 
			}
			set 
			{ 
				
				_format = value; 
			}
		}

		/// <summary>
		/// This routine Dynamically generates Regular Expression of both
		/// Integer and Decimal with negetive value for different format.
		/// It Generates two Javascript routines
		///	One of the routine can check 
		///		1. whether this control contain decimal or not
		///			if decimal then it returns true
		///			otherwise false.
		///		2. If the format String content any non-integer value then it returns true
		///			otherwise false.
		///	and the another routine locks keyboard so that user can type only 
		///	[0-9], - and decimal seperator based on culture. 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e) 
		{
			char[] DecimalSeparator = curci.NumberFormat.CurrencyDecimalSeparator.ToString().ToCharArray();
			int CurrentSeparator = (int)DecimalSeparator[0];
			string fmt;							//local format string

			//if format string is absent then default format is 15,5
			if(_format=="" || _format==null)
			{
				fmt="15,5";
			}
			else
			{
				fmt = _format;
			}

			//string JavaScript code written using C# and JavaScript
			string str = @"
					<script language='javascript' type='text/javascript'>
					<!--
					
					function "+this.ID+@"_CheckDecimal(decimal) 
					{
";
						string[] arrFmt = fmt.Split(',');				//Split up the format string Against ','
						if(arrFmt.Length==1 && IsNumeric(arrFmt[0]))	//when format {a} and a is numeric
						{
							str += @"
					var limit=0;
					var integerRE = /^(\+|-)?(\d){1,"+arrFmt[0]+@"}$/;
					var decimalRE = /^(\+|-)?(\d){1,"+arrFmt[0]+@"}(\"+DecimalSeparator[0]+@")(\d){0,0}$/;
";
						}
						else if(arrFmt.Length==2)
						{
							if(IsNumeric(arrFmt[0]) && IsNumeric(arrFmt[1]))	//when format {a,b}
							{
								str += @"
					var limit="+arrFmt[1]+@";
					var integerRE = /^(\+|-)?(\d){1,"+arrFmt[0]+@"}$/;
					var decimalRE = /^(\+|-)?(\d){1,"+arrFmt[0]+@"}(\"+DecimalSeparator[0]+@")(\d){0,"+arrFmt[1]+@"}$/;";
							}
							else if(IsNumeric(arrFmt[0]) && arrFmt[1]=="")		//when format {a,}
							{
								arrFmt[1]="0";
								str += @"
					var limit="+arrFmt[1]+@";
					var integerRE = /^(\+|-)?(\d){1,"+arrFmt[0]+@"}$/;
					var decimalRE = /^(\+|-)?(\d){1,"+arrFmt[0]+@"}(\"+DecimalSeparator[0]+@")(\d){0,"+arrFmt[1]+@"}$/;
";
							}
							else if(arrFmt[0]=="" && IsNumeric(arrFmt[1]))		//when format {,b}
							{
								str += @"
					var limit="+arrFmt[1]+@";
					var integerRE = /^(\+|-)?[0-9][0-9]*$/;
					var decimalRE = /^(\+|-)?[0-9][0-9]*(\"+DecimalSeparator[0]+@")(\d){0,"+arrFmt[1]+@"}$/;
";
							}
							else	// when format {,}
							{
								str+=@"alert('Invalid format\nFormat != {"+fmt+@"}');return false";
							}
						}
						else		//otherwise
						{
							str+=@"alert('Invalid format\nFormat != {"+fmt+@"}');return false";
						}

					str+=@"
					var x = decimal.value;
					if(x.match(integerRE))
					{
						if(limit!=0)
						{
							decimal.value += '" + DecimalSeparator[0]+@"';					
							for(i=0;i<limit; i++)
								decimal.value +='0';
						}
					} 
					else if (x.match(decimalRE)) 
					{
						return true;
					}
					else 
					{
						if(x == '')
						{	
							return true;
						}
						alert( 'Decimal not valid\nFormat = {"+fmt+@"}' );
					}
				return false;
			}

			function "+ this.ID +@"_AllowOnlyDecimal(evt)
			{
				evt = (evt) ? evt : window.event;		//windows event
				var charCode = (evt.which)?evt.which:evt.keyCode

				if(charCode != "+CurrentSeparator+@" && charCode != 45 && charCode > 31 && (charCode < 48 || charCode > 57))
				{
					status = 'Only (0-9),(-) and ("+DecimalSeparator[0]+@")'; //the valid charecter set in the status bar
					return false;
				}

				status = '';
				return true;
			}

			//-->
			</script>";

			if(!Page.IsClientScriptBlockRegistered(this.ID))
			{
				Page.RegisterClientScriptBlock(this.ID,str);   //Regester this script
			}
			this.Attributes["OnKeyPress"] = "return "+this.ID+@"_AllowOnlyDecimal(event)";
			this.Attributes["OnChange"] = "return "+this.ID+@"_CheckDecimal(this)";
		}


		
		protected override void Render(HtmlTextWriter w) 
		{
			if (_value != null)
			{
				//this.Text = _value.ToString();
				//this.Text = this.Text.Replace(".",curci.NumberFormat.NumberDecimalSeparator);
				_decValue = double.Parse(_value.ToString());
				this.Text = _decValue.ToString("F2",curci);
			}
			base.Attributes["style"] = "text-align:right";			//As the numeric field Alignment Right
			base.Render(w);											// Render this control
			if(!IsInDesignMode())
			{
				if(Required == true && this.Text.Trim().Equals(""))
				{
					req.RenderControl(w);		//Render Required Field Validator with this control
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
		/// <summary>
		/// Check whether 'theValue' is Numeric or not.
		/// </summary>
		/// <param name="theValue">Stirng value</param>
		/// <returns>True/False</returns>
		private bool IsNumeric(string theValue)
		{
			Regex _isNumber = new Regex(@"^\d+$");
			Match m = _isNumber.Match(theValue);
			return m.Success;
		}

	}
}
