using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWork.ScriptBuilder.JavaScript.ExtJs
{
	/// <summary>
	/// Provides a quick way to create ExtJs Items
	/// This makes script building code more readable
	/// </summary>
	public class ExtJs
	{
		#region Apply
		/// <summary>
		/// render the Ext.apply statement:
		/// Ext.apply(this, config);
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public static ExtJsApply Apply(JsObject config)
		{
			return new ExtJsApply(config);
		}
		/// <summary>
		/// render the Ext.apply statement:
		/// Ext.apply(receiver, config);
		/// </summary>
		/// <param name="reciever"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static ExtJsApply Apply(object reciever, JsObject config)
		{
			return new ExtJsApply(reciever, config);
		}
		#endregion

		#region ApplyIf
		/// <summary>
		/// render the Ext.applyIf statement:
		/// Ext.applyIf(this, config);
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public static ExtJsApplyIf ApplyIf(JsObject config)
		{
			return new ExtJsApplyIf(config);
		}
		/// <summary>
		/// render the Ext.applyIf statement:
		/// Ext.applyIf(receiver, config);
		/// </summary>
		/// <param name="reciever"></param>
		/// <param name="config"></param>
		/// <returns></returns>
		public static ExtJsApplyIf ApplyIf(object reciever, JsObject config)
		{
			return new ExtJsApplyIf(reciever, config);
		}

		#endregion

		#region Component
		/// <summary>
		/// Renders a component structure
		/// place component properties and functions in the componentObject
		/// </summary>
		/// <param name="componentName">name to give the component</param>
		/// <param name="baseComponent">base component to extend</param>
		/// <param name="componentObject">the object that defines the component</param>
		/// <returns></returns>
        public static ExtJsComponent Component(object componentName, object baseComponent, JsObject componentObject)
		{
			return new ExtJsComponent(componentName, baseComponent, componentObject);
		}
		#endregion

		#region Class
		/// <summary>
		/// Script Item designed to help construct the class pattern often used in ExtJs
		/// aka pre-configured classes
		/// </summary>
		/// <param name="className">name to give the class</param>
		/// <param name="baseClass">base class to extend</param>
		/// <param name="parameters">constructor parameters</param>
		/// <param name="constructor">constructor script</param>
		/// <returns></returns>
        public static ExtJsClass Class(object className, object baseClass, JsParameters parameters, JsBlock constructor)
		{
			return new ExtJsClass(className, baseClass, parameters, constructor);
		}
		#endregion

		#region Helpers
		/// <summary>
		/// Works out the namespace part of the class name
		/// </summary>
		public static string GetNamespace(string className)
		{

			int lastDot = className.LastIndexOf(".");

			if (lastDot >= 0)
				return className.Substring(0, lastDot);
			else
				return null;

		}

		/// <summary>
		/// Provides a Script item to do a call to a base classes functions
		/// Should be used when overriding base functions
		/// see BaseApply as an alternative
		/// </summary>
		/// <param name="className">name of the calling class</param>
		/// <param name="function">function to call in the base class (should normally be same name as current function)</param>
		/// <param name="parameters">parameters to pass to the base function</param>
		/// <returns></returns>
        public static JsStatement BaseCall(object className, object function, params object[] parameters)
		{
			return Js.Statement(
				Js.Call(Sb.Line(className,".superclass.",function,".call"),
					"this",
					parameters
				)
			);
		}

		/// <summary>
		/// Provides a Script item to do a call to a base classes functions
		/// Should be used when overriding base functions
		/// The preferred way to call base functions as it does not depend on paremters
		/// see BaseCall as an alternative
		/// </summary>
		/// <param name="className">name of the calling class</param>
		/// <param name="function">function to call in base class (should normally be same name as current function)</param>
		/// <returns></returns>
        public static JsStatement BaseApply(object className, string function)
		{
			return Js.Statement(
				Js.Call(Sb.Line(className,".superclass.",function,".apply"),
					"this",
					"arguments"
				)
			);
		}

        /// <summary>
        /// Convert .Net types to Javascript types
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
		public static string ExtJsType(Type type)
		{
			switch (type.ToString())
			{
				case "Int16":
				case "Int32":
				case "Int64":
					return "int";
				case "Double":
				case "Float":
				case "Decimal":
					return "float";
				case "DateTime":
					return "date";
				case "Boolean":
					return "bool";
			}

			return "string";
		}
		#endregion
	}
}
