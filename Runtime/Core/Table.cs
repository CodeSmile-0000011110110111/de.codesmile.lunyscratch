// Variable wrapper (weakly typed, like Scratch)

using System;
using System.Collections.Generic;

namespace LunyScratch
{
	// Table (array + dictionary hybrid, like Lua)
	public class Table
	{
		private readonly List<Variable> _array = new();
		private readonly Dictionary<String, Variable> _dictionary = new();

		// Array operations (1-indexed like Scratch/Lua)
		public void Add(Variable value) => _array.Add(value);
		public Variable Get(Int32 index)
		{
			if (index < 0 || index >= _array.Count)
				return new Variable(0);

			return _array[index - 1];
			// 1-indexed!
		}

		public void Set(Int32 index, Variable value) => _array[index - 1] = value;
		public Int32 Length() => _array.Count;

		// Dictionary operations
		public Variable Get(String key) => _dictionary.TryGetValue(key, out var v) ? v : default;
		public void Set(String key, Variable value) => _dictionary[key] = value;
		public Boolean Has(String key) => _dictionary.ContainsKey(key);
	}
}
