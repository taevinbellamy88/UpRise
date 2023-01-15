﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace UpRise.Web.Core.Filters
{
    public abstract class BaseAuthActionFilter : System.Attribute, Microsoft.AspNetCore.Mvc.Filters.IAsyncActionFilter
    {

        /// <summary>
        /// The parameter name to look for in the Request that holds the Id of the given entity.
        /// </summary>
        public string EntityIdField { get; set; }

        public EntityType EntityTypeId { get; set; }

        public EntityActionType Action { get; set; }

        public abstract Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next);


    }
}
