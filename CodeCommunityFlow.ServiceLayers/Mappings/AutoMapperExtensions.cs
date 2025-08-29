using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeCommunityFlow.Mappings
{
    //public static class AutoMapperExtensions
    //{
    //    public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(
    //   this IMappingExpression<TSource, TDestination> expression)
    //    {
    //        var existingMaps = expression
    //            .TypeMap
    //            .GetPropertyMaps()
    //            .Where(pm => !pm.IsIgnored && pm.DestinationProperty != null)
    //            .Select(pm => pm.DestinationProperty.Name);

    //        foreach (var propertyName in expression.TypeMap.DestinationType.GetProperties().Select(p => p.Name))
    //        {
    //            if (!existingMaps.Contains(propertyName))
    //            {
    //                expression.ForMember(propertyName, opt => opt.Ignore());
    //            }
    //        }
    //        return expression;
    //    }
    //}

}