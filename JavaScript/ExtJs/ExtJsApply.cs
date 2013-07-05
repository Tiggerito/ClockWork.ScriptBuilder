using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWork.ScriptBuilder.JavaScript.ExtJs
{
	/// <summary>
	/// render the Ext.apply statement:
	/// Ext.apply(receiver, config);
	/// Ext.apply(receiver, config, defaults);
	/// </summary>
	public class ExtJsApply : ScriptItem
	{
		#region Constructors
		/// <summary>
		/// render the Ext.apply statement:
		/// Ext.apply(this, config);
		/// </summary>
		/// <param name="config"></param>
		public ExtJsApply(JsObject config)
		{
			_Config = config;
		}

		/// <summary>
		/// render the Ext.apply statement:
		/// Ext.apply(receiver, config);
		/// </summary>
		/// <param name="config"></param>
		/// <param name="reciever"></param>
        public ExtJsApply(object reciever, JsObject config)
		{
			Receiver = reciever;
			Config = config;
		}

		/// <summary>
		/// render the Ext.apply statement:
		/// Ext.apply(this, config, defaults);
		/// </summary>
		/// <param name="config"></param>
		/// <param name="defaults"></param>
		public ExtJsApply(JsObject config, JsObject defaults)
		{
			_Config = config;
			Defaults = defaults;
		}

		/// <summary>
		/// render the Ext.apply statement:
		/// Ext.apply(receiver, config, defaults);
		/// </summary>
		/// <param name="config"></param>
		/// <param name="defaults"></param>
		/// <param name="reciever"></param>
        public ExtJsApply(object reciever, JsObject config, JsObject defaults)
		{
			Receiver = reciever;
			Config = config;
			Defaults = defaults;
		}

		#endregion

		#region Data
        private object _Receiver = "this";
		/// <summary>
		/// The object to have the config applied to
		/// defaults to this
		/// </summary>
		public object Receiver
		{
			get 
			{ 
				return _Receiver; 
			}
			set { _Receiver = value; }
		}

		private JsObject _Config;
		/// <summary>
		/// A configuration object that contains the property values to apply to the reciever
		/// </summary>
		public JsObject Config
		{
			get 
			{
				if (_Config==null)
					_Config = Js.Object();
				return _Config; 
			}
			set { _Config = value; }
		}

		private JsObject _Defaults;
		/// <summary>
		/// An optional defaults object
		/// </summary>
		public JsObject Defaults
		{
			get 
			{
				if (_Defaults==null)
					_Defaults = Js.Object();
				return _Defaults; 
			}
			set { _Defaults = value; }
		}



		#endregion


		#region Rendering
		/// <summary>
		/// render the apply statement
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRender(RenderingEventArgs e)
		{
			base.OnRender(e);

			IScriptWriter writer = e.Writer;



			if (HasRenderContent)
			{
				JsArguments arguments = Js.Arguments(Receiver, Config);

				if (this.Defaults.Count>0)
					arguments.Add(this.Defaults);

				writer.Write(Js.Statement(Js.Call("Ext.apply", arguments)));
			}
		}
		/// <summary>
		/// If either config or defaults have render content
		/// </summary>
		public override bool HasRenderContent
		{
			get
			{
				return Config.HasRenderContent || this.Defaults.HasRenderContent;
			}
		}
		#endregion

	}
}
