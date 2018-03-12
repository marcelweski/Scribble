﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scribble
{
	static public class IEnumerableExtensions
	{
		static Random random = new Random((int)DateTime.Now.Ticks);

		static public T Pick<T>(this IEnumerable<T> data)
		{
			return data.ElementAt(random.Next(data.Count()));
		}
	}
}
