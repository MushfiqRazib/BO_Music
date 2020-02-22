using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.Globalization;

namespace HawarIT.WebControls
{
	/// <summary>
	/// This is a Common control which is inherited by
	/// Integer control, Decimal Control, Date Control and Financial control
	/// So, this is the parent control of the controls mentioned above.
	/// 
	/// This control Inherits simply a webcontrol TextBox
	/// and added some extra common properties.
	/// </summary>
	public abstract class BaseFieldControl : System.Web.UI.WebControls.TextBox, FieldControl
	{
		
		/// <summary>
		/// Boolean which defines if a certain field is required or not
		/// </summary>
		protected bool _required = false;

		/// <summary>
		/// Set/gets the value of the texbox as an object so it 
		/// can be transfered into a database directly
		/// </summary>
		protected object _value;

		/// <summary>
		/// Boolean which defines if pasting is enabled in a certain field
		/// </summary>
		///protected bool _pasteEnable = true;


		/// <summary>
		/// For Validating Required
		/// </summary>
		protected RequiredFieldValidator req;
		protected CustomValidator customValidator;
		protected RegularExpressionValidator RegExpValidate;
		protected RangeValidator rangeValidator;
		protected ArrayList cbCultures = new  ArrayList();
		protected CultureInfo curci = new CultureInfo(CultureInfo.CurrentCulture.Name);
		
		public BaseFieldControl()
		{
			//Page.Response.Write(curci.Name);
		}
		/// <summary>
		/// Get/Set the value of ConnectionString
		/// </summary>
		protected string _connectionstring = null;
		[	
		Category("Databinding"), Browsable(true)
		]
		public string Connectionstring
		{
			get
			{
				return _connectionstring;
			}
			set 
			{
				_connectionstring = value;
			}
		}

		/// <summary>
		/// Get/Set whether this field is required or not!
		/// </summary>
		[Category("Display"),Browsable(true),Bindable(true),DefaultValue(false)]
		[Description("Get/Set whether value is required or not!")]	
		public bool Required
		{
			get 
			{ 
				
				return _required; 
			}
			set 
			{ 
				
				_required = value; 
			}
		}

		/// <summary>
		/// Get/Set whether this field is read only or not!
		/// </summary>
//		[Category("Display"),Browsable(true),Bindable(true),DefaultValue(false)]
//		[Description("Get/Set whether this field is read only or not!")]	
//		public override bool ReadOnly
//		{
//			get 
//			{ 
//				
//				return base.ReadOnly; 
//			}
//			set 
//			{ 
//				
//				base.ReadOnly = value; 
//			}
//		}

		/// <summary>
		/// Get/Set the length of this field
		/// i.e. How many digit/charecter it can accept
		/// </summary>
		[Category("Display"),Browsable(true),Bindable(true)]
		[Description("Get/Set the length of this field")]	
		public int Length
		{
			get 
			{ 
				
				return this.MaxLength; 
			}
			set 
			{ 
				
				this.MaxLength = value; 
			}
		}

		/// <summary>
		/// Sets/Gets the value of this field (TextBox)
		/// </summary>
		[Category("Display"),Browsable(false),Bindable(true)]
		[Description("Sets/Gets the value of a textbox")]	
		public object Value
		{
			get 
			{ 
				
				return _value; 
			}
			set 
			{ 
				
				_value = value;
			}
		}

		/// <summary>
		/// Sets/Gets the parent Id
		/// </summary>
		protected string _parentID = "";
		[Category("Behaviour"),	Browsable(true)]
		[Description("Sets/Gets the parent Id")]	
		public string Parentid 
		{
			get
			{
				return _parentID;
			}
			set 
			{
				_parentID = value;
			}
		}
		
		protected override void OnPreRender(EventArgs e)
		{
			if(!IsInDesignMode())
			{	//if the field is required
				//then add the Required Field Validator with this field
				if(Required == true)
				{
					req = new RequiredFieldValidator();
					req.ControlToValidate = this.ID;
					req.ErrorMessage = "Value Required";
					Controls.Add(req);
				}
			}
			base.OnPreRender (e);
		}
	
		/// <summary>
		/// See whether we are in design mode or runtime.
		/// </summary>
		/// <returns></returns>
		protected bool IsInDesignMode()
		{
			if (this.Site != null) 
				return this.Site.DesignMode; 
			return false;
		}
		public System.Web.UI.Control getControl() 
		{
			return this;
		}
	}
}
