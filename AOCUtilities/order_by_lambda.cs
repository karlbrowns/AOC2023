namespace AdventOfCodeUtilities
{
    public static class AoC
    {
        public static IOrderedEnumerable<TSource> OrderByLambda<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TKey?, TKey?, int> compareFunc)
        {
            var comparer = new AoCComparer<TKey>(compareFunc, false);
            return source.OrderBy(keySelector, comparer);
        }
		
		public static IOrderedEnumerable<TSource> OrderByLambdaDescending<TSource, TKey>(
			this IEnumerable<TSource> source,
			Func<TSource, TKey> keySelector,
			Func<TKey?, TKey?, int> compareFunc)
		{
			var comparer = new AoCComparer<TKey>(compareFunc, true);
			return source.OrderBy(keySelector, comparer);
		}
	}

	internal class AoCComparer<TKey> : IComparer<TKey>
	{
		private readonly Func<TKey?, TKey?, int> _compareFunc;
		private readonly bool _invert;

		public AoCComparer(Func<TKey?, TKey?, int> compareFunc, bool invert)
		{
			_compareFunc = compareFunc;
			_invert = invert;
		}

		public int Compare(TKey? x, TKey? y)
		{
			int result = _compareFunc(x, y);
			return _invert ? result * -1 : result;
		}
	}
}

/*
list_of_something.OrderByLambda(something => something.interesting_field, (x, y) => {
	if (x > y)
		return 1;
	else if (x < y)
		return -1;
	else
		return 0;
	
	// Or something MUCH more complex, like sorting hands in a card game perhaps...
});
