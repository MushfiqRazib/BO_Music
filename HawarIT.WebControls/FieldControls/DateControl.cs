using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Globalization;

namespace HawarIT.WebControls
{

	#region Enumerations

	/// <summary>
	/// The date format string
	/// </summary>
	public enum format
	{
		None,
		dd_MM_yyyy,
		MM_dd_yyyy
	}

	#endregion

	/// <summary>
	/// This DateControl has all the attributes of FieldControl and the following attributes
	/// ----------------------------------------------------------------------------------
	/// 1. Format: Get/set the format of the decimal.
	///				There are various patterns of this format as follows:
	///				{none}	-->	When no format (Default format)
	///							for this case the format is Short Date Pattern according to the culture
	///				{dd_MM_yyyy} --> two digit day, two digit month and 4 digit year
	///				{MM_dd_yyyy} --> two digit month, two digit day and 4 digit year
	///							if any valid incompletion then it auto-correct
	/// </summary>
	public class DateControl : BaseFieldControl
	{
		private DateTime _datValue;
		private format	_format;

		/// <summary>
		/// Set/Get the Format String
		/// </summary>
		[Browsable(true)]
		[Category("Display")]
		[Description("Set/Get the Format String")]		
		public format Format
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
		/// This routine helps to generate Regular Expression dynamically
		/// according to the valid day, month and year expression
		/// </summary>
		/// <param name="str">pattern</param>
		/// <returns>Regular Expression according to the pattern</returns>
		private string MakeRegularExpression(string str)
		{
			string mystr = "";
			if(str == "d")					// 1,2,3 pattern day expression
			{
				mystr += "([1-9]|[12][0-9]|3[01])";
			}
			if(str == "dd")					// 01, 02, 03 pattern day expression
			{
				mystr += "(0[1-9]|[12][0-9]|3[01])";
			}
			else if(str == "MM")			// 1,2,3 pattern month expression
			{
				mystr += "(0[1-9]|1[012])";
			}
			else if(str == "M")				// 01, 02, 03 pattern month expression
			{
				mystr += "([1-9]|1[012])";
			}
			else if(str == "yyyy")
			{
//				mystr += @"(19|20)\d\d";	//year value for 19th or 20th century
				mystr += @"\d{1,4}";		//year value upto 9999
			}
			
			return mystr;
		}

		/// <summary>
		/// It Generates two Javascript routines
		///	One of the routine can check the date value with the regular expression
		///	if match then returns true otherwise false
		///	and the another routine locks keyboard so that user can type only 
		///	[0-9] and Date seperator based on culture. 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e) 
		{
			char[] DateSeparator = curci.DateTimeFormat.DateSeparator.ToString().ToCharArray(); //date seperator based on culture
			int CurrentSeparator = (int)DateSeparator[0]; //Integer equivalent of the date separator
			string []cDateFormat ;
			if(_format.ToString()=="None") //default format
			{
				cDateFormat = curci.DateTimeFormat.ShortDatePattern.ToString().Split(DateSeparator[0]);
			}
			else
			{
				cDateFormat = _format.ToString().Split('_');		//split up the date pattern according to the date separator (formatted)
			}
			string CurrentDateFormat = cDateFormat[0] + DateSeparator[0] + cDateFormat[1] + 
				DateSeparator[0] + cDateFormat[2];				//Complete Date format according to the culture
			string []breakdate = CurrentDateFormat.Split(DateSeparator[0]); //split up the date pattern according to the date separator
			string expression = "";
			for(int i = 0 ; i < breakdate.Length ; i++)
			{
				if(i == breakdate.Length-1)
				{
					expression += MakeRegularExpression(breakdate[i]) ;
				}
				else
				{
					expression += MakeRegularExpression(breakdate[i])  + DateSeparator[0];
				}
			}
			expression = expression.Replace("/",@"\/");  //Complete Regular Expression according to the date format
					
			string str = @"
					<script language='javascript' type='text/javascript'>
					<!--
				
					function "+this.ID+@"_CheckDate(decimal) 
					{
						var decimalRE = /^" + expression + @"$/;" + @"
						var x = decimal.value;
						var dtDate = x.split('"+DateSeparator[0]+@"');
						if(dtDate.length==3)
						{";
			if (CurrentDateFormat=="dd/MM/yyyy" || CurrentDateFormat=="MM/dd/yyyy" ||
				CurrentDateFormat=="dd-MM-yyyy" || CurrentDateFormat=="MM-dd-yyyy")
			{
				str +=@"
							if(dtDate[0] !=null && dtDate[0].length==1)
								dtDate[0]='0'+dtDate[0];				//make two digit (0 padded)
							if(dtDate[1] !=null && dtDate[1].length==1)
								dtDate[1] = '0' + dtDate[1];				//make two digit (0 padded)
";
			}
			str +=@"
							if(dtDate[2].length == 1)
								dtDate[2] = '"+DateTime.Now.Year.ToString().Substring(0,3)+@"'+dtDate[2];
							else if(dtDate[2].length == 2)			
								dtDate[2] = '"+DateTime.Now.Year.ToString().Substring(0,2)+@"'+dtDate[2];
							else if(dtDate[2].length == 3)
								dtDate[2] = '"+DateTime.Now.Year.ToString().Substring(0,1)+@"'+dtDate[2];
							x = decimal.value = dtDate[0]+'" + DateSeparator[0] + @"'+ dtDate[1]+'" + 
								DateSeparator[0] + @"'+dtDate[2];			//corrected date value
						}
						if (x.match(decimalRE)) //if value mached with The Regular Expression
						{
							return true;
						}
						else 
						{
							if(x == '')
							{	
								return true;
							}
							alert( 'DATE not valid! Format - " + CurrentDateFormat + @"');
							return false;
						}
					}

					function "+this.ID+@"_AllowOnlyDate(evt)
					{
						evt = (evt) ? evt : window.event;
						var charCode = (evt.which)?evt.which:evt.keyCode
						if(charCode != " + CurrentSeparator + @" && charCode > 31 && (charCode < 48 || charCode > 57))
						{
							status = 'Only numeric and "+DateSeparator[0]+@"';
							return false;
						}
						status = '';
						return true;
					}

					//-->
					</script>";


			if(!Page.IsClientScriptBlockRegistered(this.ID))
			{
				Page.RegisterClientScriptBlock(this.ID,str);			//Register this script
			}
			this.Attributes["OnKeyPress"] = "return "+this.ID+@"_AllowOnlyDate(event)";
			this.Attributes["OnChange"] = "return "+this.ID+@"_CheckDate(this)";

		}

		protected override void Render(HtmlTextWriter w) 
		{
			if (_value != null)
			{
				_datValue = (DateTime) _value;
				this.Text =  _datValue.ToString("d",curci);
			}
			base.Render(w);
			if(!IsInDesignMode())
			{
				if(Required == true && this.Text.Trim().Equals(""))
				{
					req.RenderControl(w);
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
