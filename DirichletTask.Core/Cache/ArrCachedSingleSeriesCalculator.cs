using System;
using DirichletTask.Core.Abstraction.Cache;

namespace DirichletTask.Core.Cache
{
    public class ArrCachedSingleSeriesCalculator : ICachedSingleSeriesCalculator<int, double, double>
    {
        private double?[] _cache;
        private int? _cacheSize;
        public virtual double Calculate(int n, double x)
        {
            if (!_cacheSize.HasValue)
            {
                throw new Exception("Please provide cach size");
            }

            if (n > _cacheSize.Value || n < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(n));
            }

            if (!_cache[n].HasValue)
            {
                _cache[n] = Calculate(n, x);
            }

            return _cache[n].Value;
        }

        public virtual void EmptyCache()
        {
            for(int i = 0; i < _cache.Length; ++i)
            {
                _cache[i] = null;
            }
        }

        public virtual void SetCacheSize(int size)
        {
            _cacheSize = size;
            _cache = new double?[size];
        }
    }
}
