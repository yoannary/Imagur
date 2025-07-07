using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ImgurCloneAuth
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

           /* //PhotoUpvote
            routes.MapRoute(
                               name: "PhotoUpvote",
                                              url: "Photo/View/{id}/Upvote",
                                                             defaults: new { controller = "Photo", action = "Upvote", id = UrlParameter.Optional }
                                                                        );

            //PhotoDownvote
            routes.MapRoute(
                                              name: "PhotoDownvote",
                                                                                           url: "Photo/View/{id}/Downvote",
                                                                                                                                                       defaults: new { controller = "Photo", action = "Downvote", id = UrlParameter.Optional }
                                                                                                                                                                                                                              ); */
        }
    }
}
