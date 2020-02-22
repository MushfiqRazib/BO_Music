using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace HawarIT.WebControls
{
	/// <summary>
	/// This FinanceControl has all the attributes of FieldControl and the following attributes
	/// ----------------------------------------------------------------------------------
	/// 1. FinValue	--> Returns the Decimal value of this control.
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
	///	3. Round    --> Returns and set the Round value of this field if round is true
	/// </summary>
	public class FinanceControl : BaseFieldControl
	{
		
		private double _finValue;		
		private bool _round;
		private string _format;

		/// <summary>
		/// Boolean Value for rounding
		/// </summary>
		[Category("Display"),Browsable(true),Bindable(true)]
		[Description("Boolean Value for rounding")]	
		public bool Round
		{
			get 
			{ 
				
				return _round; 
			}
			set 
			{ 
				
				_round = value; 
			}
		}

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

//		protected override void OnTextChanged(EventArgs e)
//		{
//			Page.Response.Write(this.Text);
//			base.OnTextChanged (e);
//		}

		/// <summary>
		/// This routine Dynamically generates Regular Expression of both
		/// Integer and Decimal with negetive value for different format.
		/// It Generates two Javascript routines
		///	One of the routine can check 
		///		1. whether this control contain decimal or not
		///			if decimal then it returns true
		///			otherwise false.
		///		2. Whether the format String content any non-integer charecter or not
		///			If type any Integer, -ve or Culturewise decimal seperator then it returns true
		///			otherwise false.
		///		3. If Round = true then it helps to write the round value
		///	and the another routine locks keyboard so that user can type only 
		///	[0-9], - and decimal seperator based on culture. 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e) 
		{
			char[] DecimalSeparator = curci.NumberFormat.CurrencyDecimalSeparator.ToString().ToCharArray();
			int CurrentSeparator = (int)DecimalSeparator[0];
			
			char[] SymbolSeparator = curci.NumberFormat.CurrencySymbol.ToString().ToCharArray();
			int CurrentSymbol = (int)SymbolSeparator[0];
			
			string Symbol = SymbolSeparator[0].ToString().Replace(SymbolSeparator.ToString(),@"\"+SymbolSeparator);
		
			string fmt;				//format string
			if(_format=="" || _format==null)
			{
				fmt="15,5";			//Default format value
			}
			else
			{
				fmt = _format;
			}

			string str = @"
			<script language='javascript' type='text/javascript'>
			<!--

			function "+this.ID+@"_CheckFinance(decimal) 
			{
";
			string[] arrFmt = fmt.Split(',');
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

			/// if the attribute Round is true then it replaces the round value in this textbox
				if(_round)
				{
					str+=@"
					var x = decimal.value;
					x = x.replace('"+DecimalSeparator[0]+@"','.');
					decimal.value = Math.round(x*Math.pow(10,limit))/Math.pow(10,limit);
					x= decimal.value;
					x=decimal.value= x.replace('.','"+DecimalSeparator[0]+@"');
";
				}
				else
				{
					str+=@"
				var x = decimal.value;";
				}
				str +=@"

				if(x.match(integerRE)){
					if(limit!=0){
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
					alert( 'Finance not valid\nFormat = {"+fmt+@"}' );
					decimal.style.color = 'red';  //if doesn't match with the regular expression
				}
				return false;
			}


			function "+this.ID+@"_AllowOnlyFinance(evt,decimal)
			{
				decimal.style.color = 'black';
				evt = (evt) ? evt : window.event;
				var charCode = (evt.which)?evt.which:evt.keyCode

				if(charCode != "+CurrentSeparator+@" && charCode != 45 && charCode > 31 && (charCode < 48 || charCode > 57))
				{
					status = 'Only (0-9),(-) and ("+DecimalSeparator[0]+@")';
					return false;
				}

				status = '';
				return true;
			}

			//-->
			</script>";

			if(!Page.IsClientScriptBlockRegistered(this.ID))
			{
				Page.RegisterClientScriptBlock(this.ID,str);	//Register this script
			}
			this.Attributes["OnKeyPress"] = "return "+this.ID + @"_AllowOnlyFinance(event,this)";
			this.Attributes["OnChange"] = "return "+this.ID + @"_CheckFinance(this)";
			this.Attributes["OnBlur"] = "this.style.color='Black'";
		}

		protected override void Render(HtmlTextWriter w) 
		{
			if (_value != null)
			{
				_finValue = double.Parse(_value.ToString());
				this.Text = _finValue.ToString("F2",curci);
				//this.Text = _value.ToString();
				//this.Text = this.Text.Replace(".",curci.NumberFormat.NumberDecimalSeparator);
			}
			// show the currency symbol based on locale
			w.Write(curci.NumberFormat.CurrencySymbol + " " );  //Render the culture specific Financial symbol

			base.Attributes["style"] = "text-align:right";		//As numeric value align right
			
			base.Render(w);										//Render this control

			if(!IsInDesignMode())
			{
				if(Required == true && this.Text.Trim().Equals(""))
				{
					req.RenderControl(w);						//Render Required Field Validator with this control
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
