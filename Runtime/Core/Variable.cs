// Copyright (C) 2021-2025 Steffen Itterheim
// Refer to included LICENSE file for terms and conditions.

using System;

namespace LunyScratch
{
	public enum ValueType
	{
		Nil = 0,
		Boolean,
		Number,
		String,
	}

	public struct Variable
	{
		private ValueType _valueType;
		private Double _number;
		private String _string;

		public Variable(Boolean truthValue)
		{
			_valueType = ValueType.Boolean;
			_number = truthValue ? 1 : 0;
			_string = null;
		}

		public Variable(Double number)
		{
			_valueType = ValueType.Number;
			_number = number;
			_string = null;
		}

		public Variable(String text)
		{
			_valueType = ValueType.String;
			_number = 0;
			_string = text;
		}

		public Double AsNumber() => _valueType switch
		{
			ValueType.Boolean or ValueType.Number => _number,
			var _ => 0.0,
		};

		public String AsString() => _valueType switch
		{
			ValueType.Boolean or ValueType.Number => _number.ToString(),
			ValueType.String => _string ?? String.Empty,
			var _ => String.Empty,
		};

		public Boolean AsBool() => _valueType switch
		{
			ValueType.Boolean or ValueType.Number => _number != 0,
			var _ => false,
		};

		public void Set(Double number)
		{
			_valueType = ValueType.Number;
			_number = number;
		}

		public void Set(Boolean truthValue)
		{
			_valueType = ValueType.Boolean;
			_number = truthValue ? 1 : 0;
		}

		public void Set(String text)
		{
			_valueType = ValueType.String;
			_string = text;
		}

		public static implicit operator Variable(Int32 v) => new(v);
		public static implicit operator Variable(Single v) => new(v);
		public static implicit operator Variable(Boolean v) => new(v);
		public static implicit operator Variable(String v) => new(v);
	}
}
