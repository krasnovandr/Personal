using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Task2
{
    public class Mapper<TSource, TDestination>
    {
        readonly Func<TSource, TDestination> _mapFunction;

        public Mapper(Func<TSource, TDestination> func)
        {
            _mapFunction = func;
        }
        public TDestination Map(TSource source)
        {
            return _mapFunction(source);
        }
    }

    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            var sourceParam = Expression.Parameter(typeof(TSource));

            var properties = sourceParam.Type.GetProperties();
            var bindings = new List<MemberAssignment>();

            foreach (var property in properties)
            {
                bindings.Add(Expression.Bind(typeof(TDestination).GetProperty(property.Name),
                   Expression.Property(sourceParam, property)));
            }

            var body = Expression.MemberInit(Expression.New(typeof(TDestination)), bindings);
            var expr = Expression.Lambda<Func<TSource, TDestination>>(body, sourceParam);

            return new Mapper<TSource, TDestination>(expr.Compile());
        }
    }



}
