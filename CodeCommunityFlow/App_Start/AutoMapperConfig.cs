using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using CodeCommunityFlow.ServiceLayers.ProfilesMapping;

namespace CodeCommunityFlow.App_Start
{

    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
        {

            return expression;
        }
    }

}