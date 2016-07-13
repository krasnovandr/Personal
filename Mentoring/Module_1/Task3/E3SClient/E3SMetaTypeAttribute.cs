using System;

namespace Task3.E3SClient
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	class E3SMetaTypeAttribute : Attribute
	{
		public string Name { get; private set; }

		public E3SMetaTypeAttribute(string name)
		{
			Name = name;
		}
	}
}
